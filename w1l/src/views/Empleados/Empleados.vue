<template>

  <v-card>
    <v-tabs class="tabsStyles" v-model="tab">
      <v-tab value="1">Listado</v-tab>
      <v-tab value="2">Registrar</v-tab>
      <v-tab value="3">Adelantos de sueldo</v-tab>
     
      
    </v-tabs>

    <v-card-text class="h-screen">
      <v-window v-model="tab">
       
        <v-window-item value="1" id="windowListado">
          <br>
          <v-container >
          <h2>Listado de empleados registrados:</h2>
          <br>
          <listadoEmpleados />  
          </v-container >
        </v-window-item> 

        <v-window-item value="2">
          <br>
          <v-container >
          <h2>Ingrese los datos del nuevo empleado:</h2>
          <br>
          <registroEmpleado />
          </v-container >
        </v-window-item>

        <v-window-item value="3">
          <br>
          <h2>Ingrese el adelanto de sueldo para el emplado:</h2>
          <adelantoSueldo />
          <listadoAdelantoSueldo /> 
        </v-window-item>
        
          
        </v-window>


    </v-card-text>
  </v-card>

  
</template>




<script>
import "./Empleados.scss";
import registroEmpleado from '@/components/formulariosEmpleado/registroEmpleado.vue'
import adelantoSueldo from '@/components/formulariosEmpleado/adelantoSueldo.vue'
import listadoEmpleados from '@/components/listadosEmpleado/listadoEmpleados.vue'
import listadoAdelantoSueldo from '@/components/ListadoAdelantoSueldo/listadoAdelantoSueldo.vue'

// import listadoAdelantoSueldo from '@/components/listadosEmpleado/listadoAdelantoSueldo.vue'


export default {
  components: {
    registroEmpleado,
    adelantoSueldo,
    listadoEmpleados,
    listadoAdelantoSueldo,
  },
  data: () => ({
    tab: null,
  })
};
</script>


