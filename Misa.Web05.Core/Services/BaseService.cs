using Misa.Web05.Core.Exceptions;
using Misa.Web05.Core.Interfaces.Repos;
using Misa.Web05.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {

        #region Properties
        private IBaseRepo<MISAEntity> _repo;
        protected List<string> ErrorMessages=new List<string>();
        #endregion

        #region Contructor
        public BaseService(IBaseRepo<MISAEntity> repo)
        {
            _repo = repo;
        }
        #endregion

        #region Methods
        public int Insert(MISAEntity entity)
        {
            // validate input
            if (!Validate(entity))
            {
                throw new MISAValidationException(ErrorMessages);
            };

            // insert
            return _repo.Insert(entity);
        }

        public int Update(MISAEntity entity)
        {
            // validate input
            if (!Validate(entity))
            {
                throw new MISAValidationException(ErrorMessages);
            };

            // update
            return _repo.Update(entity);
        }



        /// <summary>
        /// validate đối tượng nhận vào
        /// trả về true nếu hợp lệ
        /// trả về false nếu không hợp lệ
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        protected virtual bool Validate(MISAEntity entity) { return true; }
        #endregion
    }
}
