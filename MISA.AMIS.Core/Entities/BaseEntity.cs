using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Entities
{
    public class BaseEntity
    {
        #region Property
        /// <summary>
        ///  Ngày tạo bản ghi
        ///  CreatedBy: duylv - 27/08/2021
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo bản ghi
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa bản ghi
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa bản ghi
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
