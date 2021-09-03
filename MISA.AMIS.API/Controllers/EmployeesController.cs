using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using MISA.TEST.API.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MISA.TEST.Core.MISAAttribute.MISAAttribute;


namespace MISA.TEST.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLEAR
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeService, employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion
        #region Method
        /// <summary>
        /// Phân trang và lọc nhân viên
        /// </summary>
        /// <param name="filterName">Lọc họ tên, mã nhân viên và số điện thoại</param>
        /// <param name="positionId">Lọc theo vị trí phòng ban</param>
        /// <param name="departmentId">Lọc theo phòng ban</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns>Tổng số bản ghi, tổng sổ trang, danh sách bản ghi đã lọc và phân trang</returns>
        /// Created by: duylv - 18/08/2021
        [HttpGet("filter")]
        public IActionResult GetEmployeePaging([FromQuery] string filterName, [FromQuery] Guid? departmentId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {


                var res = _employeeRepository.GetEmployeePaging(filterName, departmentId, pageSize, pageIndex);
                if (res != null)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {

                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.TEST.Core.Resources.Message.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }

        }

        /// <summary>
        /// Xóa danh sách bản ghi theo id
        /// </summary>
        /// <param name="listId">danh sách id</param>
        /// <returns>Số lượng bản ghi xóa được</returns>
        /// Created by: duylv - 17/08/2021
        [HttpPost("deleteList")]
        public IActionResult DeleteListId([FromBody] List<Guid> listId)
        {
            try
            {
                var res = _employeeService.DeleteListId(listId);

                if (res.IsValid)
                {
                    res.Messenger = MISA.TEST.Core.Resources.Message.Delete_Success;

                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {
                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.TEST.Core.Resources.Message.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: duylv - 22/08/2021
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var res = _employeeService.NewEmployeeCode();

                if (res != null)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(204, res);
                }
            }
            catch (Exception ex)
            {

                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.TEST.Core.Resources.Message.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }
        }

        [HttpPost("checkDuplicate")]
        public IActionResult CheckDuplicateEmployeeCode([FromBody] Employee employee)
        {
            try
            {
                var res = _employeeRepository.CheckDuplicate(employee.EmployeeId, employee.EmployeeCode);


                return StatusCode(200, res);

            }
            catch (Exception ex)
            {

                var errObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.TEST.Core.Resources.Message.Exception_ErrorMsg,
                };
                _serviceResult.Data = errObj;
                return StatusCode(500, _serviceResult);
            }
        }

        [HttpPost("Export")]
        public async Task<IActionResult> ExportData(CancellationToken cancellationToken, [FromBody] List<Employee> employees)
        {
            // query data from database  
            await Task.Yield();
            var list = employees;

            var properties = typeof(Employee).GetProperties();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                var column = 1;

                foreach (var prop in properties)
                {
                    var propMISAExport = prop.GetCustomAttributes(typeof(MISAExportProperty), true);


                    workSheet.Cells.AutoFitColumns();

                    if (propMISAExport.Length > 0)
                    {
                        var fieldName = (propMISAExport[0] as MISAExportProperty).FieldName;
                        workSheet.Cells[1, column].Value = fieldName;
                        workSheet.Cells[1, column].Style.Font.Bold = true;

                        workSheet.Cells[1, column].Style.Fill.SetBackground(Color.LightGray);

                    }

                  
                    // xet cac truong export hay ko?
                    if (!(propMISAExport.Length == 1))
                    {
                        workSheet.Column(column).Hidden = true;
                    }

                    // dinh dang ngay thang nam
                    if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                    {
                        workSheet.Column(column).Style.Numberformat.Format = "mm/dd/yyyy";
                    }

                    column++;
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        #endregion
    }
}
