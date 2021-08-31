<template>
  <div class="popup">
    <div class="popup-body">
      <div class="content-popup">
        <div :class="popupDetail.icon" class="icon"></div>
        <div class="text">{{popupDetail.mess}}</div>
      </div>
      <div class="popup-footer">
        <div class="button-left">
          <slot name="buttonLeft"></slot>
        </div>
        <div class="button-center">
          <slot name="buttonCenter"></slot>
        </div>
        <div class="button-right">
          <slot name="buttonRight"></slot>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    popupDetail: {
      type: Object,
      default: () => {}
    }
  }
};
</script>
<style scoped>
@import url("../../assets/css/base/Popup.css");
</style>