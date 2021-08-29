<template>
  <div class="employee">
    <div class="title-distance">
      <div class="title">Nhân viên</div>
      <button @click="btnAddClick()" class="button btn-add">Thêm mới nhân viên</button>
    </div>
    <div class="grid-data">
      <div class="filter">
        <div class="input-feild-group">
          <input
            type="text"
            class="input-feild input-feild-group-text"
            placeholder="Tìm theo mã, tên nhân viên"
          />
          <div class="input-feild-group-icon">
            <div class="icon icon-search"></div>
          </div>
        </div>
        <div class="icon icon-refresh"></div>
        <div class="icon icon-export-excel"></div>
      </div>
      <div class="table-view">
        <table>
          <thead>
            <tr class>
              <th class="col-checkbox">
                <div class="check-box">
                  <input type="checkbox" />
                  <span class="checkmark"></span>
                </div>
              </th>
              <th class="col-employeeCode">Mã nhân viên</th>
              <th class="col-fullName">Tên nhân viên</th>
              <th>Giới tính</th>
              <th>Ngày sinh</th>
              <th>Số CMND</th>
              <th>Chức danhh</th>
              <th>Tên đơn vị</th>
              <th>Số tài khoản</th>
              <th>Tên ngân hàng</th>
              <th>Chi nhánh ngân hàng</th>
              <th class="col-control">Chức năng</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in 10" :key="item">
              <td class="col-checkbox">
                <div class="check-box">
                  <input type="checkbox" />
                  <span class="checkmark"></span>
                </div>
              </td>
              <td class="col-employeeCode">Mã nhân viên</td>
              <td class="col-fullName">Tên nhân viên</td>
              <td>Giới tính</td>
              <td>Ngày sinh</td>
              <td>Số CMND</td>
              <td>Chức danhh</td>
              <td>Tên đơn vị</td>
              <td>Số tài khoản</td>
              <td>Tên ngân hàng</td>
              <td>Chi nhánh ngân hàng</td>
              <td class="col-control">Chức năng</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="pagination">
        <div class="total-record"></div>
        <div class="page-size">
          <multiselect
            @keyup.native=" onkeyup($event, value)"
            v-model="value"
            :options="options"
            :searchable="false"
            :close-on-select="false"
            :show-labels="false"
          ></multiselect>
        </div>
        <div class="page-index">al;o</div>
      </div>
    </div>
    <employee-detail v-if="showFormDetail" @btnCloseForm="btnCloseFormClick"></employee-detail>
  </div>
</template>
<script>
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

import EmployeeDetail from "./EmployeeDetail.vue";

export default {
  components: { EmployeeDetail },

  data() {
    return {
      value: "10 nhân viên/trang",
      options: [
        "10 nhân viên/trang",
        "20 nhân viên/trang",
        "30 nhân viên/trang"
      ],
      showFormDetail: false
    };
  },
  methods: {
    /**
     * Bắt sự kiện khi bấm vào nút thêm mới
     * CreatedBy: duylv 28/08/2021
     */
    btnAddClick() {
      this.showFormDetail = true;
    },

    /**
     * Bắt sự kiện sử dụng phím mũi tên cho dropdown
     * CreatedBy: duylv 29/08/2021
     */
    onkeyup(event, value) {
      let index = this.options.indexOf(value);
      console.log(index);
      if (event.key == "ArrowUp" && index != -1 && index > 0) {
        index--;
      } else if (
        event.key == "ArrowDown" &&
        index != -1 &&
        index < this.options.length - 1
      ) {
        index++;
      }
      this.value = this.options[index];
    },

    /**
     * Bắt sự kiện nút đóng form
     * CreatedBy: duylv 29/08/2021
     */
    btnCloseFormClick() {
      this.showFormDetail = false;
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/page/Employee.css");
button#dropdown-1__BV_toggle_ {
    background: transparent;
    color: #111;
    border: none;
    box-shadow: none;
    outline: none;
    font-size: 13px !important;
  font-family: MisaNotoSans-Regular !important;

}
.dropdown-toggle::after{
  top:15px;
  right: 15px;
}

</style>