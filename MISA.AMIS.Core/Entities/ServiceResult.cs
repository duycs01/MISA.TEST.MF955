using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.TEST.Core.Entities
{
    public class ServiceResult
    {
        #region Property

        /// <summary>
        /// Kiểm tra lỗi
        /// CreatedBy: duylv-27/08/2021
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Dữ liệu trả về
        /// CreatedBy: duylv-27/08/2021
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông báo
        /// CreatedBy: duylv-27/08/2021
        /// </summary>
        public string Messenger { get; set; }

        /// <summary>
        /// Trạng thái restful
        /// CreatedBy: duylv-27/08/2021
        /// </summary>
        public int StatusCode { get; set; }
        #endregion
    }
}
