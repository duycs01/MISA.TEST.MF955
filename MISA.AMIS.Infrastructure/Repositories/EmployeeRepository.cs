using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.TEST.API.Entities;
using MISA.TEST.API.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>,IEmployeeRepository
    {
        #region Contructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
        #region Method
        /// <summary>
        /// Thực hiện lọc và phân trang
        /// </summary>
        /// <param name="filterName">Tìm kiếm theo tên mã và số điện thoại</param>
        /// <param name="departmentId">Tìm kếm theo phòng ban</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns>Tổng số trang, tổng số bản ghi,  vad data</returns>
        /// CreateBy duylv - 27/08/2021
        public object GetEmployeePaging(string filterName, Guid? departmentId, int pageSize, int pageIndex)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            var input = filterName == null ? string.Empty : filterName;
            dynamicParameters.Add("@EmployeeFilter", input);
            dynamicParameters.Add("@DepartmentId", departmentId);

            dynamicParameters.Add("@PageIndex", pageIndex);
            dynamicParameters.Add("@PageSize", pageSize);
            dynamicParameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


            var resPaging = _dbConnection.Query("Proc_GetEmployeeFilterPaging", dynamicParameters, commandType: CommandType.StoredProcedure);


            var objData = new
            {
                Data = resPaging.ToList(),
                TotalPage = dynamicParameters.Get<Int32>("@TotalPage"),
                TotalRecord = dynamicParameters.Get<Int32>("@TotalRecord")
            };

            return objData;
        }

        /// <summary>
        /// Xóa danh sách nhân viên
        /// </summary>
        /// <param name="listId">danh sách nhân viên</param>
        /// <returns>Số hàng xóa được</returns>
        /// CreateBy duylv - 27/08/2021
        public int DeleteListId(List<Guid> listId)
        {
            var transaction = _dbConnection.BeginTransaction();
            DynamicParameters parameters = new DynamicParameters();
            var rowEffects = 0;
            foreach (var item in listId)
            {
                parameters.Add("@EmployeeId", item.ToString());
                rowEffects += _dbConnection.Execute($"Proc_DeleteEmployee", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            transaction.Commit();
            return rowEffects;
        }

        /// <summary>
        /// Kiểm tra trùng theo mã, sđt, email
        /// </summary>
        /// <param name="employeeId">Id nhân viên nếu là sửa - null sẽ là thêm</param>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true là trùng, false là không trùng</returns>
        /// CreateBy duylv - 27/08/2021
        public bool CheckDuplicate(Guid? employeeId, string employeeCode)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@EmployeeCode", employeeCode);

            // Lấy ra nhân viên trùng mã
            var sqlCommand = "SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode LIMIT 1";

            // Trả ra nhân viên có mã trùng
            var res = _dbConnection.QueryFirstOrDefault<Employee>(sqlCommand, dynamicParameters);

            // Nếu có dữ liệu trả về thì kiểm tra xem có trùng với id đang sửa không
            if (res != null && res.EmployeeId != employeeId)
            {
                // dữ liệu trả về có id không trùng với id đang sửa(hoặc thêm) thì mã nhân viên đã bị trùng - true
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy mã lớn nhất
        /// </summary>
        /// <returns>Trả về mã lớn nhất</returns>
        /// CreateBy duylv - 27/08/2021
        public string NewEmployeeCode()
        {
            var sqlCommand = "SELECT EmployeeCode FROM Employee e ORDER BY CHAR_LENGTH(e.EmployeeCode) DESC, e.EmployeeCode DESC LIMIT 1";
            var res = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);
            return res;
        }
        #endregion
    }
}
