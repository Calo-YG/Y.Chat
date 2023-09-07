import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import {createPinia} from 'pinia'
import 'element-plus/dist/index.css'
import App from './App.vue'


const store = createPinia()

const app = createApp(App)

app.use(store)

app.use(ElementPlus)

app.mount('#app')
