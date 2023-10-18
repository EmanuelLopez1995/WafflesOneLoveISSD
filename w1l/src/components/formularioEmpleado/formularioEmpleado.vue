<template>
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
</template>

<script>
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
}
</script>

<style>

</style>