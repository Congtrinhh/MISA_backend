using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Web05.Core.Interfaces.Services
{
    // add comment
    public interface IBaseService<MISAEntity>
    {
        int Insert(MISAEntity entity);

        int Update(MISAEntity entity);
    }
}
