using Dapper;
using Misa.Web05.Core.Interfaces.Repos;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Infrastructure.Repos
{
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
        public bool CheckExist(Guid id)
        {
            MISAEntity entity = GetById(id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }

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

        public IEnumerable<MISAEntity> GetAll()
        {
            using (Conn = new MySqlConnection(SqlConnectionString))
            {
                var sql = $"SELECT * FROM {SqlTableName}";
                var entityList = Conn.Query<MISAEntity>(sql).ToList();
                return entityList;
            }
        }

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
