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
            <div class="icon icon-help" title="Giúp (F1)"></div>
            <div @click="btnExitForm" class="icon icon-close" title="Xóa (Esc)"></div>
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
                      @change="validateRequired($refs['EmployeeCode'],employeeDetail.EmployeeCode)"
                      ref="EmployeeCode"
                      maxlength="20"
                      v-model="employeeDetail.EmployeeCode"
                      id="txtemployeeCode"
                      type="text"
                      class="input-feild"
                      tabindex="1"
                      title="Mã nhân viên"
                      textVN="Mã nhân viên"
                    />
                  </div>
                  <div class="txtfullName">
                    <label class="input-lable" for="txtfullName">
                      Tên
                      <span class="required">*</span>
                    </label>
                    <input
                      @blur="validateRequired($refs['FullName'],employeeDetail.FullName)"
                      maxlength="100"
                      v-model="employeeDetail.FullName"
                      id="txtfullName"
                      type="text"
                      class="col input-feild"
                      tabindex="2"
                      ref="FullName"
                      title="Họ tên"
                      textVN="Họ tên"
                    />
                  </div>
                </div>
                <div class="cbo-department">
                  <label class="input-lable" for="txtfullName">
                    Đơn vị
                    <span class="required">*</span>
                  </label>
                  <div ref="Department" textVN="Đơn vị" title="Đơn vị">
                    <multiselect
                      class="custom-select-form"
                      @open="focusDropdown($refs['Department'])"
                      @close="validateRequired($refs['Department'],departmentDetail)"
                      @input="getDepartmentId(departmentDetail.DepartmentId)"
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
                </div>
                <div class="txtPosition">
                  <label class="input-lable" for="txtPosition">Chức danh</label>
                  <input
                    v-model="employeeDetail.PositionName"
                    id="txtPosition"
                    type="text"
                    class="input-feild"
                    tabindex="4"
                    textVN="Chức danh"
                  />
                </div>
              </div>
              <div class="infor-right">
                <div class="row-input">
                  <div class="dtp-birthOfDate">
                    <label class="input-lable" for="dtp-birthOfDate">Ngày sinh</label>

                    <DxDateBox
                      id="dtp-birthOfDate"
                      :value="formatDate(employeeDetail.DateOfBirth)"
                      :onValueChanged="getDateValue"
                      displayFormat="dd/MM/yyyy"
                      placeholder="DD/MM/YYYY"
                      :useMaskBehavior="true"
                      :showClearButton="false"
                      class="dxDateBox dtp-birthOfDate"
                      dateOutOfRangeMessage="Ngày chọn không được quá ngày hiện tại"
                      :max="new Date()"
                      :tabIndex="5"
                      title="Ngày sinh"
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
                          textVN="radio-group"
                          checked
                          tabindex="6"
                          title="Nam"
                        />
                        <label for="gender-male">Nam</label>
                      </div>
                      <div class="radio-button">
                        <input
                          v-model="employeeDetail.Gender"
                          value="1"
                          type="radio"
                          id="gender-female"
                          textVN="radio-group"
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
                          textVN="radio-group"
                          tabindex="8"
                          title="Khác"
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
                      title="Số CMND"
                    />
                  </div>
                  <div class="dtpIdentityDate">
                    <label class="input-lable" for="dtpIdentityDate">Ngày cấp</label>

                    <DxDateBox
                      id="dtpIdentityDate"
                      :value="formatDate(employeeDetail.IdentityDate)"
                      :onValueChanged="getDateValue"
                      displayFormat="dd/MM/yyyy"
                      placeholder="DD/MM/YYYY"
                      :useMaskBehavior="true"
                      :showClearButton="false"
                      class="dxDateBox dtpIdentityDate"
                      dateOutOfRangeMessage="Ngày chọn không được quá ngày hiện tại"
                      :max="new Date()"
                      :tabIndex="10"
                      title="Ngày cấp"
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
                    title="Nơi cấp"
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
                  title="Địa chỉ"
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
                    title="Số điện thoại di động"
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
                    title="Số điện thoại cố định"
                  />
                </div>
                <div class="txtEmail contact-input">
                  <label class="input-lable" for="txtEmail">Email</label>
                  <input
                    @change="validateEmail($event,employeeDetail.Email)"
                    v-model="employeeDetail.Email"
                    id="txtEmail"
                    type="text"
                    class="input-feild"
                    tabindex="15"
                    title="Địa chỉ Email"
                    textVN="Địa chỉ Email"
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
                    title="Tài khoản ngân hàng"
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
                    title="Tên ngân hàng"
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
                    title="Chi nhánh ngân hàng"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="form-footer">
          <div class="button-left">
            <button @click="btnCloseForm" tabindex="21" class="button button-white">Hủy</button>
          </div>
          <div class="button-right">
            <button
              @click="btnSaveFormClick"
              class="button button-white mx-10"
              title="Cất (Crtl + S)"
              tabindex="19"
            >Cất</button>
            <button
              @click="btnSaveAddClick"
              class="button"
              title="Cất và Thêm (Crtl + Shift + S)"
              tabindex="20"
            >Cất và Thêm</button>
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
    <transition textVN="slide-fade">
      <base-toast-mess v-if="showToastMess" :message="toastMess"></base-toast-mess>
    </transition>
  </div>
