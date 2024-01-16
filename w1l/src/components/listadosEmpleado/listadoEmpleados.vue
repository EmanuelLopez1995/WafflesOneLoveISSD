<template>
    <div class="listadoEmpleados">
        <Table v-if="empleados" :contenido="empleados" :titulos="titulosTabla" @eliminar="eliminarEmpleado"/> 
    </div>
</template>

<script>
import Table from '../Table/Table.vue';
import {algoSalioMalError} from '@/components/Swal/SwalCustom.js';

  export default {
    components: {
        Table
    },
    props: {
        tabSelected: Boolean,
    },
    watch: {
        tabSelected(newValue) {
            if(newValue) {
                this.getDatosEmpleados();
            }
        },
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
                   this.empleados = response.data;
              })
               .catch(error => {
                  algoSalioMalError();
                });
           },
           eliminarEmpleado(id) {
              const url = `/employees?id=${id}`;

              this.$http.delete(url).then(response => {
                  this.getDatosEmpleados();
              })
              .catch(error => {
                  algoSalioMalError();
              });
}
     },
  }
</script>