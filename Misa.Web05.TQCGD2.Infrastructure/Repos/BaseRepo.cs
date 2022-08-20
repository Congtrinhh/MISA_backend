using Dapper;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Models.Paging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.TQCGD2.Infrastructure.Repos
{
    /// <summary>
    /// Class base thực thi inteface IBaseRepo
    /// </summary>
    /// <typeparam name="MISAEntity">Tên 1 class như User, Department,..</typeparam>
    /// CreatedBy TQCONG 29/7/2022
    public class BaseRepo<MISAEntity> : IBaseRepo<MISAEntity> where MISAEntity : BaseEntity
    {

        #region Property
        /// <summary>
        /// Chuỗi kết nối sql
        /// </summary>
        protected string ConnectionString;

        /// <summary>
        /// Đối tượng kết nối với database
        /// </summary>
        protected MySqlConnection Conn;

        /// <summary>
        /// Tên table trong database của entity đang tương tác với database
        /// </summary>
        protected string TableName;

        /// <summary>
        /// Tên entity 
        /// (cần có trường này để xử lý trường hợp đặc biệt. ví dụ: tên table là Positions
        /// nhưng ta cần tên entity là Position - cần bỏ kí tự s ở cuối đi)
        /// </summary>
        protected string EntityName;

        /// <summary>
        /// Lưu trữ câu lệnh sql cho method GetById
        /// </summary>
        protected SqlStatementHolder SqlStatementGetById { get; set; }

        /// <summary>
        /// Lưu trữ câu lệnh sql cho method GetPaging
        /// </summary>
        protected SqlStatementHolder SqlStatementGetPaging { get; set; }

        /// <summary>
        /// Lưu trữ câu lệnh sql cho method GetAll
        /// </summary>
        protected SqlStatementHolder SqlStatementGetAll { get; set; }
        #endregion

        #region Constructor
        public BaseRepo()
        {
            ConnectionString = "User Id=root;Host=localhost;Port=3306;Database=tqc_gd2; Password=12345678";
            TableName = typeof(MISAEntity).Name;
            EntityName = typeof(MISAEntity).Name;

            SqlStatementGetById = new SqlStatementHolder() { Select = "*", From = $"{TableName}", Where = $"{EntityName}Id=@{EntityName}Id" };
            SqlStatementGetAll = new SqlStatementHolder() { Select = "*", From = $"{TableName}" };
            SqlStatementGetPaging = new SqlStatementHolder() { Select = "*", From = $"{TableName}", Where = string.Empty, OrderBy = string.Empty };
        }
        #endregion

        #region Method
        /// <summary>
        /// Xoá entity
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>1 nếu xoá thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        public virtual async Task<int> DeleteAsync(int id)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"DELETE FROM {TableName} WHERE {EntityName}Id=@{EntityName}Id";
                var parameters = new DynamicParameters();
                parameters.Add($"@{EntityName}Id", id);
                return await Conn.ExecuteAsync(sql, parameters);
            }
        }

        /// <summary>
        /// Lấy ra entity theo id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity tương ứng</returns>
        /// CreatedBy TQCONG 29/7/2022 
        public virtual async Task<MISAEntity> GetByIdAsync(int id)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                SetSqlStatementGetById();

                string sql = $"SELECT {SqlStatementGetById.Select} FROM {SqlStatementGetById.From} WHERE {SqlStatementGetById.Where}";
                var parameters = new DynamicParameters();
                parameters.Add($"@{EntityName}Id", id);
                return await Conn.QueryFirstOrDefaultAsync<MISAEntity>(sql, parameters);
            }
        }

        /// <summary>
        /// Lấy ra list entity
        /// </summary>
        /// <returns>List entity</returns>
        /// CreatedBy TQCONG 29/7/2022 
        public virtual async Task<IEnumerable<MISAEntity>> GetAllAsync()
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                SetSqlStatementGetAll();

                string sql = $"SELECT {SqlStatementGetAll.Select} FROM {SqlStatementGetAll.From}";
                IEnumerable<MISAEntity> list = await Conn.QueryAsync<MISAEntity>(sql);
                return list;
            }
        }

        /// <summary>
        /// Lấy ra danh sách entity dựa vào kết quả tìm kiếm và phân trang
        /// </summary>
        /// <param name="paginationRequest">Object chứa các trường để lọc dữ liệu</param>
        /// <returns>Đối tượng chứa danh sách entity và các thông tin phân trang</returns>
        /// CreatedBy TQCONG 2/8/2022 
        public async Task<PaginationResponse<MISAEntity>> GetPagingAsync(BasePaginationRequest paginationRequest)
        {
            // số bản ghi muốn lấy
            var limit = paginationRequest.PageSize;
            // vị trí bắt đầu lấy
            var offset = (paginationRequest.CurrentPage - 1) * paginationRequest.PageSize;

            // set up câu lệnh sql
            SetSqlStatementGetPaging(paginationRequest);

            // lấy ra các mệnh đề sql
            var sqlSelectSubfix = SqlStatementGetPaging.Select;
            var sqlFromSubfix = SqlStatementGetPaging.From;
            var sqlWhereSubfix = SqlStatementGetPaging.Where;
            var sqlOrderBySubfix = SqlStatementGetPaging.OrderBy;

            // câu lệnh sql
            var sqlTotalCount = $@"SELECT DISTINCT COUNT(*) OVER () AS TotalRecords FROM {sqlFromSubfix} WHERE 1=1 {sqlWhereSubfix}";
            var sqlItems = $@"SELECT {sqlSelectSubfix} FROM {sqlFromSubfix} WHERE 1=1 {sqlWhereSubfix} ORDER BY {sqlOrderBySubfix} LIMIT @limit OFFSET @offset";
            var sql = $"{sqlTotalCount}; {sqlItems}";

            using (Conn = new MySqlConnection(ConnectionString))
            {
                var reader = await Conn.QueryMultipleAsync(sql, new { limit, offset });

                int totalCount = reader.Read<int>().FirstOrDefault();
                List<MISAEntity> data = reader.Read<MISAEntity>().ToList();

                return new PaginationResponse<MISAEntity>(data, totalCount, paginationRequest.PageSize, paginationRequest.CurrentPage);
            }
        }

        /// <summary>
        /// Set up câu lệnh sql cho method GetPaging
        /// </summary>
        /// <param name="paginationRequest">Dùng để build mệnh đề WHERE</param>
        /// CreatedBy TQCONG 8/8/2022
        public virtual void SetSqlStatementGetPaging(BasePaginationRequest paginationRequest) { }

        /// <summary>
        /// Set up câu lệnh sql cho method GetById
        /// </summary>
        /// CreatedBy TQCONG 8/8/2022
        public virtual void SetSqlStatementGetById() { }

        /// <summary>
        /// Set up câu lệnh sql cho method GetAll
        /// </summary>
        /// CreatedBy TQCONG 8/8/2022
        public virtual void SetSqlStatementGetAll() { }

        /// <summary>
        /// Thêm entity 
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>1 nếu thêm thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        public virtual async Task<int> InsertAsync(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"Proc_Insert{TableName}";
                return await Conn.ExecuteAsync(sql: sql, param: entity, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Cập nhật entity
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        /// <returns>1 nếu sửa thành công</returns>
        /// CreatedBy TQCONG 29/7/2022 
        public virtual async Task<int> UpdateAsync(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"Proc_Update{TableName}";
                return await Conn.ExecuteAsync(sql: sql, param: entity, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Lấy ra đối tượng Sql Connection
        /// </summary>
        /// <returns></returns>
        /// CreatedBy TQCONG 8/8/2022
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        #endregion
    }
}
