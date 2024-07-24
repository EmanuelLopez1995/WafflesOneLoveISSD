import { defineStore } from 'pinia';
import axios from 'axios';

export const useGeneralStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    user: null,
    turnoIniciado: localStorage.getItem('turnoIniciado') === 'true',
  }),
  actions: {
    async login(credentials) {
      try {
        const response = await axios.post('/auth/login', credentials)
        this.token = response.data.token;
        localStorage.setItem('token', this.token);

        // Decodifica el JWT para obtener la información del usuario
        const decodedToken = JSON.parse(atob(this.token.split('.')[1]));
        this.user = decodedToken;
        
        // Puedes también guardar la información del usuario en localStorage si lo prefieres
        localStorage.setItem('user', JSON.stringify(this.user));

        // Redirige o realiza otras acciones post-login
      } catch (error) {
        return error
      }
    },
    logout() {
      this.token = '';
      this.user = null;
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      // Redirige o realiza otras acciones post-logout
    },
    // async updateUser() {
    //   if (this.token) {
    //     try {
    //       // Supongamos que tienes un endpoint para obtener los datos del usuario
    //       const response = await axios.get('/auth/user');
    //       this.user = response.data;
    //       // Guarda la información del usuario en localStorage si lo prefieres
    //       // localStorage.setItem('user', JSON.stringify(this.user));
    //     } catch (error) {
    //       console.error('Failed to update user', error);
    //     }
    //   }
    // },
    iniciarTurno() {
      this.turnoIniciado = true;
      localStorage.setItem('turnoIniciado', 'true');
    },
    finalizarTurno() {
      this.turnoIniciado = false;
      localStorage.setItem('turnoIniciado', 'false');
    },
    updateTurnoIniciado() {
      this.turnoIniciado = localStorage.getItem('turnoIniciado') === 'true';
    }
  },
  getters: {
    isAuthenticated(state) {
      return !!state.token;
    },
  },
});