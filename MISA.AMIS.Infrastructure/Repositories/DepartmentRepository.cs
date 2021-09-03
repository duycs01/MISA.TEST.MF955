using Microsoft.Extensions.Configuration;
using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>,IDepartmentRepository
    {
        #region Contructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
