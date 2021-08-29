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
            <div @click="btnCloseForm" class="icon icon-close"></div>
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
                    <input id="txtemployeeCode" type="text" class="input-feild" tabindex="1" />
                  </div>
                  <div class="txtfullName">
                    <label class="input-lable" for="txtfullName">
                      Tên
                      <span class="required">*</span>
                    </label>
                    <input id="txtfullName" type="text" class="col input-feild" tabindex="2" />
                  </div>
                </div>
                <div class="cbo-department">
                  <label class="input-lable" for="txtfullName">
                    Đơn vị
                    <span class="required">*</span>
                  </label>
                  <multiselect
                    class="custom-select-form"
                    :tabindex="3"
                    v-model="value"
                    :options="options"
                    :searchable="true"
                    :close-on-select="true"
                    :show-labels="false"
                    :multiple="false"
                    placeholder
                  >
                    <span slot="noResult">Không tìm thấy đơn vị</span>
                    <template slot="singleLabel" slot-scope="props">
                      <span>{{ props.option.DepartmentName }}</span>
                    </template>
                    <template slot="option" slot-scope="props">
                      <div class="option-item-name">
                        <span class="pr-20">{{ props.option.DepartmentCode }}</span>
                        <span class="pl-20">{{ props.option.DepartmentName }}</span>
                      </div>
                    </template>
                  </multiselect>
                </div>
                <div class="txtPosition">
                  <label class="input-lable" for="txtPosition">Chức danh</label>
                  <input id="txtPosition" type="text" class="input-feild" tabindex="4" />
                </div>
              </div>
              <div class="infor-right">
                <div class="row-input">
                  <div class="dtp-birthOfDate">
                    <label class="input-lable" for="txtPosition">Ngày sinh</label>
                    <input id="dtp-birthOfDate" type="date" class="input-feild" tabindex="5" />
                  </div>
                  <div class="rbtn-gender">
                    <label class="input-lable" for="rbtn-gender">Giới tính</label>
                    <div class="radio-button-group">
                      <div class="radio-button">
                        <input
                          type="radio"
                          id="gender-male"
                          name="radio-group"
                          checked
                          tabindex="6"
                        />
                        <label for="gender-male">Nam</label>
                      </div>
                      <div class="radio-button">
                        <input type="radio" id="gender-female" name="radio-group" tabindex="7" />
                        <label for="gender-female">Nữ</label>
                      </div>
                      <div class="radio-button">
                        <input type="radio" id="gender-order" name="radio-group" tabindex="8" />
                        <label for="gender-order">Khác</label>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input">
                  <div class="txtCMND">
                    <label class="input-lable" for="txtCMND">Số CMND</label>
                    <input id="txtCMND" type="text" class="input-feild" tabindex="9" />
                  </div>
                  <div class="dtpIdentityDate">
                    <label class="input-lable" for="dtpIdentityDate">Ngày cấp</label>
                    <input id="dtpIdentityDate" type="date" class="col input-feild" tabindex="10" />
                  </div>
                </div>
                <div class="txtIdentityplace">
                  <label class="input-lable" for="txtIdentityplace">Nơi cấp</label>
                  <input id="txtIdentityplace" type="text" class="input-feild" tabindex="11" />
                </div>
              </div>
            </div>
            <div class="contact">
              <div class="txtAddress">
                <label class="input-lable" for="txtAddress">Địa chỉ</label>
                <input id="txtAddress" type="text" class="input-feild" tabindex="12" />
              </div>
              <div class="row-input">
                <div class="txtMobilePhoneNumber contact-input">
                  <label class="input-lable" for="txtMobilePhoneNumber">ĐT di động</label>
                  <input id="txtMobilePhoneNumber" type="text" class="input-feild" tabindex="13" />
                </div>
                <div class="txtTelePhoneNumber contact-input">
                  <label class="input-lable" for="txtTelePhoneNumber">ĐT cố định</label>
                  <input id="txtTelePhoneNumber" type="text" class="input-feild" tabindex="14" />
                </div>
                <div class="txtEmail contact-input">
                  <label class="input-lable" for="txtEmail">Email</label>
                  <input id="txtEmail" type="text" class="input-feild" tabindex="15" />
                </div>
              </div>
              <div class="row-input">
                <div class="txtBankAccount contact-input">
                  <label class="input-lable" for="txtBankAccount">Tài khoản ngân hàng</label>
                  <input id="txtBankAccount" type="text" class="input-feild" tabindex="16" />
                </div>
                <div class="txtBankName contact-input">
                  <label class="input-lable" for="txtBankName">Tên ngân hàng</label>
                  <input id="txtBankName" type="text" class="input-feild" tabindex="17" />
                </div>
                <div class="txtBankBranch contact-input">
                  <label class="input-lable" for="txtBankBranch">Chi nhánh</label>
                  <input id="txtBankBranch" type="text" class="input-feild" tabindex="18" />
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
            <button class="button button-white mx-10">Cất</button>
            <button class="button">Cất và Thêm</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    formData: {
      type: Object,
      default: () => {}
    }
  },
  data() {
    return {
      value: "10 nhân viên/trang",
      options: [
        {
          DepartmentCode: "PB-001",
          DepartmentName: "CMC"
        },
        {
          DepartmentCode: "PB-002",
          DepartmentName: "MISA"
        },
        {
          DepartmentCode: "PB-003",
          DepartmentName: "FSOFT"
        },
        {
          DepartmentCode: "PB-004",
          DepartmentName: "VTV"
        }
      ],
      tabIndexForm: 1
    };
  },
  methods: {
    /**
     * Bắt sự kiện nút đóng form
     * CreatedBy: duylv 29/08/2021
     */
    btnCloseForm() {
      this.$emit("btnCloseForm");
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/base/FormDetail.css");
</style>