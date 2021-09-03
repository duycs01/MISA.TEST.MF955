<template>
  <div class="employee">
    <div class="title-distance">
      <div class="title">Nhân viên</div>
      <button @click="btnAddClick()" class="button btn-add">Thêm mới nhân viên</button>
    </div>
    <div class="grid-data">
      <div class="filter">
        <div>
          <button
            @click="openPopupDelete(0)"
            v-show="deleteList.length > 1"
            class="button button-white"
          >Xóa hàng loạt</button>
        </div>
        <div class="action">
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
          <div @click="getEmployeeData" class="icon icon-refresh"></div>
          <div @click="exportEmployeeData" class="icon icon-export-excel"></div>
        </div>
      </div>
      <div class="table-view">
        <table>
          <thead>
            <tr class>
              <th class="col-checkbox">
                <div class="check-box">
                  <input type="checkbox" :checked="checkDeleteList" @change="inputCheckAll" />
                  <span class="checkmark"></span>
                </div>
              </th>
              <th class="col-employeeCode">Mã nhân viên</th>
              <th class="col-fullName">Tên nhân viên</th>
              <th>Giới tính</th>
              <th class="text-center">Ngày sinh</th>
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
            <tr
              @dblclick="editForm(item.EmployeeId)"
              v-for="item in employeeList"
              :key="item.EmployeeId"
            >
              <td class="col-checkbox">
                <div class="check-box">
                  <input
                    type="checkbox"
                    :checked="deleteList.indexOf(item.EmployeeId) !== -1? true: false"
                    @change="inputChecked(item.EmployeeId)"
                  />
                  <span class="checkmark"></span>
                </div>
              </td>
              <td class="col-employeeCode">{{item.EmployeeCode}}</td>
              <td class="col-fullName">{{item.FullName}}</td>
              <td>{{formatGender(item.Gender)}}</td>
              <td class="text-center">{{formatDate(item.DateOfBirth)}}</td>
              <td>{{item.IdentityNumber}}</td>
              <td>{{item.PositionName}}</td>
              <td>{{item.DepartmentName}}</td>
              <td>{{item.BankAccount}}</td>
              <td>{{item.BankName}}</td>
              <td>{{item.BankBranch}}</td>
              <td class="col-action-td">
                <span @click="editForm(item.EmployeeId)" class="edit">Sửa</span>
                <span
                  :class="{'active': employeeId == item.EmployeeId}"
                  @click="showActionClick($event,item)"
                  class="action"
                >
                  <span class="icon icon-arrow-bottom-blue"></span>
                </span>
              </td>
            </tr>
          </tbody>
        </table>
        <div v-show="employeeId" class="action-option" ref="action">
          <div @click="replication()" class="action-option-item">Nhân bản</div>
          <div @click="openPopupDelete(1)" class="action-option-item">Xóa</div>
          <div class="action-option-item">Ngưng sử dụng</div>
        </div>
      </div>
      <div class="pagination">
        <div class="total-record">
          Tổng số:
          <span class="font-bold">{{totalRecord}}</span> bản ghi
        </div>
        <div class="page-size">
          <multiselect
            @open="focusDropdown = true"
            @close="focusDropdown = false"
            :class="{'border-focus': focusDropdown}"
            @input="loadData(1)"
            @keyup.native=" onkeyup($event, pageSize)"
            v-model="pageSize"
            :options="optionsPageSize"
            :searchable="false"
            :close-on-select="true"
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
            @pagechanged="loadData"
          ></base-paging>
        </div>
      </div>
    </div>
    <employee-detail
      @openToastMess="openToastMess"
      v-if="showFormDetail"
      @btnCloseForm="btnCloseFormClick"
      @reloadData="loadData(1)"
      :formMode="formMode"
      :formData="employeeDetail"
    ></employee-detail>
    <base-popup v-if="showPopupDelete" :popupDetail="popupDetail">
      <template v-slot:buttonLeft>
        <button @click="showPopupDelete=false" class="button button-white">Không</button>
      </template>
      <template v-slot:buttonRight>
        <button @click="btnPopupDelete" class="button">Có</button>
      </template>
    </base-popup>
    <base-popup v-if="showPopupWarning" :popupDetail="popupDetail">
      <template v-slot:buttonCenter>
        <button @click="btnClosePopupWarning" class="button">Đóng</button>
      </template>
    </base-popup>
    <transition name="slide-fade">
      <base-toast-mess v-if="showToastMess" :message="toastMess"></base-toast-mess>
    </transition>
    <base-loading v-if="loading"></base-loading>
  </div>
