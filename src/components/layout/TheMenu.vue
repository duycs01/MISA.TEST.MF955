<template>
  <div :class="{'close-menu': !showMenu}" class="menu">
    <div class="menu-header">
      <a v-if="!showMenu" @click="btnToggle" class="toggle icon icon-toggle-white" href="#"></a>
      <a v-if="showMenu" class="home icon icon-home" href="#"></a>
      <a v-if="showMenu" class="logo" href="#">
        <img src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="logo" />
      </a>
    </div>
    <div class="menu-body">
      <div v-for="(item,index) in menuItems" :key="item.title" class="menu-item">
        <a href="#" class="item" :class="{'active': index==0}">
          <div class="menu-icon">
            <div :class="index==0? item.activeIcon : item.classIcon" class="icon"></div>
          </div>
          <span v-if="showMenu" class="title">{{item.title}}</span>
        </a>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    showMenu: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      menuItems: [
        {
          title: "Tổng quan",
          classIcon: "icon-dashboard",
          activeIcon: "icon-dashboard-active"
        },
        {
          title: "Tiền mặt",
          classIcon: "icon-cash"
        },
        {
          title: "Tiền gửi",
          classIcon: "icon-deposits"
        },
        {
          title: "Mua hàng",
          classIcon: "icon-purchase"
        },
        {
          title: "Bán hàng",
          classIcon: "icon-sell"
        },
        {
          title: "Quản lí hóa đơn",
          classIcon: "icon-receipt"
        },
        {
          title: "Kho",
          classIcon: "icon-warehouse"
        },
        {
          title: "Công cụ dụng cụ",
          classIcon: "icon-tools"
        },
        {
          title: "Tài sản cố định",
          classIcon: "icon-fixed-assets"
        },
        {
          title: "Thuế",
          classIcon: "icon-tax"
        },
        {
          title: "Giá thành",
          classIcon: "icon-price"
        },
        {
          title: "Tổng hợp",
          classIcon: "icon-general"
        },
        {
          title: "Ngân sách",
          classIcon: "icon-budget"
        },
        {
          title: "Báo cáo",
          classIcon: "icon-report"
        },
        {
          title: "Phân tích tài chính",
          classIcon: "icon-finance"
        }
      ]
    };
  },

  methods: {
    /**
     * Hàm đóng mở memu
     * CreateBy: 02/09/2021
     */
    btnToggle() {
      // false là đóng menu
      this.$emit("btnToggle");
    }
  }
};
</script>

<style scoped>
@import url("../../assets/css/layout/TheMenu.css");
</style>