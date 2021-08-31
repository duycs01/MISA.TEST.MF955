<template>
  <div class="dialog">
    <div class="dialog-content">
      <form action="#" class="form-detail">
        <div class="form-header">
          <div class="form-title">
            <div class="title">Thông tin nhân viên</div>
            <div class="customer">
              <div class="check-box">
                <input type="checkbox" />
                <span class="checkmark"></span>
              </div>
              <label class="label">Là khách hàng</label>
            </div>
            <div class="supplier">
              <div class="check-box">
                <input type="checkbox" />
                <span class="checkmark"></span>
              </div>
              <label class="label">Là nhà cung cấp</label>
            </div>
          </div>
          <div class="form-close">
            <div class="icon icon-help"></div>
            <div @click="btnExitForm" class="icon icon-close"></div>
          </div>
        </div>
        <div class="form-body">
          <div class="form-content">
            <div class="row-input">
              <div class="infor-left">
                <div class="row-input">
                  <div class="txtemployeeCode">
                    <label class="input-lable" for="txtemployeeCode">
                      Mã
                      <span class="required">*</span>
                    </label>
                    <input
                      v-model="employeeDetail.EmployeeCode"
                      id="txtemployeeCode"
                      type="text"
                      class="input-feild"
                      tabindex="1"
                    />
                  </div>
                  <div class="txtfullName">
                    <label class="input-lable" for="txtfullName">
                      Tên
                      <span class="required">*</span>
                    </label>
                    <input
                      v-model="employeeDetail.FullName"
                      id="txtfullName"
                      type="text"
                      class="col input-feild"
                      tabindex="2"
                    />
                  </div>
                </div>
                <div class="cbo-department">
                  <label class="input-lable" for="txtfullName">
                    Đơn vị
                    <span class="required">*</span>
                  </label>
                  <multiselect
                    class="custom-select-form"
                    @input="getDepartmentId"
                    @keyup.native=" onkeyup($event, departmentDetail)"
                    :tabindex="3"
                    v-model="departmentDetail"
                    :options="departmentList"
                    :searchable="true"
                    :close-on-select="true"
                    :show-labels="false"
                    :multiple="false"
                    :allowEmpty="false"
                    :custom-label="searchLabel"
                    placeholder
                  >
                    <span slot="noResult">Không tìm thấy đơn vị</span>
                    <template slot="singleLabel" slot-scope="props">
                      <span>{{ props.option.DepartmentName }}</span>
                    </template>
                    <template slot="beforeList">
                      <div class="option-item-header">
                        <span class="px-10">Mã đơn vị</span>
                        <span class="px-10">Tên đơn vị</span>
                      </div>
                    </template>
                    <template slot="option" slot-scope="props">
                      <div class="option-item-name">
                        <span class="pr-20">{{ props.option.DepartmentCode }}</span>
                        <span class="pl-20 uppercase">{{ props.option.DepartmentName }}</span>
                      </div>
                    </template>
                  </multiselect>
                </div>
                <div class="txtPosition">
                  <label class="input-lable" for="txtPosition">Chức danh</label>
                  <input
                    v-model="employeeDetail.PositionName"
                    id="txtPosition"
                    type="text"
                    class="input-feild"
                    tabindex="4"
                  />
                </div>
              </div>
              <div class="infor-right">
                <div class="row-input">
                  <div class="dtp-birthOfDate">
                    <label class="input-lable" for="txtPosition">Ngày sinh</label>
                    <input
                      @change="getDateValue($event)"
                      :value="formatDate(employeeDetail.DateOfBirth)"
                      id="dtp-birthOfDate"
                      type="date"
                      class="input-feild"
                      tabindex="5"
                    />
                  </div>
                  <div class="rbtn-gender">
                    <label class="input-lable" for="rbtn-gender">Giới tính</label>
                    <div class="radio-button-group">
                      <div class="radio-button">
                        <input
                          v-model="employeeDetail.Gender"
                          value="0"
                          type="radio"
                          id="gender-male"
                          name="radio-group"
                          checked
                          tabindex="6"
                        />
                        <label for="gender-male">Nam</label>
                      </div>
                      <div class="radio-button">
                        <input
                          v-model="employeeDetail.Gender"
                          value="1"
                          type="radio"
                          id="gender-female"
                          name="radio-group"
                          tabindex="7"
                        />
                        <label for="gender-female">Nữ</label>
                      </div>
                      <div class="radio-button">
                        <input
                          v-model="employeeDetail.Gender"
                          value="2"
                          type="radio"
                          id="gender-order"
                          name="radio-group"
                          tabindex="8"
                        />
                        <label for="gender-order">Khác</label>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input">
                  <div class="txtCMND">
                    <label class="input-lable" for="txtCMND">Số CMND</label>
                    <input
                      v-model="employeeDetail.IdentityNumber"
                      id="txtCMND"
                      type="text"
                      class="input-feild"
                      tabindex="9"
                    />
                  </div>
                  <div class="dtpIdentityDate">
                    <label class="input-lable" for="dtpIdentityDate">Ngày cấp</label>
                    <input
                      @change="getDateValue($event)"
                      :value="formatDate(employeeDetail.IdentityDate)"
                      id="dtpIdentityDate"
                      type="date"
                      class="col input-feild"
                      tabindex="10"
                    />
                  </div>
                </div>
                <div class="txtIdentityplace">
                  <label class="input-lable" for="txtIdentityplace">Nơi cấp</label>
                  <input
                    v-model="employeeDetail.IdentityPlace"
                    id="txtIdentityplace"
                    type="text"
                    class="input-feild"
                    tabindex="11"
                  />
                </div>
              </div>
            </div>
            <div class="contact">
              <div class="txtAddress">
                <label class="input-lable" for="txtAddress">Địa chỉ</label>
                <input
                  v-model="employeeDetail.Address"
                  id="txtAddress"
                  type="text"
                  class="input-feild"
                  tabindex="12"
                />
              </div>
              <div class="row-input">
                <div class="txtMobilePhoneNumber contact-input">
                  <label class="input-lable" for="txtMobilePhoneNumber">ĐT di động</label>
                  <input
                    v-model="employeeDetail.MobilePhoneNumber"
                    id="txtMobilePhoneNumber"
                    type="text"
                    class="input-feild"
                    tabindex="13"
                  />
                </div>
                <div class="txtTelePhoneNumber contact-input">
                  <label class="input-lable" for="txtTelePhoneNumber">ĐT cố định</label>
                  <input
                    v-model="employeeDetail.TelePhoneNumber"
                    id="txtTelePhoneNumber"
                    type="text"
                    class="input-feild"
                    tabindex="14"
                  />
                </div>
                <div class="txtEmail contact-input">
                  <label class="input-lable" for="txtEmail">Email</label>
                  <input
                    v-model="employeeDetail.Email"
                    id="txtEmail"
                    type="text"
                    class="input-feild"
                    tabindex="15"
                  />
                </div>
              </div>
              <div class="row-input">
                <div class="txtBankAccount contact-input">
                  <label class="input-lable" for="txtBankAccount">Tài khoản ngân hàng</label>
                  <input
                    v-model="employeeDetail.BankAccount"
                    id="txtBankAccount"
                    type="text"
                    class="input-feild"
                    tabindex="16"
                  />
                </div>
                <div class="txtBankName contact-input">
                  <label class="input-lable" for="txtBankName">Tên ngân hàng</label>
                  <input
                    v-model="employeeDetail.BankName"
                    id="txtBankName"
                    type="text"
                    class="input-feild"
                    tabindex="17"
                  />
                </div>
                <div class="txtBankBranch contact-input">
                  <label class="input-lable" for="txtBankBranch">Chi nhánh</label>
                  <input
                    v-model="employeeDetail.BankBranch"
                    id="txtBankBranch"
                    type="text"
                    class="input-feild"
                    tabindex="18"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="form-footer">
          <div class="button-left">
            <button @click="btnCloseForm" class="button button-white">Hủy</button>
          </div>
          <div class="button-right">
            <button @click="btnSaveFormClick" class="button button-white mx-10">Cất</button>
            <button @click="btnSaveAddClick" class="button">Cất và Thêm</button>
          </div>
        </div>
      </form>
    </div>
    <base-popup v-if="showPopupWarning" :popupDetail="popupDetail">
      <template v-slot:buttonCenter>
        <button @click="closePopupWarning" class="button">Đóng</button>
      </template>
    </base-popup>
    <base-popup v-if="showPopupQuestion" :popupDetail="popupDetail">
      <template v-slot:buttonLeft>
        <button @click="closePopupQuestion" class="button button-white">Hủy</button>
      </template>
      <template v-slot:buttonRight>
        <button @click="btnCloseForm" class="button button-white mx-8">Không</button>
        <button @click="btnSaveFormClick" class="button">Có</button>
      </template>
    </base-popup>
  </div>
