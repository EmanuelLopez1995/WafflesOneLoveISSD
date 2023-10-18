import { createStore } from 'vuex'

export default createStore({
  state: {
    usuarioAutenticado: false,
    reglas: {
      nombre: [
        value => {
          if (value) return true
          return 'El nombre es obligatorio'
        }
      ],
      apellido: [
        value => {
          if (value) return true
          return 'El apellido es obligatorio'
        }
      ],
    }
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
