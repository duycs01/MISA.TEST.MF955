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
            v-model="filterName"
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
              <th class="col-action-th">Chức năng</th>
            </tr>
          </thead>
          <tbody v-if="employeeList">
            <tr v-for="item in employeeList" :key="item.EmployeeId">
              <td class="col-checkbox">
                <div class="check-box">
                  <input type="checkbox" />
                  <span class="checkmark"></span>
                </div>
              </td>
              <td class="col-employeeCode">{{item.EmployeeCode}}</td>
              <td class="col-fullName">{{item.FullName}}</td>
              <td>{{formatGender(item.Gender)}}</td>
              <td>{{formatDate(item.DateOfBirth)}}</td>
              <td>{{item.IdentityNumber}}</td>
              <td>{{item.PositionName}}</td>
              <td>{{item.DepartmentName}}</td>
              <td>{{item.BankAccount}}</td>
              <td>{{item.BankName}}</td>
              <td>{{item.BankBranch}}</td>
              <td class="col-action-td">
                <span @click="editForm(item.EmployeeId)" class="edit">Sửa</span>
                <span @click="showActionClick" class="action">
                  <span class="icon icon-arrow-bottom-blue"></span>
                </span>
              </td>
            </tr>
          </tbody>
        </table>
        <div v-show="showAction" class="action-option" ref="action">
          <div class="action-option-item">Nhân bản</div>
          <div class="action-option-item">Xóa</div>
          <div class="action-option-item">Ngưng sử dụng</div>
        </div>
      </div>
      <div class="pagination">
        <div class="total-record">
          Tổng số:
          <span class="font-bold">55</span> bản ghi
        </div>
        <div class="page-size">
          <multiselect
            @select="getEmployeeData"
            @close="getEmployeeData"
            @keyup.native=" onkeyup($event, pageSize)"
            v-model="pageSize"
            :options="optionsPageSize"
            :searchable="false"
            :close-on-select="false"
            :show-labels="false"
            :multiple="false"
            :allowEmpty="false"
          >
            <template slot="singleLabel" slot-scope="props">
              <span>{{ props.option.name }}</span>
            </template>
            <template slot="option" slot-scope="props">
              <span>{{ props.option.name }}</span>
            </template>
          </multiselect>
        </div>
        <div class="page-index">
          <base-paging
            :totalPages="totalPages"
            :total="totalRecord"
            :current-page="currentPage"
            @pagechanged="onPageChange"
          ></base-paging>
        </div>
      </div>
    </div>

    <employee-detail
      v-if="showFormDetail"
      @btnCloseForm="btnCloseFormClick"
      @reloadData="getEmployeeData"
      :formMode="formMode"
      :formData="employeeDetail"
    ></employee-detail>
    <base-popup v-if="showPopup" :popupDetail="popupDetail">
      <template v-slot:buttonLeft>
        <button @click="btnClosePopup" class="button button-white">Không</button>
      </template>
      <template v-slot:buttonRight>
        <button @click="btnClosePopup" class="button">Có</button>
      </template>
    </base-popup>
  </div>
</template>
<script>
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import moment from "moment";
import EmployeeDetail from "./EmployeeDetail.vue";
import BasePopup from "../../components/base/BasePopup.vue";
import BasePaging from "../../components/base/BasePaging.vue";
import EmployeeAPI from "../../apis/components/EmployeesAPI";
import DepartmentAPI from "../../apis/components/DepartmentsAPI";
import Format from "../../util/Format";
import EmployeeModel from "../../models/Employee";
import DepartmentModel from "../../models/Department";
import Validate from "../../util/Validate";

