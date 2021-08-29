<template>
  <div class="content">
    <div class="content-header">
      <div class="header-left">
        <div class="toggle">
          <div class="icon icon-toggle-i"></div>
        </div>
        <div class="branch">
          <div class="branch-name">Công ty TNHH Sản Xuất - Thương Mại - Dịch Vụ QUI PHÚC</div>
          <div class="icon icon-arrow-bottom"></div>
        </div>
      </div>
      <div class="header-right">
        <div class="notification">
          <div class="icon icon-bell"></div>
        </div>
        <div class="account">
          <div class="avartar">
            <div class="icon icon-user"></div>
          </div>
          <div class="user-name">Lê Văn Duy</div>
          <div class="icon icon-arrow-bottom-small"></div>
        </div>
      </div>
    </div>
    <div class="content-body">
      
      <employee-list></employee-list>
    </div>
  </div>
</template>
<script>
import EmployeeList from "../../pages/employee/EmployeeList.vue";
export default {
  components: {
    EmployeeList
  }
};
</script>
<style scoped>
@import url("../../assets/css/layout/TheContent.css");
</style>