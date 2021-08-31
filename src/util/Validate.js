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
                return { mess: res.data.Messager, icon: "success" };
            case 201:
                return { mess: res.data.Messager, icon: "success" };
            case 204:
                return { mess: "Không có dữ liệu", icon: "icon-waring" };
            case 400:
                return { mess: res.data.Messager, icon: "icon-waring" };
            case 404:
                return { mess: "Không tìm thấy máy chủ! Vui lòng liên hệ MISA", icon: "icon-waring" };
            case 500:
                return { mess: "Sảy ra lỗi từ máy chủ! Vui lòng liên hệ MISA", icon: "icon-waring" };
            default:
                break;
        }
    }
}
export default new Validate()