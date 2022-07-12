using Dapper;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Infrastructure.Repos
{
    /// <summary>
    /// Class tổng quát thực thi interface IBaseRepo
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public class BaseRepo<MISAEntity> : IBaseRepo<MISAEntity>
    {
        #region Properties
        /// <summary>
        /// Chuỗi kết nối sql
        /// </summary>
        protected string SqlConnectionString;
        /// <summary>
        /// Đối tượng sql
        /// </summary>
        protected MySqlConnection Conn;

        /// <summary>
        /// Tên table trong database
        /// </summary>
        protected string SqlTableName;

        /// <summary>
        /// Mặc định, SqlEntityName == SqlTableName
        /// Với những bảng có 's' ở cuối như Positions, SqlEntityName=position thay vì positions
        /// </summary>
        protected string SqlEntityName;
        #endregion

        #region Contructor
        public BaseRepo()
        {
            SqlConnectionString = "User Id=dev;Host=3.0.89.182;Port=3306;Database=MISA.WEB05.TQCONG; Password=12345678";
            SqlTableName = typeof(MISAEntity).Name;
            SqlEntityName = SqlTableName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check đối tượng tồn tại trong DB
        /// </summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>true nếu tồn tại; ngược lại false</returns>
        public bool CheckExist(Guid id)
        {
            MISAEntity entity = GetById(id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Xoá đối tượng khỏi DB
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>1 nếu xoá thành công</returns>
        public int Delete(Guid id)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"DELETE FROM {SqlTableName} WHERE {SqlEntityName}Id=@id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return Conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// lấy ra tất cả đối tượng trong DB
        /// </summary>
        /// <returns>tất cả đối tượng trong DB</returns>
        public IEnumerable<MISAEntity> GetAll()
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"SELECT * FROM {SqlTableName}";
                var entityList = Conn.Query<MISAEntity>(sql).ToList();
                return entityList;
            }
        }

        /// <summary>
        /// lấy ra đối tượng dựa vào id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>đối tượng với id tương ứng</returns>
        public MISAEntity GetById(Guid id)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"SELECT * FROM {SqlTableName} WHERE {SqlEntityName}Id=@id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return Conn.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">chỉ số trang, bắt đầu từ 0</param>
        /// <param name="size">số phần từ trên 1 trang</param>
        /// <param name="keyword">từ khoá để lọc</param>
        /// <returns></returns>
        public Paging GetPaging(int pageIndex, int size, string keyword)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh sql
                var sqlProcedure = $"Proc_Filter{SqlTableName}";

                // thêm param
                var parameters = new DynamicParameters();
                parameters.Add("@PageIndex", pageIndex); // tên param có @ hay không đều được
                parameters.Add("@Size", size);
                parameters.Add("@Keyword", keyword);
                parameters.Add("@TotalRecords", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@RecordEnd", direction: System.Data.ParameterDirection.Output);

                // lấy ra danh sách đối tượng và các thông tin phân trang
                List<MISAEntity> list = Conn.Query<MISAEntity>(sqlProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
                var totalRecords = parameters.Get<int>("@TotalRecords");
                var recordStart = parameters.Get<int>("@RecordStart");
                var recordEnd = parameters.Get<int>("@RecordEnd");

                // trả về đối tượng paging
                return new Paging(list, totalRecords, recordStart, recordEnd);
            }
        }

        /// <summary>
        /// thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        public int Insert(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"Proc_Insert{SqlTableName}";
                // create entity
                var res = Conn.Execute(sql, entity, commandType: System.Data.CommandType.StoredProcedure);
                // tra ve 1 neu thanh cong; 0 neu that bai
                return res;
            }
        }

        /// <summary>
        /// sửa đối tượng trong DB
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        public int Update(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"Proc_Update{SqlTableName}";
                // update entity
                var res = Conn.Execute(sql, entity, commandType: System.Data.CommandType.StoredProcedure);
                // tra ve 1 neu thanh cong; 0 neu that bai
                return res;
            }
        }

        #endregion
    }
}
