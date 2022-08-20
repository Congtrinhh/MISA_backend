using Misa.Web05.TQCGD2.Core.Exceptions;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Resources;
using Misa.Web05.TQCGD2.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Misa.Web05.TQCGD2.Core.Services
{
    /// <summary>
    /// Class User service
    /// </summary>
    /// CreatedBy TQCONG 4/8/2022
    public class UserService : BaseService<User>, IUserService
    {
        #region Property
        private readonly IUserRoleRepo _userRoleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;
        #endregion
        #region Contructor
        public UserService(IUserRepo userRepo, IUserRoleRepo userRoleRepo, IRoleRepo roleRepo) : base(userRepo)
        {
            _userRoleRepo = userRoleRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }
        #endregion

        #region Method
        /// <summary>
        /// Insert dữ liệu vào bảng user_role và
        /// cập nhật các trường DepartmentName, PositionName, RoleNames của user
        /// cho đúng
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        public override async Task AfterInsert(User entity)
        {
            // lấy ra user vừa được thêm từ DB ra với userId chuẩn
            User newlyCreatedUser = await _userRepo.GetByUserCodeAsync(entity.UserCode);

            // thêm dữ liệu vào bảng user_role
            if (entity.Roles != null && entity.Roles.Count > 0)
            {
                foreach (var role in entity.Roles)
                {
                    UserRole userRole = new UserRole(newlyCreatedUser.UserId, role.RoleId);
                    if (await _userRoleRepo.InsertAsync(userRole) != 1)
                    {
                        // TODO exception lỗi insert vào bảng user_role -dùng resource
                        throw new Exception(ExceptionErrorMessage.InsertUserRoleFailure);
                    }
                }
            }
        }

        /// <summary>
        /// insert đối tượng user vào DB
        /// </summary>
        /// <param name="entity">đối tượng user</param>
        /// <returns>1 nếu insert thành công đối tượng user</returns>
        /// CreatedBy TQCONG 4/8/2022
        public override Task<int> DoInsert(User entity)
        {
            return base.DoInsert(entity);
        }

        /// <summary>
        /// build thuộc tính roleNames cho đối tượng user
        /// </summary>
        /// <param name="entity">đối tượng user</param>
        /// <returns></returns>
        /// CreatedBy TQCONG 4/8/2022
        public override async Task BeforeInsert(User entity)
        {
            if (entity.Roles != null && entity.Roles.Count > 0)
            {
                var roleNames = "";
                foreach (var role in entity.Roles)
                {
                    // lấy role từ DB ra để có thể lấy tên role
                    var roleFromDB = await _roleRepo.GetByIdAsync(role.RoleId);
                    roleNames += $"{roleFromDB.Name}; ";
                }

                roleNames = roleNames.Substring(0, roleNames.Length - 2);

                entity.RoleNames = roleNames;
            }

        }

        /// <summary>
        /// Cập nhật user: các thông tin trong bảng user và
        /// bảng User_role (nếu có cập nhật role)
        /// </summary>
        /// <param name="entity">Đối tượng user</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 4/8/2022
        public override async Task<int> DoUpdate(User entity)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Cập nhật role của user (nếu có)
                    if (entity.Roles != null && entity.Roles.Count > 0)
                    {
                        // Lấy ra user từ DB (ta có thể lấy theo UserCode vì UserCode sẽ giữ nguyên (không bị cập nhật)
                        // cần lấy ra trong vòng for để chắc chắn rằng user là mới nhất
                        User userFromDB = await _userRepo.GetByUserCodeAsync(entity.UserCode);

                        foreach (var role in entity.Roles)
                        {
                            switch (role.ModificationMode)
                            {
                                case Enums.ModificationMode.Insert:
                                    // thêm user_role vào DB
                                    UserRole userRoleToInsert = new UserRole(userFromDB.UserId, role.RoleId);
                                    await _userRoleRepo.InsertAsync(userRoleToInsert);
                                    break;
                                case Enums.ModificationMode.Remove:
                                    UserRole userRoleToRemove = new UserRole(userFromDB.UserId, role.RoleId);
                                    await _userRoleRepo.DeleteAsync(userFromDB.UserId, role.RoleId);
                                    break;
                            }
                        }

                        // Lấy ra user mới nhất (mục đích để lấy ra list role mới nhất -> build roleNames cho user để cập nhật roleNames)
                        var mostUpToDateUser = await _userRepo.GetByIdAsync(userFromDB.UserId);

                        // build roleNames cho entity từ list role mới nhất
                        var tempRoleNames = "";
                        if (mostUpToDateUser.Roles != null && mostUpToDateUser.Roles.Count > 0)
                        {
                            foreach (var role in mostUpToDateUser.Roles)
                            {
                                tempRoleNames += $"{role.Name}; ";
                            }
                            entity.RoleNames = tempRoleNames.Substring(0, tempRoleNames.Length - 2);
                        }
                    }

                    // Cập nhật user
                    int rowsAffected = await _userRepo.UpdateAsync(entity);
                    // Commit transaction
                    transaction.Complete();
                    return rowsAffected;
                }
                catch
                {
                    throw new MISAValidationException(new List<string>());
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Tạo nhiều user
        /// </summary>
        /// <param name="users">Danh sách user</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        /// CreatedBy TQCONG 8/8/2022
        public async Task<int> InsertManyAsync(IEnumerable<User> users)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var user in users)
                    {
                        int rowsAffected = await this.InsertAsync(user);
                        if (rowsAffected != 1)
                        {
                            throw new MISAValidationException(new List<string>());
                        }
                    }
                    // return về 1 nếu insert tất cả user trong list user thành công
                    transaction.Complete();
                    return users.Count();
                }
                catch
                {
                    throw new MISAValidationException(new List<string>());
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// validate dữ liệu của user
        /// </summary>
        /// <param name="entity">Đối tượng user</param>
        /// <returns>hợp lệ: true; không hợp lệ: false</returns>
        /// CreatedBy TQCONG 16/8/2022
        public override bool Validate(User entity)
        {
            bool valid = true;
            // check null
            // check department và position nếu thêm mới,
            // không cần check Id vì Id đã có ràng buộc bắt buộc trong class của nó
            if (CrudMode == Enums.CrudMode.Add)
            {
                if (entity.Department == null)
                {
                    this.ErrorMessages.Add(ExceptionErrorMessage.DepartmentNull);
                    valid = false;
                }
                if (entity.Position == null)
                {
                    this.ErrorMessages.Add(ExceptionErrorMessage.PositionNull);
                    valid = false;
                }
                // check trùng mã user code
                var userFromDb = _userRepo.GetByUserCodeAsync(entity.UserCode);
                if (userFromDb != null && userFromDb.Result != null)
                {
                    ErrorMessages.Add(string.Format(ExceptionErrorMessage.UserCodeExists, entity.UserCode));
                    valid = false;
                }
            }

            // check null role cho cả khi thêm mới và update
            if (entity.Roles == null || entity.Roles.Count <= 0)
            {
                this.ErrorMessages.Add(ExceptionErrorMessage.RoleNull);
                valid = false;
            }

            // check định dạng mã user code
            if (!CommonMethods.IsUserCodeValid(entity.UserCode))
            {
                this.ErrorMessages.Add(string.Format(ExceptionErrorMessage.UserCodeInvalid, entity.UserCode));
                valid = false;
            }

            // check định dạng email
            if (!CommonMethods.IsEmailValid(CommonMethods.GetEmptyStringIfNull(entity.Email)))
            {
                this.ErrorMessages.Add(string.Format(ExceptionErrorMessage.EmailInvalid, entity.Email));
                valid = false;
            }

            // check định dạng họ tên
            if (!CommonMethods.IsFullNameValid(entity.FullName))
            {
                this.ErrorMessages.Add(string.Format(ExceptionErrorMessage.FullNameInvalid, entity.FullName));
                valid = false;
            }
            return valid;
        }

        #endregion

    }
}