</template>
<script>
import DepartmentModel from "../../models/Department";
import moment from "moment";
import EmployeeAPI from "../../apis/components/EmployeesAPI";
import BasePopup from "../../components/base/BasePopup.vue";
import Validate from "../../util/Validate";
import EmployeeModel from "../../models/Employee";

export default {
  components: { BasePopup },
  props: {
    formData: {
      type: Object,
      default: () => {}
    },
    formMode: {
      type: Number,
      default: () => {}
    }
  },
  data() {
    return {
      departmentList: DepartmentModel.departmentList,
      departmentDetail: { ...DepartmentModel.departmentDetail },

      employeeDetail: { ...this.formData },
      tabIndexForm: 1,

      showPopupWarning: false,
      showPopupQuestion: false,
      popupDetail: {
        icon: "",
        description: ""
      }
    };
  },
  methods: {
    /**
     * Bắt sự kiện sử dụng phím mũi tên cho dropdown
     * CreatedBy: duylv 29/08/2021
     */
    onkeyup(event, departmentDetail) {
      let index = this.departmentList.findIndex(
        item => item.DepartmentCode == departmentDetail.DepartmentCode
      );
      console.log(event.key);
      if (event.key == "ArrowUp" && index != -1 && index > 0) {
        index--;
      } else if (
        event.key == "ArrowDown" &&
        index != -1 &&
        index < this.departmentList.length - 1
      ) {
        index++;
      }
      this.departmentDetail = this.departmentList[index];
    },

    /**
     * Bắt sự kiện nút đóng form
     * CreatedBy: duylv 29/08/2021
     */
    btnCloseForm() {
      this.$emit("btnCloseForm");
    },

    btnExitForm() {
      var checkObj = Validate.checkObjEqual(this.formData, this.employeeDetail);

      if (checkObj == false) {
        this.openPopupQuestion();
      } else {
        this.btnCloseForm();
      }
    },

    /**
     * Bắt sự kiện tìm kiếm theo tên và mã
     * CreatedBy: duylv 30/08/2021
     */
    searchLabel({ DepartmentCode, DepartmentName }) {
      return `${DepartmentCode} — [${DepartmentName}]`;
    },

    /**
     * Hàm sử lí định dạng ngày tháng
     * Create by: nvduy(30/8/2021)
     */
    formatDate(date) {
      if (date != null) {
        return moment(date).format("YYYY-MM-DD");
      }
      return null;
    },

    /**
     * Hàm gắn ngày tháng vào formData
     * Create by: nvduy(31/8/2021)
     */
    getDateValue(e) {
      var attrId = e.target.id;
      if (attrId == "dtp-DateOfBirth") {
        this.employeeDetail.DateOfBirth = e.srcElement.value;
      }
      if (attrId == "dtp-IdentityDate") {
        this.employeeDetail.IdentityDate = e.srcElement.value;
      }
    },

    /**
     * Lấy dữ liệu mặc định từ formData
     * Create by: nvduy(31/8/2021)
     */
    bindDepartment() {
      let index = this.departmentList.find(
        item => item.DepartmentId == this.formData.DepartmentId
      );
      console.log(index);
      this.departmentDetail = index ? index : null;
    },

    /**
     * Gắn departmentId vào formData
     * Create by: nvduy(31/8/2021)
     */
    getDepartmentId() {
      this.employeeDetail.DepartmentId = this.departmentDetail.DepartmentId;
    },

    /**
     * Bắt sự kiện đóng popup cảnh báo
     * CreatedBy: duylv 30/08/2021
     */
    closePopupWarning() {
      this.showPopupWarning = false;
    },

    /**
     * Bắt sự kiện đóng popup question
     * CreatedBy: duylv 30/08/2021
     */
    closePopupQuestion() {
      this.showPopupQuestion = false;
    },

    /**
     * Bắt sự kiện mở popup
     * CreatedBy: duylv 30/08/2021
     */
    openPopupWarning() {
      this.showPopupWarning = true;
    },

    /**
     * Bắt sự kiện mở popup
     * CreatedBy: duylv 30/08/2021
     */
    openPopupQuestion() {
      this.popupDetail = {
        mess: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
        icon: "icon-question"
      };
      this.showPopupQuestion = true;
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
     * Thực hiện cất form
     * Create by: nvduy(31/8/2021)
     */
    saveForm() {
      var checkSave = true;
      var checkObj = Validate.checkObjEqual(this.formData, this.employeeDetail);

      if (checkObj == false) {
        if (this.formMode == 0) {
          EmployeeAPI.insertData(this.employeeDetail)
            .then(res => {
              this.$emit("reloadData");
              this.toastMess = this.$statusCode.checkStatus(res);
              this.showPopupWarning = true;

              checkSave = true;
            })
            .catch(error => {
              this.popupDetail = this.Validate.checkStatus(error.response);
              this.showPopupWarning = true;
              checkSave = false;
            });
        }
        if (this.formMode == 1) {
          let id = this.employeeDetail.EmployeeId;

          EmployeeAPI.updateDataById(id, this.employeeDetail)
            .then(res => {
              this.$emit("reloadData");

              this.popupDetail = Validate.checkStatus(res);

              this.showPopupWarning = true;

              checkSave = true;
            })
            .catch(error => {
              this.popupDetail = Validate.checkStatus(error.response);
              this.showPopupWarning = true;
              checkSave = false;
            });
        }
      } else {
        this.popupDetail = {
          mess: "Bạn vấn chưa thay đổi gì trong form",
          icon: "icon-error"
        };
        this.openPopupWarning();
        checkSave = false;
      }
      return checkSave;
    },

    /**
     * Bắt sự kiện khi bấm vào nút cất và thêm
     * Create by: nvduy(31/8/2021)
     */
    btnSaveAddClick() {
      var checkSave = this.saveForm();
      if (checkSave) {
        this.employeeDetail = { ...EmployeeModel.employeeDetail };
        this.getNewEmployeeCode();
        this.formMode == 0;
      }
    },

    /**
     * Bắt sự kiện khi bấm vào nút cất
     * Create by: nvduy(31/8/2021)
     */
    btnSaveFormClick() {
      var checkSave = this.saveForm();
      if (checkSave) {
        this.btnCloseForm();
      }
    }
  },
  watch: {
    formData: {
      immediate: true,
      deep: true,
      handler() {
        this.bindDepartment();
      }
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/base/FormDetail.css");
</style>