<template>
  <div id="app">
    <the-menu @btnToggle="btnToggle" :showMenu="showMenu"></the-menu>
    <the-content @btnToggle="btnToggle" :showMenu="showMenu"></the-content>
  </div>
</template>

<script>
import TheMenu from "./components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";

export default {
  name: "App",
  components: {
    TheMenu,
    TheContent
  },
  data() {
    return {
      showMenu: true
    };
  },
  methods: {
    btnToggle() {
      this.showMenu = !this.showMenu;
    }
  }
};
</script>

<style>
#app{
  display: flex;
}
</style>
