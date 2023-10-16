import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView/HomeView.vue'
import Empleados from '../views/Empleados/Empleados.vue'
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
    component: Empleados
  },
  {
    path: '/login',
    name: 'login',
    component: () => import ('@/views/Login/Login.vue')
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
  let usuarioAutenticado = true; // este valor va a venir del backend
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
