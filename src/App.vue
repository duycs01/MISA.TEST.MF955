<template>
  <div id="app">
    <the-menu></the-menu>
    <the-content></the-content>
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
  }
};
</script>

<style>
#app{
  display: flex;
}
</style>
