<template>
  <v-card>
    <v-tabs v-model="tab" bg-color="#29272A">
      <v-tab value="1">Registrar</v-tab>
      <v-tab value="2">Consultas</v-tab>
      <v-tab value="3">Eliminar</v-tab>
      <v-tab value="4">Adelantos de sueldo</v-tab>
    </v-tabs>

    <v-card-text class="h-screen">
      <v-window v-model="tab">
        <v-window-item value="1">
          <formularioEmpleado />
        </v-window-item>

        <v-window-item value="2"> Two </v-window-item>

        <v-window-item value="3"> Three </v-window-item>

        <v-window-item value="4"> Four </v-window-item>
      </v-window>
    </v-card-text>
  </v-card>
</template>




<script>
import "./Empleados.scss";
import formularioEmpleado from '@/components/formularioEmpleado/formularioEmpleado.vue'

export default {
  components: {
    formularioEmpleado
  },
  data: () => ({
    tab: null,
  })
};
</script>


