class Department {
    constructor() {
    }

    departmentDetail = {
        CreatedDate: Date,
        CreatedBy: "",
        ModifiedDate: Date,
        ModifiedBy: "",
        DepartmentId: null,
        DepartmentCode: "",
        DepartmentName: "",
        Description: ""
    }

    departmentList = []
}

export default new Department()