class Format {
    constructor() { }

    /**
     * Hàm sử lí định dạng giới tính
     * Create by: nvduy(30/8/2021)
     */
    formatGender(gender) {
        switch (gender) {
            case 0:
                return "Nam"
            case 1:
                return "Nữ"
            case 2:
                return "Khác"
            default:
                return "Khác"
        }
    }

    /**
    * Hàm sử lí định dạng ngày tháng
    * Create by: nvduy(30/7/2021)
    */
    formatDate(dateTime) {
        if (dateTime !== null) {
            var dateString = "";
            var newDate = new Date(dateTime);
            var date = newDate.getDay();
            var month = newDate.getMonth() + 1;
            var year = newDate.getFullYear();
            if (date < 10) {
                date = "0" + date;
            }
            if (month < 10) {
                month = "0" + month;
            }
            dateString = `${date}/${month}/${year}`;
            return dateString;
        }
    }
}
export default new Format()