using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Repos
{
    /// <summary>
    /// Base interface for database manipulation
    /// CreatedBy Trinh Quy Cong - 5/7/2022
    /// </summary>
    /// <typeparam name="MISAEntity"></typeparam>
    public interface IBaseRepo<MISAEntity>
    {
        /// <summary>
        /// get all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// get entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MISAEntity GetById(Guid id);

        /// <summary>
        /// return true if entity exists
        /// return false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExist(Guid id);

        /// <summary>
        /// add new entity into database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(MISAEntity entity);

        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(MISAEntity entity);

        /// <summary>
        /// delete entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(Guid id);
    }
}
