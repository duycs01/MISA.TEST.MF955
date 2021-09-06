class Validate {
    constructor() {
    }

    /**
         * Kiểm tra 2 obj có bằng nhau k
         * CreadtedBy: 31/08/2021
         */
    checkObjEqual(object1, object2) {
        const keys1 = Object.keys(object1);
        const keys2 = Object.keys(object2);
        if (keys1.length !== keys2.length) {
            return false;
        }
        for (const key of keys1) {
            const val1 = object1[key];
            const val2 = object2[key];
            if (val1 != val2) {
                return false;
            }
        }
        return true;
    }

    /**
     * Kiểm tra resful trả về
     * CreadtedBy: 31/08/2021
     */
    checkStatus(res) {
        switch (res.status) {
            case 200:
                return { mess: res.data.Messenger, icon: "icon-success" };
            case 201:
                return { mess: res.data.Messenger, icon: "icon-success" };
            case 204:
                return { mess: "Không có dữ liệu", icon: "icon-success" };
            case 400:
                return { mess: res.data.Messenger, icon: "icon-error" };
            case 404:
                return { mess: "Không tìm thấy máy chủ! Vui lòng liên hệ MISA", icon: "icon-error" };
            case 500:
                return { mess: res.data.Data.userMsg, icon: "icon-error" };
            default:
                break;
        }
    }

    /**
     * Hàm kiểm tra định dạng email
     */
    validateEmail(email) {
        let re =
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (email != "") { return re.test(String(email).toLowerCase()); }
        else {
            return false
        }
    }

    /**
        * Thực hiện kiểm tra dịnh dạng Email
        * CreatedBy: duylv 01/09/2021
        */
    validateInputEmail(ref, value) {
        let isValid = this.validateEmail(value);
        if (!isValid && value != "") {
            ref.classList.add("border-err");
            ref.setAttribute("title", "Email không đúng định dạng");
        } else {
            ref.classList.remove("border-err");
            ref.setAttribute("title", 'Email');
        }
        return isValid
    }

    /**
   * validate các trường bắt buộc nhập
   * CreatedBy: duylv 01/09/2021
   */
    validateRequired(ref, value) {
        ref.classList.remove("border-focus");
        let isValid = true;
        let attr = ref.getAttribute("textVN");
        if (value == "" || value == null) {
            ref.classList.add("border-err");
            ref.title = attr + " không được để trống";
            isValid = false;
        } else {
            ref.classList.remove("border-err");
            ref.title = ref.name;
            isValid = true;
        }
        return isValid;
    }

}
export default new Validate()