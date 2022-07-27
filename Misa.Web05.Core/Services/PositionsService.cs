using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using Misa.Web05.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Services
{
    // <summary>
    /// Service cho đối tượng Positions
    /// Created by TQCONG 5/7/2022
    /// </summary>
    public class PositionsService : BaseService<Positions>, IPositionsService
    {
        #region Properties
        /// <summary>
        /// repo để tương tác với DB: thêm, sửa, xoá, đọc
        /// </summary>
        private IPositionsRepo _positionsRepo;
        #endregion

        #region Constructor
        public PositionsService(IPositionsRepo positionsRepo) : base(positionsRepo)
        {
            _positionsRepo = positionsRepo;
        }
        #endregion

        /// <summary>
        /// Validate position
        /// </summary>
        /// <param name="pos">Đối tượng position</param>
        /// <returns>true nếu hợp lệ; false nếu không hợp lệ</returns>
        /// CreatedBy TQCONG 5/7/2022
        protected override bool Validate(Positions pos)
        {
            bool valid = true;
            // check position id khác null
            if (string.IsNullOrEmpty(pos.PositionId.ToString()))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.PositionIdNull);
            }

            // check position id trùng
            if (_positionsRepo.CheckExist(pos.PositionId))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.PositionIdExists);
            }

            // check position name khác null
            if (string.IsNullOrEmpty(pos.PositionName))
            {
                valid = false;
                ErrorMessages.Add(Resources.ExceptionErrorMessage.PositionNameNull);
            }
            return valid;
        }
    }
}
