<template>
  <div class="toast-mess">
    <div class="content-mess">
      <div :class="message.icon" class="icon"></div>
      <div class="text">{{message.mess}}</div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    message: { type: Object, require }
  },
  components: {}
};
</script>
<style scoped>
@import url("../../assets/css/base/ToastMess.css");
</style>