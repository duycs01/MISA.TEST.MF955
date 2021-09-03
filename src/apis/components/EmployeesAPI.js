import BaseAPI from '../base/BaseAPI'
import baseAPIConfig from "../base/BaseAPIConfig"

class Employees extends BaseAPI {
    constructor() {
        super()
        this.controller = 'v1/employees'
    }

    /**
    * Hàm tìm kiếm và phân trang dữ liệu
    * CreatedBy: duylv 29/08/2021
    */
    filterData(filterName, departmentId, pageSize, pageIndex) {
        return baseAPIConfig.get(`${this.controller}/filter?filterName=${filterName}&departmentId=${departmentId}&pageSize=${pageSize}&pageIndex=${pageIndex}`)
    }

    /**
    * Hàm xóa danh sách nhân viên
    * CreatedBy: duylv 01/09/2021
    */
    deleteDataByList(deleteList) {
        return baseAPIConfig.post(`${this.controller}/deleteList`, deleteList)
    }

    /**
    * Hàm kiểm tra trùng mã nhân viên
    * CreatedBy: duylv 01/09/2021
    */
    checkDuplicate(body) {
        return baseAPIConfig.post(`${this.controller}/checkDuplicate`, body)
    }

    /**
    * Hàm lấy mã nhân viên mới
    * CreatedBy: duylv 01/09/2021
    */
    getNewEmployeeCode() {
        return baseAPIConfig.get(`${this.controller}/newEmployeeCode`)
    }

    /**
  * Hàm export dữ liệu
  * CreatedBy: duylv 01/09/2021
  */
    exportData(body) {
        return baseAPIConfig.post(`${this.controller}/Export`, body, {
            responseType: 'blob'
        })
    }
}
export default new Employees()