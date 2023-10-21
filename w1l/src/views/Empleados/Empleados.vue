<template>
  <v-card>
    <v-tabs v-model="tab" bg-color="#29272A">
      <v-tab value="1">Listado</v-tab>
      <v-tab value="2">Registrar</v-tab>
      <v-tab value="3">Eliminar</v-tab>
      <v-tab value="4">Adelantos de sueldo</v-tab>
     
      
    </v-tabs>

    <v-card-text class="h-screen">
      <v-window v-model="tab">
       
        <v-window-item value="1"></v-window-item> 

        <v-window-item value="2">
          <br>
          <h2>Ingrese los datos del nuevo empleado:</h2>
          <registroEmpleado />
        </v-window-item>

        <v-window-item value="3"> Eliminar </v-window-item>

        <v-window-item value="4">
          <br>
          <h2>Ingrese el adelanto de sueldo para el emplado:</h2>
          <adelantoSueldo />
          <h2>Aca abajo pondria el listado de adelantos que tiene el empleado que seleccionamos cuando registramos</h2>
        </v-window-item>
        
          
        </v-window>


    </v-card-text>
  </v-card>
</template>




<script>
import "./Empleados.scss";
import registroEmpleado from '@/components/formulariosEmpleado/registroEmpleado.vue'
import adelantoSueldo from '@/components/formulariosEmpleado/adelantoSueldo.vue'


export default {
  components: {
    registroEmpleado,
    adelantoSueldo,
  },
  data: () => ({
    tab: null,
  })
};
</script>