</template>
<script>
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
import BaseToastMess from "../../components/base/BaseToastMess.vue";
import BaseLoading from "../../components/base/BaseLoading.vue";

export default {
  components: {
    EmployeeDetail,
    BasePopup,
    BasePaging,
    BaseToastMess,
    BaseLoading
  },
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

      showPopupDelete: false,
      showPopupWarning: false,

      filterName: "",
      departmentId: "",
      employeeList: EmployeeModel.employeeList,
      departmentList: DepartmentModel.departmentList,

      employeeDetail: { ...EmployeeModel.employeeDetail },
      employeeId: "",

      totalPages: 10,
      currentPage: 1,
      totalRecord: 100,
      showAction: false,

      formMode: 0,
      deleteMode: 1,
      deleteList: [],

      checked: false,

      showToastMess: false,
      toastMess: { icon: "icon-success", mess: "Lấy dữ liệu thành công" },

      loading: false,

      timeout: null,

      focusDropdown: false
    };
  },
  methods: {
    /**
     * Lấy dữ liệu filter và phân trang nhân viên
     * CreatedBy: duylv 30/08/2021
     */
    getEmployeeData() {
      this.loading = true;
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
          this.loading = false;
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopup = true;
          this.loading = false;
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
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopup = true;
        });
    },

    /**
     * Lấy mã nhân viên mới
     * Created by duylv 31/08/2021
     */
    getNewEmployeeCode() {
      const me = this;
      EmployeeAPI.getNewEmployeeCode()
        .then(res => {
          me.employeeDetail.EmployeeCode = res.data;
          me.showFormDetail = true;
        })
        .catch(error => {
          me.popupDetail = Validate.checkStatus(error.response);
          me.showPopup = true;
        });
    },

    /**
     * Bắt sự kiện export nhân viên
     * CreatedBy: duylv 28/08/2021
     */
    exportEmployeeData() {
      EmployeeAPI.exportData(this.employeeList)
        .then(res => {
          const blob = new Blob([res.data], {
            type:
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
          });
          let tagA = document.createElement("a");
          tagA.href = URL.createObjectURL(blob);
          tagA.download = "Danh sach";
          tagA.click();
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
      this.employeeDetail = { ...EmployeeModel.employeeDetail };
      this.formMode = 0;
      this.getNewEmployeeCode();
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
     * Bắt sự kiện khi bấm vào nút đóng popup cảnh báo
     * CreatedBy: duylv 30/08/2021
     */
    btnClosePopupWarning() {
      this.showPopupWarning = false;
    },

    /**
     * Bắt sự kiện mở popup cảnh báo
     * CreatedBy: duylv 30/08/2021
     */
    openPopupWarning(mess) {
      this.popupDetail = mess;
      this.showPopupWarning = true;
    },

    /**
     * Bắt sự kiện mở popup delete
     * CreatedBy: duylv 31/08/2021
     */
    openPopupDelete(mode) {
      this.deleteMode = mode; // mode = 1 xóa 1 nhân viên && mode = 0 xóa nhiều nhân viên
      if (this.deleteMode == 1) {
        this.popupDetail = {
          mess: `Bạn có chắc chắn muốn xóa nhân viên [${this.employeeDetail.EmployeeCode}] không?`,
          icon: "icon-warning"
        };
      } else {
        this.popupDetail = {
          mess: `Bạn có chắc chắn muốn xóa các nhân viên này không?`,
          icon: "icon-warning"
        };
      }
      this.showPopupDelete = true;
      this.showAction = false;
      this.employeeId = null;
    },

    /**
     * Bắt sự kiện mở toast message
     * CreatedBy: duylv 31/08/2021
     */
    openToastMess(mess) {
      this.toastMess = Validate.checkStatus(mess);
      this.showToastMess = true;

      setTimeout(() => {
        this.showToastMess = false;
      }, 2000);
    },

    /**
     * Thực hiện xóa khi bấm vào nút xóa ở popup
     * CreatedBy: duylv 01/09/2021
     */
    btnPopupDelete() {
      // mode = 1 xóa 1 nhân viên && mode = 0 xóa nhiều nhân viên
      if (this.deleteMode == 1) {
        let id = this.employeeDetail.EmployeeId;
        EmployeeAPI.deleteDataById(id)
          .then(res => {
            this.openToastMess(res);
            this.loadData(1);
          })
          .catch(error => {
            this.popupDetail = Validate.checkStatus(error.response);
            this.showPopupWarning = true;
          });
      } else {
        EmployeeAPI.deleteDataByList(this.deleteList)
          .then(res => {
            this.openToastMess(res);
            this.checked = false;
            this.deleteList = [];

            this.loadData(1);
          })
          .catch(error => {
            this.popupDetail = Validate.checkStatus(error.response);
            this.showPopupWarning = true;
          });
      }
      this.showPopupDelete = false;

      this.loadData(1);
    },

    /**
     * Lấy toàn bộ id khi check
     * CreatedBy: duylv 01/09/2021
     */
    inputCheckAll() {
      this.checked = !this.checked;
      console.log(this.deleteList);
      this.employeeList.forEach(item => {
        if (this.checked == true) {
          this.deleteList.push(item.EmployeeId);
        } else {
          this.deleteList = [];
        }
      });
    },

    /**
     * Lấy id những hàng đang check
     * CreatedBy: duylv 01/09/2021
     */
    inputChecked(id) {
      console.log(this.deleteList);
      let rowIndex = this.deleteList.indexOf(id);
      if (rowIndex !== -1) {
        this.deleteList.splice(rowIndex, 1);
      } else {
        this.deleteList.push(id);
      }
    },

    /**
     * Bắt sự kiện khi thay đổi vị trí page
     * CreatedBy: duylv 30/08/2021
     */
    loadData(page) {
      console.log(page);
      this.currentPage = page;
      this.getEmployeeData();
    },

    hideActionClick() {
      this.showAction = false;
    },

    /**
     * Bắt sự kiện Hiển thị chức năng tại row
     * CreatedBy: duylv 30/08/2021
     */
    showActionClick(event, detail) {
      if (this.employeeId == detail.EmployeeId) {
        this.showAction = false;
        this.employeeId = null;
      } else {
        this.employeeId = detail.EmployeeId;
        this.showAction = true;
        let positionStyle = event.target.getBoundingClientRect();
        this.employeeDetail = detail;
        console.log(event.target.parentNode);
        if (this.showAction) {
          if (positionStyle.top >= 500) {
            this.$refs.action.style = `top: 445px;`;
          } else {
            this.$refs.action.style = `top:calc(${positionStyle.top}px + 25px);`;
          }
        }
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
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopupWarning = true;
        });
    },

    /**
     * Nhân bản nhân viên
     * Create by: nvduy(30/7/2021)
     */
    replication() {
      let id = this.employeeDetail.EmployeeId;
      EmployeeAPI.getDataById(id)
        .then(res => {
          this.employeeDetail = res.data;
          this.formMode = 0;
          this.getNewEmployeeCode();
          this.showAction = false;
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopupWarning = true;
        });
      this.employeeId = null;
    }
  },
  computed: {
    checkDeleteList() {
      let check = this.checked;
      if (this.deleteList.length == this.employeeList.length) {
        check = true;
      } else {
        check = false;
      }

      return check;
    }
  },
  mounted() {
    this.loadData(1);

    this.getDepartmentData();
  },

  watch: {
    filterName() {
      clearTimeout(this.timeout);
      this.timeout = setTimeout(() => {
        this.loadData(1);
      }, 500);
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/page/Employee.css");

</style>