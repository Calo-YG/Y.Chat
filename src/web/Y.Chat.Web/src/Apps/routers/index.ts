import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/register',
    name: 'register',
    alias: ['/register'],   // 别名，可以定义很多个
    component: () => import('../regitster/register.vue')
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
