﻿using MISA.TEST.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.TEST.API.Interfaces.Repository
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Phân trang và lọc nhân viên
        /// </summary>
        /// <param name="filterName">Lọc họ tên, mã nhân viên và số điện thoại</param>
        /// <param name="positionId">Lọc theo vị trí phòng ban</param>
        /// <param name="departmentId">Lọc theo phòng ban</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Vị trí của trang</param>
        /// <returns></returns>
        /// Created by: duylv - 18/08/2021
        object GetEmployeePaging(string filterName, Guid? departmentId, int pageSize, int pageIndex);

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        bool CheckDuplicate(Guid? employeeId, string employeeCode);

        /// <summary>
        /// Xóa danh sách thông tin khách hàng
        /// </summary>
        /// <param name="listId">danh sách id khách hàng</param>
        /// <returns></returns>
        /// Created by: duylv-14/8/2021
        int DeleteListId(List<Guid> listId);

        /// <summary>
        /// Lấy mã nhân viên mới nhất
        /// </summary>
        /// <returns>Trả ra mã nhân viên mới nhất</returns>
        /// Created by: duylv - 21/08/2021
        public string NewEmployeeCode();
    }
}
