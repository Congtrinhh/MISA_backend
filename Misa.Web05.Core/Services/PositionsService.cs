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
    public class PositionsService : BaseService<Positions>, IPositionsService
    {
        #region Properties
        private IPositionsRepo _positionsRepo;

        #endregion
        #region Constructor
        public PositionsService(IPositionsRepo positionsRepo) : base(positionsRepo)
        {
            _positionsRepo = positionsRepo;
        }
        #endregion


        protected override bool Validate(Positions pos)
        {
            bool valid = true;
            // check position id khác null
            if (string.IsNullOrEmpty(pos.PositionId.ToString()))
            {
                valid = false;
                ErrorMessages.Add("Mã vị trí không được trống");
            }

            // check position id trùng
            if (_positionsRepo.CheckExist(pos.PositionId))
            {
                valid = false;
                ErrorMessages.Add("Mã vị trí đã tồn tại");
            }

            // check position name khác null
            if (string.IsNullOrEmpty(pos.PositionName))
            {
                valid = false;
                ErrorMessages.Add("Tên vị trí không được trống");
            }
            return valid;
        }
    }
}
