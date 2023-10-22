import { createStore } from 'vuex'
import validacionesStore from '@/store/validaciones'

export default createStore({
  state: {
    usuarioAutenticado: false,
  },
  getters: {
  },
  mutations: {
    setUsuarioAutenticado(state, val){
      state.usuarioAutenticado = val;
    }
  },
  actions: {
  },
  modules: {
    validaciones: validacionesStore
  }
})
