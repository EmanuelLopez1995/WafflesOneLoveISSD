<template>
    <div class="listadoEmpleados">
        <Table v-if="empleados" :contenido="empleados" :titulos="titulosTabla"/> 
    </div>
</template>

<script>
import Table from '../Table/Table.vue'

  export default {
    components: {
        Table
    },
    data () {
      return {
        empleados: [],
        titulosTabla:['ID','Nombre','Apellido','DNI','Telefono','Direccion','Email','Puesto']
      }
    },
       mounted() {
          this.getDatosEmpleados();
          
      },
       methods: {
          getDatosEmpleados() {
                this.$http.get('/employees/get-all').then((response) =>{
                  console.log('Respuesta de la API:', response.data);
                   this.empleados = response.data;
              })
               .catch(error => {
                  console.error('Error al obtener empleados', error);
                });
           }
     },
  }
</script>