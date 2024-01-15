<template>
    <div class="listadoEmpleados">
        <Table v-if="empleados" :contenido="empleados" :titulos="titulosTabla" @eliminarEmpleado="eliminarEmpleado"/> 
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
           },
           eliminarEmpleado(id) {
              console.log(`Intentando eliminar al empleado con ID: ${id}`);

              const url = `/employees?id=${id}`;

              console.log(`Intentando eliminar al empleado con ID: ${id}`);

              this.$http.delete(url)
              
                  .then(response => {
                    console.log(`Intentando eliminar al empleado con ID: ${id}`);
                      console.log('Respuesta de la API:', response.data);
                      console.log('Empleado eliminado con éxito');
                      console.log(`Eliminar empleado con ID: ${id}`);
                      this.getDatosEmpleados();
                  })
                  .catch(error => {
                      console.error('Error al eliminar empleado', error);
                  });
}
     },
  }
</script>