import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'login',
    alias: ['/login'],   // 别名，可以定义很多个
    component: () => import('../login/login.vue')
  },
  {
    path: '/register',
    name: 'register',
    alias: ['/register'],   // 别名，可以定义很多个
    component: () => import('../regitster/register.vue')
  },
  {
    path: '/chat',
    name: 'chat',
    alias: ['/chat'],   // 别名，可以定义很多个
    component: () => import('../chat/chat.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
