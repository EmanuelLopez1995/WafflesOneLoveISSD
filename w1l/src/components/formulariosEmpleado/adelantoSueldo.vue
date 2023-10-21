<template>
    <v-form @submit.prevent id="formularioAdelanto" ref="form">
    
    <v-select
        v-model="empleados"
        :rules="reglas.empleados"
        :items="itemsEmpleados"
        label="Empleado"
       
    ></v-select>

    <v-text-field
        v-model="adelantoSueldo"
        :rules="reglas.adelantoSueldo"
        :counter="15"
        label="Adelanto Sueldo"
        type="number"
    ></v-text-field>
    
    <v-checkbox
        v-model="confirmarAdelanto"
        :rules="reglas.confirmarAdelanto"
        value="1"
        label="Confirmar adelanto de sueldo"
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
    empleados: '',
    adelantoSueldo: '',
    confirmarAdelanto: false,
    itemsEmpleados: [
      'Jorge "El gallego" Salas',
      'Emanuel "El ex new cocodrile" Lopez',
      'Ignacio "El brujo" Santamaria',
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