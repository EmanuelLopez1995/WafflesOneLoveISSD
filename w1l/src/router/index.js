import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView/HomeView.vue'
import store from '@/store';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/empleados',
    name: 'empleados',
    component: () => import ('@/views/Empleados/Empleados.vue')
  },
  {
    path: '/login',
    name: 'login',
    component: () => import ('@/views/Login/Login.vue')
  },
  {
    path: '/stock',
    name: 'stock',
    component: () => import ('@/views/Stock/Stock.vue')
  },
  {
    path: '/proveedores',
    name: 'proveedores',
    component: () => import ('@/views/Proveedores/Proveedores.vue')
  },
  {
    path: '/compras',
    name: 'compras',
    component: () => import ('@/views/Compras/Compras.vue')
  },
  { 
    path: '/:pathMatch(.*)*', 
    redirect: {name: 'home'}
  } 
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach( async (to, from, next) => {

  let usuarioAutenticado = localStorage.getItem('usuarioAutenticado'); // esto cambiaría desde el back 
  store.commit('setUsuarioAutenticado', usuarioAutenticado);

  if(to.path != '/login'){
    if(usuarioAutenticado){
      next(); 
    }else{
      next('/login')
    }
  }else{
    if(!usuarioAutenticado){
      next(); 
    }else{
      next('/')
    }
  }
})

export default router
