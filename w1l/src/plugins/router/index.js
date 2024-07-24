import { createRouter, createWebHistory } from 'vue-router'
import { routes } from './routes'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach( async (to, from, next) => {

  let user = localStorage.getItem('user');

  if(to.path != '/login'){
    if(user){
      next(); 
    }else{
      next('/login')
    }
  }else{
    if(!user){
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
