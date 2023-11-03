<template>
    <div>
        <h2 class="pt-4">Registrar proveedor</h2>
        <v-form @submit.prevent="registrarProveedor" ref="form">
            <v-container id="formRegistroProveedor" >
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="nombreFantasiaProveedor"
                            :rules="reglas.nombre"
                            label="Nombre de fantasía"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="razonSocialProveedor"
                            label="Razón social"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>

                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="domicilioProveedor"
                            label="Domicilio"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="telefonoProveedor"
                            label="Teléfono"
                            type="tel"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="cuitProveedor"
                            label="CUIT"
                            type = "number"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="emailProveedor"
                            type="email"
                            label="Email"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-textarea
                            v-model="detalleProveedor"
                            label="Detalle"
                            class="textareaProveedor"
                        ></v-textarea>
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
import './RegistrarProveedor.scss'
export default {
  data: () => ({
    nombreFantasiaProveedor: '',
    razonSocialProveedor: '',
    domicilioProveedor: '',
    telefonoProveedor: '',
    cuitProveedor: '',
    emailProveedor: '',
    detalleProveedor: ''
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
    registrarProveedor() {
      this.$refs.form.validate().then(response => {
        if (response.valid) {
          console.log('form valido')
        }
      });
    }
  }
}
</script>