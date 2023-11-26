import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import {createPinia} from 'pinia'
import 'element-plus/dist/index.css'
import App from './App.vue'
import VueCookies from 'vue3-cookies'
import router from './Apps/routers'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import VueVirtualScroller from 'vue-virtual-scroller'
import 'vue-virtual-scroller/dist/vue-virtual-scroller.css'

const pinia = createPinia()

const app = createApp(App)

app.use(VueCookies)

app.use(ElementPlus)

app.use(VueVirtualScroller)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
  }

app.use(pinia)

app.use(router)

app.mount('#app')
