import Vue from 'vue'
import App from './App.vue'
import "@/assets/css/main.css";
import "@/assets/css/common/CustomSelectbox.css";

import "vue-multiselect/dist/vue-multiselect.min.css";
import 'devextreme/dist/css/dx.light.css';


import Multiselect from "vue-multiselect";
Vue.component('multiselect', Multiselect)
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
