import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import {createPinia} from 'pinia'
import 'element-plus/dist/index.css'
import App from './App.vue'
import VueCookies from 'vue3-cookies'
import router from './Apps/routers'
import Antd from 'ant-design-vue';
import * as ElementPlusIconsVue from '@element-plus/icons-vue'

const store = createPinia()

const app = createApp(App)

app.use(VueCookies)

app.use(store)

app.use(ElementPlus)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
  }

app.use(Antd)

app.use(router)

app.mount('#app')
