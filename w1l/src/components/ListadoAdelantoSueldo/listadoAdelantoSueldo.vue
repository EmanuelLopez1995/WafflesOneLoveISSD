<template>
    

<v-table
    fixed-header
    height="500px"
    class="listadoEmpleados" 
  >
    <thead >
      <tr>
        <th class="text-center">
          Nombre
        </th>
        <th class="text-center">
          Apellido
        </th>
         <th class="text-center">
          Monto adelantado
        </th>
          <th class="text-center">
          Opciones
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="item in datos"
        :key="item.nombre"
      >
        <td>{{ item.nombre }} </td>
        <td>{{ item.apellido }}</td>
        <td>{{ item.adelantoSueldo }}</td>
        
      </tr>
    </tbody>
  </v-table>

</template>

<script>
  export default {
    data () {
      return {
        datos: [
          {
            nombre: 'Ignacio ',
            apellido: 'Santamaria',
            adelantoSueldo: '2000',
          },
          {
            nombre: 'Ignacio ',
            apellido: 'Santamaria',
            adelantoSueldo: '4000',
          },
          
        ],
      }
    },
  }
</script>