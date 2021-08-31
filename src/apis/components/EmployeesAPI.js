import BaseAPI from '../base/BaseAPI'
import baseAPIConfig from "../base/BaseAPIConfig"

class Employees extends BaseAPI {
    constructor() {
        super()
        this.controller = 'v1/employees'
    }

    /**
    * Hàm tìm kiếm và phân trang dữ liệu
    */
    filterData(filterName, departmentId, pageSize, pageIndex) {
        return baseAPIConfig.get(`${this.controller}/filter?filterName=${filterName}&departmentId=${departmentId}&pageSize=${pageSize}&pageIndex=${pageIndex}`)
    }
}
export default new Employees()