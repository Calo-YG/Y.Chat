import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import {createPinia} from 'pinia'
import 'element-plus/dist/index.css'
import './style.css'
import App from './App.vue'
//import zhCn from 'element-plus/dist/locale/zhCn.mjs'



const store = createPinia()

const app = createApp(App)

app.use(store)

// app.use(ElementPlus, {
//     locale: zhCn,
//   })

app.use(ElementPlus)

app.mount('#app')
