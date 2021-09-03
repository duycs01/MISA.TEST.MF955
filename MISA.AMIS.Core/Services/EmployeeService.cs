using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using MISA.TEST.API.Interfaces.Services;
using MISA.TEST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.TEST.API.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLEAR
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Contructor

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="employeeRepository">Kết nối database</param>
        /// CreateBy duylv - 27/08/2021
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Kiểm tra trùng theo mã, sđt, email
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <param name="input">mã, sđt, email</param>
        /// <returns>true là trùng, false là không trùng</returns>
        /// CreateBy duylv - 27/08/2021
        protected override bool ValidateCustom(Employee employee)
        {

            // Kiểm tra trùng mã
            if (employee.EmployeeCode != null)
            {
                var duplicate = _employeeRepository.CheckDuplicate(employee.EmployeeId, employee.EmployeeCode);
                if (_serviceResult.IsValid == true && duplicate == true)
                {
                    _serviceResult.Messenger = MISA.TEST.Core.Resources.Message.Duplicate_Employee;
                    _serviceResult.IsValid = false;
                }
            }

            if(employee.Email!="" && employee.Email != null)
            {
                var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool testEmail = Regex.IsMatch(employee.Email, regex, RegexOptions.IgnoreCase);
                if (_serviceResult.IsValid == true && testEmail == false)
                {
                    _serviceResult.Messenger = MISA.TEST.Core.Resources.Message.Error_Email;
                    _serviceResult.IsValid = false;
                }
            }
                return _serviceResult.IsValid;
        }
        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>Trả ra mã nhân viên mới nhất</returns>
        /// Created by: duylv - 21/08/2021
        public string NewEmployeeCode()
        {
            var employeeCode = _employeeRepository.NewEmployeeCode();

            var strVal = string.Empty;
            var numbVal = string.Empty;

            int numb = 0;

            for (int i = 0; i < employeeCode.Length; i++)
            {
                if (Char.IsDigit(employeeCode[i]))
                {
                    numbVal += employeeCode[i];
                }
                else
                {
                    strVal += employeeCode[i];
                }
            }

            if (numbVal.Length > 0)
                numb = Int32.Parse(numbVal);

            var res = $"{strVal}{numb}";

            do
            {
                numb ++;
                res = $"{strVal}{numb}";
            } while (_employeeRepository.CheckDuplicate(null, res));
            return res;
        }

        /// <summary>
        /// Xóa danh sách nhân viên
        /// </summary>
        /// <param name="listId">Danh sách id nhân viên</param>
        /// <returns>Trả ra số nhân viên được xóa</returns>
        /// Created by: duylv - 01/09/2021
        /// 
        public ServiceResult DeleteListId(List<Guid> listId)
        {
             var rowEffect = _employeeRepository.DeleteListId(listId);
            _serviceResult.Data = rowEffect;

            if (rowEffect > 0)
            {
                _serviceResult.IsValid = true;
            }
            else
            {
                _serviceResult.IsValid = false;
            }
            return _serviceResult;
        }


        public string FormatGender(int gender)
        {
            switch (gender)
            {
                case 0:
                    return "Nam";
                case 1:
                    return "Nữ";
                default:
                    return "Khác";
            }
        }
        #endregion
    }
}