export default {
  components: { EmployeeDetail, BasePopup, BasePaging },

  data() {
    return {
      pageSize: { value: 10, name: "10 nhân viên/trang" },
      optionsPageSize: [
        { value: 10, name: "10 nhân viên/trang" },
        { value: 20, name: "20 nhân viên/trang" },
        { value: 30, name: "30 nhân viên/trang" },
        { value: 40, name: "40 nhân viên/trang" },
        { value: 50, name: "50 nhân viên/trang" }
      ],
      showFormDetail: false,

      popupDetail: {
        icon: "icon-warning",
        mess: "Bạn có muốn xóa"
      },
      showPopup: false,

      filterName: "",
      departmentId: "",
      employeeList: EmployeeModel.employeeList,
      departmentList: DepartmentModel.departmentList,

      employeeDetail: { ...EmployeeModel.employeeDetail },

      totalPages: 10,
      currentPage: 1,
      totalRecord: 100,
      showAction: false,

      formMode: 0
    };
  },
  methods: {
    /**
     * Lấy dữ liệu filter và phân trang nhân viên
     * CreatedBy: duylv 30/08/2021
     */
    getEmployeeData() {
      EmployeeAPI.filterData(
        this.filterName,
        this.departmentId,
        this.pageSize.value,
        this.currentPage
      )
        .then(res => {
          this.employeeList = res.data.Data;
          this.totalRecord = res.data.TotalRecord;
          this.totalPages = res.data.TotalPage;
          console.log(res);
        })
        .catch(err => {
          console.log(err);
        });
    },

    /**
     * Lấy dữ liệu filter và phân trang chức vụ
     * CreatedBy: duylv 30/08/2021
     */
    getDepartmentData() {
      DepartmentAPI.getAllData()
        .then(res => {
          DepartmentModel.departmentList = res.data;
       
        })
        .catch(err => {
          console.log(err);
        });
    },

    /**
     * Lấy mã nhân viên mới
     * Created by duylv 31/08/2021
     */
    getNewEmployeeCode() {
      var id = "NewEmployeeCode";
      EmployeeAPI.getDataById(id)
        .then(res => {
          this.employeeDetail.EmployeeCode = res.data;
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopup = true;
        });
    },

    /**
     * Bắt sự kiện khi bấm vào nút thêm mới
     * CreatedBy: duylv 28/08/2021
     */
    btnAddClick() {
      this.formMode = 0;
      this.employeeDetail = EmployeeModel.employeeDetail;
      this.getNewEmployeeCode();
      this.showFormDetail = true;
    },

    /**
     * Bắt sự kiện nút đóng form
     * CreatedBy: duylv 29/08/2021
     */
    btnCloseFormClick() {
      this.showFormDetail = false;
    },

    /**
     * Bắt sự kiện sử dụng phím mũi tên cho dropdown
     * CreatedBy: duylv 29/08/2021
     */
    onkeyup(event, pageSize) {
      let index = this.optionsPageSize.findIndex(
        item => item.value == pageSize.value
      );
      if (event.key == "ArrowUp" && index != -1 && index > 0) {
        index--;
      } else if (
        event.key == "ArrowDown" &&
        index != -1 &&
        index < this.optionsPageSize.length - 1
      ) {
        index++;
      }
      this.pageSize = this.optionsPageSize[index];
    },

    /**
     * Bắt sự kiện khi bấm vào nút đóng popup
     * CreatedBy: duylv 30/08/2021
     */
    btnClosePopup() {
      this.showPopup = false;
    },

    /**
     * Bắt sự kiện mở popup
     * CreatedBy: duylv 30/08/2021
     */
    showPopupMessage(mess) {
      this.popupDetail = mess;

      this.showPopup = true;
    },

    /**
     * Bắt sự kiện khi thay đổi vị trí page
     * CreatedBy: duylv 30/08/2021
     */
    onPageChange(page) {
      console.log(page);
      this.currentPage = page;
      this.getEmployeeData();
    },

    /**
     * Bắt sự kiện Hiển thị chức năng tại row
     * CreatedBy: duylv 30/08/2021
     */
    showActionClick(event) {
      let positionStyle = event.target.getBoundingClientRect();
      console.log(event.target.parentNode);
      this.showAction = true;
      if (this.showAction) {
        event.target.parentNode.classList.add("active");
        if (positionStyle.top >= 500) {
          this.$refs.action.style = `top: 445px;`;
        } else {
          this.$refs.action.style = `top:calc(${positionStyle.top}px + 25px);`;
        }
      } else {
        event.target.parentNode.classList.remove("active");
      }
    },

    /**
     * Hàm sử lí định dạng giới tính
     * Create by: nvduy(30/8/2021)
     */
    formatGender(gender) {
      return Format.formatGender(gender);
    },

    /**
     * Hàm sử lí định dạng ngày tháng
     * Create by: nvduy(30/7/2021)
     */
    formatDate(date) {
      if (date != null) {
        return moment(date).format("DD/MM/YYYY");
      }
      return null;
    },

    /**
     * Hàm mở form chình sửa dữ liệu
     * Create by: nvduy(30/7/2021)
     */
    editForm(id) {
      EmployeeAPI.getDataById(id)
        .then(res => {
          this.employeeDetail = res.data;
          this.formMode = 1;
          this.showFormDetail = true;

          console.log(this.employeeDetail);
        })
        .catch(err => {
          console.log(err);
        });
    }
  },

  created() {
    this.getEmployeeData();
    this.getDepartmentData();
  }
};
</script>
<style scoped>
@import url("../../assets/css/page/Employee.css");

</style>