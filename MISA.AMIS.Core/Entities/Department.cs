using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khóa chính Đơn vị
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public string Description { get; set; }
    }
}
