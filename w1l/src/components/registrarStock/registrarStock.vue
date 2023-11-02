<template>
<div>
    <h2 class="pt-4">Registrar producto</h2>
  <v-form @submit.prevent="registrarProducto" ref="form">
    <v-container id="formRegistroStock" >
        <v-row>
            <v-col>
                <v-text-field
                    v-model="nombreProducto"
                    :rules="reglas.nombre"
                    label="Nombre del producto"
                    class="inputsFormStock"
                ></v-text-field>
            </v-col>
            <v-col>
                <v-text-field
                    v-model="marcaProducto"
                    label="Marca"
                    class="inputsFormStock"
                ></v-text-field>
            </v-col>

        </v-row>
        <v-row>
            <v-col>
                <v-text-field
                    v-model="stockMinimoProducto"
                    :rules="reglas.notNull"
                    type="number"
                    label="Stock mínimo"
                    class="inputsFormStock"
                    color="green-darken-4"
                ></v-text-field>
            </v-col>
            <v-col>
                <v-text-field
                    v-model="detalleProducto"
                    label="Detalle"
                    class="inputsFormStock"
                ></v-text-field>
            </v-col>

        </v-row>
    </v-container>



    <br>
    <v-btn class="me-10" type="submit" color="green-darken-4"> Registrar </v-btn>

    <v-btn @click="resetForm" class="botonLimpiar " color="red-darken-4"> Limpiar </v-btn>
  </v-form>
</div>
</template>

<script>
import './registrarStock.scss'
export default {
  data: () => ({
    tab: null,
    nombreProducto: '',
    detalleProducto: '',
    stockMinimoProducto: '',
    marcaProducto: ''
  }),
  computed: {
    reglas() {
      return this.$store.getters['validaciones/reglas'];
    }
  },
  methods: {
    resetForm () {
      this.$refs.form.reset()
    },
    registrarProducto() {
      this.$refs.form.validate().then(response => {
        if (response.valid) {
          console.log('form valido')
        }
      });
    }
  }
}
</script>

<style>

</style>