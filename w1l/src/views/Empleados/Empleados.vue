<template>
  <v-card>
    <v-tabs v-model="tab" bg-color="#29272A">
      <v-tab value="1">Registrar</v-tab>
      <v-tab value="2">Consultas</v-tab>
      <v-tab value="3">Eliminar</v-tab>
      <v-tab value="4">Adelantos de sueldo</v-tab>
    </v-tabs>

    <v-card-text>
      <v-window v-model="tab">
        <v-window-item value="1">

          <v-form @submit.prevent id="formulario" ref="form">
            <v-text-field
              v-model="nombre"
              :rules="reglas.nombre"
              :counter="15"
              label="Nombre"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-text-field
              v-model="apellido"
              :rules="reglas.apellido"
              :counter="15"
              label="Apellido"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-text-field
              v-model="dni"
              :rules="reglas.apellido"
              :counter="15"
              label="DNI"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-text-field
              v-model="telefono"
              :rules="reglas.apellido"
              :counter="15"
              label="Telefono"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-text-field
              v-model="direccion"
              :rules="reglas.apellido"
              :counter="10"
              label="Direccion"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-text-field
              v-model="email"
              :rules="reglas.apellido"
              label="E-mail"
              class="inputsFormEmpleados"
            ></v-text-field>

            <v-select
              v-model="puestos"
              :rules="reglas.apellido"
              :items="itemsPuestos"
              label="Puesto"
              class="inputsFormEmpleados"
            ></v-select>

            <v-checkbox
              v-model="confirmarDatos"
              :rules="reglas.apellido"
              value="1"
              label="Confirmar datos"
              type="checkbox"
            ></v-checkbox>

            <v-btn class="me-10" type="submit"> Registrar </v-btn>

            <v-btn @click="resetForm" class="botonLimpiar"> Limpiar </v-btn>
          </v-form>

          <!-- </v-sheet> -->
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

export default {
  data: () => ({
    tab: null,
    nombre: '',
    apellido: '',
    dni: '',
    telefono: '',
    direccion: '',
    email: '',
    puestos: '',
    confirmarDatos: false,
    itemsPuestos: [
      'Cocinero',
      'Encargado de turno',
      'Encargado general',
      'Limpieza',
    ]
  }),
  computed: {
    reglas() {
      return this.$store.state.reglas;
    }
  },
  methods: {
    resetForm () {
      this.$refs.form.reset()
    },
  }
};
</script>


