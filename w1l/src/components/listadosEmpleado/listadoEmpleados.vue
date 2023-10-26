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
          DNI
        </th>
         <th class="text-center">
          Telefono
        </th>
         <th class="text-center">
          Email
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
        <td>{{ item.nombre }}</td>
        <td>{{ item.apellido }}</td>
        <td>{{ item.dni }}</td>
        <td>{{ item.telefono }}</td>
        <td>{{ item.email }}</td>
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
            dni: '38001617',
            telefono: '3512278051',
            email: 'santamariaign@live.com.ar',
          },
          {
            nombre: 'Ignacio ',
            apellido: 'Santamaria',
            dni: '38001617',
            telefono: '3512278051',
            email: 'santamariaign@live.com.ar',
          },
          {
            nombre: 'Ignacio ',
            apellido: 'Santamaria',
            dni: '38001617',
            telefono: '3512278051',
            email: 'santamariaign@live.com.ar',
          },
          {
            nombre: 'Ignacio ',
            apellido: 'Santamaria',
            dni: '38001617',
            telefono: '3512278051',
            email: 'santamariaign@live.com.ar',
          },
          
        ],
      }
    },
  }
</script>