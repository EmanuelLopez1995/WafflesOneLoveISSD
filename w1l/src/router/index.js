import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView/HomeView.vue'
import Empleados from '../views/Empleados/Empleados.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/empleados',
    name: 'empleados',
    component: Empleados
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
