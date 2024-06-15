import { defineStore } from 'pinia';

export const useGeneralStore = defineStore('general', {
  state: () => ({
    turnoIniciado: localStorage.getItem('turnoIniciado') === 'true'
  }),
  actions: {
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
  }
});