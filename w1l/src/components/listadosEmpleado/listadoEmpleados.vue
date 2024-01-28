<template>
    <div class="listadoEmpleados">
        <Table :contenido="empleados" :titulos="titulosTabla" titulo="Empleados" :loading="loading" @eliminar="eliminarEmpleado"/> 
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
        loading: false,
        titulosTabla: [
            {key: 'id', title: 'Id',},
            {key: 'nombre', title: 'Nombre' },
            {key: 'apellido', title: 'Apellido' },
            {key: 'dni', title: 'Dni' },
            {key: 'numero', title: 'Teléforno' },
            {key: 'direccion', title: 'Dirección' },
            {key: 'email', title: 'Email' },
            {key: 'posicion', title: 'Puesto' },
            {key: 'opciones', title: 'Opciones' }
        ],
      }
    },
       mounted() {
          this.getDatosEmpleados();
          
      },
       methods: {
          getDatosEmpleados() {
            this.loading = true;
                this.$http.get('/employees/get-all').then((response) =>{
                   this.empleados = response.data;
                   this.loading = false;
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