</template>
<script>
import DepartmentModel from "../../models/Department";
import moment from "moment";
import EmployeeAPI from "../../apis/components/EmployeesAPI";
import BasePopup from "../../components/base/BasePopup.vue";
import Validate from "../../util/Validate";
import EmployeeModel from "../../models/Employee";
import BaseToastMess from "../../components/base/BaseToastMess.vue";

import DxDateBox from "devextreme-vue/date-box";
import { locale } from "devextreme/localization";
locale("vi-VN");

export default {
  components: { BasePopup, BaseToastMess, DxDateBox },
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
      departmentDetail: DepartmentModel.departmentDetail,

      employeeDetail: {},
      tabIndexForm: 1,

      showPopupWarning: false,
      showPopupQuestion: false,
      popupDetail: {
        icon: "",
        description: ""
      },
      formModeSave: this.formMode,

      showToastMess: false,
      toastMess: { icon: "icon-success", mess: "Lấy dữ liệu thành công" },

      inputError: [],

      closeForm: false
    };
  },
  methods: {
    /**
     * Bắt sự kiện sử dụng phím mũi tên cho dropdown
     * CreatedBy: duylv 29/08/2021
     */
    onkeyup(event, departmentDetail) {
      if (departmentDetail != null) {
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
      } else {
        this.departmentDetail = this.departmentList[0];
      }
    },

    /**
     * Bắt sự kiện nút đóng form
     * CreatedBy: duylv 29/08/2021
     */
    btnCloseForm() {
      this.$emit("btnCloseForm");
      this.closePopupQuestion();
    },

    /**
     * Bắt sự kiện nút X đóng form
     * CreatedBy: duylv 29/08/2021
     */
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
      var attrId = e.element.id;
      console.log(e.element.id);
      if (attrId == "dtp-birthOfDate") {
        this.employeeDetail.DateOfBirth = this.formatDate(e.value);
        console.log(this.formatDate(e.value));
      }
      if (attrId == "dtpIdentityDate") {
        this.employeeDetail.IdentityDate = this.formatDate(e.value);
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
    getDepartmentId(id) {
      this.employeeDetail.DepartmentId = id;
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
     * Bắt sự kiện mở tôast
     * CreatedBy: duylv 31/08/2021
     */
    showToast() {
      this.showToastMess = true;
    },

    focusDropdown(ref) {
      if (ref.classList[0] != "border-focus") {
        ref.classList.add("border-focus");
      }
    },

    /**
     * validate các trường bắt buộc nhập
     * CreatedBy: duylv 01/09/2021
     */
    validateRequired(ref, value) {
      Validate.validateRequired(ref, value);
    },

    /**
     * Thực hiện kiểm tra dịnh dạng Email
     * CreatedBy: duylv 01/09/2021
     */
    validateEmail(event, value) {
      Validate.validateInputEmail(event, value);
    },

    /**
     * Thực hiện cất form
     * Create by: nvduy(31/8/2021)
     */
    saveForm() {
      // Kiểm tra tất cả input bắt buộc nhập
      let checkRequired = this.validateAll();

      if (checkRequired) {
        // Kiêm tra trùng mã nhân viên : true là trùng / false là không trùng
        // formModeSave: 1 Sửa / 0 Thêm
        if (this.formModeSave == 0) {
          EmployeeAPI.checkDuplicate(this.employeeDetail).then(
            duplicateCode => {
              if (!duplicateCode.data) {
                this.insertData();
              } else {
                // Hiển thị cảnh báo nếu mã nhân viên bị trùng
                this.popupDetail = {
                  mess: `Mã nhân viên <${this.employeeDetail.EmployeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
                  icon: "icon-error"
                };
                this.showPopupWarning = true;
              }
            }
          );
        }

        // Kiểm tra form đã thay đổi chưa : true là chưa thay đổi/ false là đã thay đổi
        var checkObj = Validate.checkObjEqual(
          this.formData,
          this.employeeDetail
        );
        // formModeSave: 1 Sửa / 0 Thêm
        if (this.formModeSave == 1) {
          if (!checkObj) {
            EmployeeAPI.checkDuplicate(this.employeeDetail).then(
              duplicateCode => {
                if (!duplicateCode.data) {
                  this.updateData();
                } else {
                  // Hiển thị cahr báo nếu mã nhân viên bị trùng
                  this.popupDetail = {
                    mess: `Mã nhân viên <${this.employeeDetail.EmployeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
                    icon: "icon-error"
                  };
                  this.showPopupWarning = true;
                  this.showPopupQuestion = false;
                }
              }
            );
          } else {
            if (this.closeForm) {
              this.btnCloseForm();
            } else {
              this.getNewEmployeeCode();
            }
          }
        }
      } else {
        // cho phép đóng form nếu là true / false là không đóng
        this.closeForm = false;
      }
    },

    /**
     * Hàm gọi api sửa nhân viên
     * Created by duylv 01/09/2021
     */
    updateData() {
      let id = this.employeeDetail.EmployeeId;
      EmployeeAPI.updateDataById(id, this.employeeDetail)
        .then(res => {
          this.$emit("openToastMess", res);
          this.$emit("reloadData");
          if (!this.closeForm) {
            this.getNewEmployeeCode();
            this.$refs.EmployeeCode.focus();
          }
          if (this.closeForm) {
            this.btnCloseForm();
          }
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopupWarning = true;
          this.closeForm = false;
        });
    },

    /**
     * Hàm gọi api thêm nhân viên
     * Created by duylv 01/09/2021
     */
    insertData() {
      console.log(this.employeeDetail);

      EmployeeAPI.insertData(this.employeeDetail)
        .then(res => {
          this.$emit("openToastMess", res);

          this.$emit("reloadData");

          if (!this.closeForm) {
            this.getNewEmployeeCode();
            this.$refs.EmployeeCode.focus();
          }

          if (this.closeForm) {
            this.btnCloseForm();
          }
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopupWarning = true;
          this.closeForm = false;
        });
    },

    /**
     * Kiểm tra các trường trước khi cất
     * Created by duylv 01/09/2021
     */
    validateAll() {
      let inputError = [];
      let checkRequired = true;
      let check;

      // Duyệt từng input để tìm lỗi
      for (let ref in this.$refs) {
        if (this.$refs[ref].nodeName == "DIV") {
          check = Validate.validateRequired(
            this.$refs[ref],
            this.employeeDetail.DepartmentId
          );
        } else {
          check = Validate.validateRequired(
            this.$refs[ref],
            this.$refs[ref].value
          );
        }

        if (!check) {
          inputError.push(ref);
          checkRequired = check;
        }
      }

      // Hiện popup cảnh báo nếu bị lỗi
      if (!checkRequired && inputError.length > 0) {
        this.popupDetail = {
          mess: this.$refs[inputError[0]].title,
          icon: "icon-error"
        };
        this.openPopupWarning();
        this.showPopupQuestion = false;
      }
      return checkRequired;
    },

    /**
     * Lấy mã nhân viên mới
     * Created by duylv 31/08/2021
     */
    getNewEmployeeCode() {
      EmployeeAPI.getNewEmployeeCode()
        .then(res => {
          if (res) {
            this.employeeDetail = { ...EmployeeModel.employeeDetail };
            this.formModeSave = 0;
            this.employeeDetail.EmployeeCode = res.data;
            this.departmentDetail = {};
          }
        })
        .catch(error => {
          this.popupDetail = Validate.checkStatus(error.response);
          this.showPopup = true;
        });
    },

    /**
     * Bắt sự kiện khi bấm vào nút cất và thêm
     * Create by: nvduy(31/8/2021)
     */
    btnSaveAddClick() {
      console.log(this.employeeDetail);
      this.closeForm = false;
      this.saveForm();
    },

    /**
     * Bắt sự kiện khi bấm vào nút cất
     * Create by: nvduy(31/8/2021)
     */
    btnSaveFormClick() {
      console.log(this.employeeDetail);
      this.closeForm = true;
      this.saveForm();
    }
  },
  mounted() {
    this.$refs.EmployeeCode.focus();
    this.departmentLis = DepartmentModel.departmentList;
    this.employeeDetail = { ...this.formData };
  },
  watch: {
    formData: {
      immediate: true,
      deep: true,
      handler() {
        console.log(this.formData);
        this.bindDepartment();
      }
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/base/FormDetail.css");
</style>