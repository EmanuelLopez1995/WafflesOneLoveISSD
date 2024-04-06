import { createRouter, createWebHistory } from 'vue-router'
import { routes } from './routes'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach( async (to, from, next) => {

  let usuarioAutenticado = localStorage.getItem('usuarioAutenticado'); // esto cambiaría desde el back 

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

export default function (app) {
  app.use(router)
}
export { router }
