using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MISA.TEST.Core.MISAAttribute.MISAAttribute;

namespace MISA.TEST.API.Entities
{
    public class Employee:BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        /// 
        [MISAExportProperty("Mã khách hàng")]
        [MISARequired]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Họ và tên")]
        [MISARequired]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Giới tính")]
        public int? Gender { get; set; }

        /// <summary>
        /// Địa chỉ
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Địa chỉ")]
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        /// 
        [MISAExportProperty("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("ĐT di động")]
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("ĐT cố định")]
        public string TelePhoneNumber { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân / căn cước
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Số CMND")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp Số chứng minh / căn cước
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Ngày cấp CMND")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp Số chứng minh / căn cước
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Nơi cấp CMND")]
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Chức vụ")]
        [MISANotMap]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Khóa ngoại của chức vụ
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Chức danh
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Chức danh")]
        public string PositionName { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Tài khoản ngân hàng")]
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Tên ngân hàng")]
        public string BankName { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// CreatedBy: duylv - 27/08/2021
        /// </summary>
        [MISAExportProperty("Chi nhánh ngân hàng")]
        public string BankBranch { get; set; }

        #endregion
    }
}
