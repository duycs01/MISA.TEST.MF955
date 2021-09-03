using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using MISA.TEST.API.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region DECLEAR
        IDepartmentRepository _departmentRepository;
        #endregion

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="departmentRepository">Kết nối database</param>
        /// CreateBy duylv - 27/08/2021
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

    }
}
