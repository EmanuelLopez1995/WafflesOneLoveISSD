<template>
    <v-form @submit.prevent id="formularioRegistro" ref="form">
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
        :rules="reglas.dni"
        :counter="15"
        label="DNI"
        class="inputsFormEmpleados"
    ></v-text-field>

    <v-text-field
        v-model="telefono"
        :rules="reglas.telefono"
        :counter="15"
        label="Telefono"
        class="inputsFormEmpleados"
    ></v-text-field>

    <v-text-field
        v-model="direccion"
        :rules="reglas.direccion"
        :counter="10"
        label="Direccion"
        class="inputsFormEmpleados"
    ></v-text-field>

    <v-text-field
        v-model="email"
        :rules="reglas.email"
        label="E-mail"
        class="inputsFormEmpleados"
    ></v-text-field>

     <v-text-field
        v-model="sueldoNormal"
        :rules="reglas.sueldoNormal"
        label="Sueldo hora normal"
        class="inputsFormEmpleados"
        type=number
    ></v-text-field>

     <v-text-field
        v-model="sueldoFeriado"
        :rules="reglas.sueldoFeriado"
        label="Sueldo hora feriado"
        class="inputsFormEmpleados"
        type=number
    ></v-text-field>

     <v-text-field
        v-model="sueldoDomingo"
        :rules="reglas.sueldoDomingo"
        label="Sueldo hora domingo"
        class="inputsFormEmpleados"
        type=number
    ></v-text-field>

    <v-select
        v-model="puestos"
        :rules="reglas.puestos"
        :items="itemsPuestos"
        label="Puesto"
        class="inputsFormEmpleados"
    ></v-select>
    
    <v-checkbox
        v-model="confirmarDatos"
        :rules="reglas.confirmarDatos"
        value="1"
        label="Confirmar datos"
        type="checkbox"
    ></v-checkbox>
    <br>
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
    sueldoNormal: '',
    sueldoFeriado: '',
    sueldoDomingo: '',
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