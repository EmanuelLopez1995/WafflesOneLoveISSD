import { createStore } from 'vuex'

export default createStore({
  state: {
    usuarioAutenticado: false
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
  }
})
