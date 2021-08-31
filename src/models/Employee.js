class EmployeeDetail {
    constructor() {
    }

    employeeDetail = {
        CreatedDate: Date,
        CreatedBy: "",
        ModifiedDate: Date,
        ModifiedBy: "",
        EmployeeId: null,
        EmployeeCode: "",
        FullName: "",
        Gender: Number,
        DateOfBirth: Date,
        PhoneNumber: "",
        Email: "",
        Address: "",
        IdentityNumber: "",
        IdentityDate: Date,
        IdentityPlace: "",
        DepartmentId: null,
        DepartmentName: "",
        PositionName: "",
        DepartmentCode: "",
        BankAccount: "",
        BankName: "",
        BankBranch: "",
        TelePhoneNumber: "",
        MobilePhoneNumber: ""
    }

    employeeList = []
}

export default new EmployeeDetail()