import { createApp } from 'vue'
import App from './App.vue'
import UserLogin from "@/components/UserLogin.vue";

createApp(App).mount('#app')

const app = createApp(App);
app.component("UserLogin", UserLogin); // 全局註冊
app.mount("#app");