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
    /// Created by TQCONG 1/7/2022
    /// </summary>
    /// <typeparam name="MISAEntity">Một trong các class như Employee/Department/Positions/...</typeparam>
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
            // khởi tạo chuỗi kết nối
            SqlConnectionString = "User Id=dev;Host=3.0.89.182;Port=3306;Database=MISA.WEB05.TQCONG; Password=12345678";
            // lấy ra tên table trong DB (dựa vào tên model vì đặt giống nhau)
            SqlTableName = typeof(MISAEntity).Name;
            // mặc định tên table = tên entity (model)
            SqlEntityName = SqlTableName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check đối tượng tồn tại trong DB
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>true nếu tồn tại; ngược lại false</returns>
        /// CreatedBy TQCONG 1/7/2022
        public bool CheckExist(Guid id)
        {
            // lấy ra đối tượng theo id
            MISAEntity entity = GetById(id);
            // nếu đối tượng tồn tại
            if (entity != null)
            {
                return true;
            }
            // nếu không tồn tại
            return false;
        }

        /// <summary>
        /// Xoá đối tượng khỏi DB
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>1 nếu xoá thành công</returns>
        /// CreatedBy TQCONG 1/7/2022
        public int Delete(Guid id)
        {
            // sử dụng khối using với tài nguyên: ngay sau khi kết thúc khối lệnh, tài nguyên được giải phóng
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh truy vấn
                var sql = $"DELETE FROM {SqlTableName} WHERE {SqlEntityName}Id=@id";
                // thêm param
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                // trả về kết quả thực thi câu lệnh xoá
                return Conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        /// Lấy ra tất cả đối tượng trong DB
        /// </summary>
        /// <returns>Tất cả đối tượng trong DB</returns>
        /// CreatedBy TQCONG 1/7/2022
        public virtual IEnumerable<MISAEntity> GetAll()
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh truy vấn
                var sql = $"SELECT * FROM {SqlTableName}";
                // lấy ra list tất cả entity
                var entityList = Conn.Query<MISAEntity>(sql).ToList();
                // trả về list
                return entityList;
            }
        }

        /// <summary>
        /// Lấy ra đối tượng dựa vào id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>Đối tượng với id tương ứng</returns>
        /// CreatedBy TQCONG 1/7/2022
        public virtual MISAEntity GetById(Guid id)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh truy vấn
                var sql = $"SELECT * FROM {SqlTableName} WHERE {SqlEntityName}Id=@id";
                // thêm param
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                // trả về entity hoặc null nếu không tìm thấy
                return Conn.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
            }
        }

        /// <summary>
        /// Lấy ra danh sách đối tượng theo thông tin phân trang
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang, bắt đầu từ 0</param>
        /// <param name="size">Số phần từ trên 1 trang</param>
        /// <param name="keyword">Từ khoá để lọc</param>
        /// <returns>Danh sách đối tượng và thông tin phân trang</returns>
        /// CreatedBy TQCONG 1/7/2022
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
        /// Thêm đối tượng vào DB
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 1/7/2022
        public int Insert(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh sql thủ tục
                var sql = $"Proc_Insert{SqlTableName}";
                // create entity
                var res = Conn.Execute(sql, entity, commandType: System.Data.CommandType.StoredProcedure);
                // tra ve 1 neu thanh cong; 0 neu that bai
                return res;
            }
        }

        /// <summary>
        /// Sửa đối tượng trong DB
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>1 nếu thành công</returns>
        /// CreatedBy TQCONG 1/7/2022
        public int Update(MISAEntity entity)
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                // khởi tạo câu lệnh sql thủ tục
                var sql = $"Proc_Update{SqlTableName}";
                // thực thi cập nhật
                var res = Conn.Execute(sql, entity, commandType: System.Data.CommandType.StoredProcedure);
                // trả về 0 nếu thành công; trả về 1 nếu thất bại
                return res;
            }
        }

        #endregion
    }
}
