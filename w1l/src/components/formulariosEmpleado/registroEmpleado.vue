<template>
<div>
    <v-form @submit.prevent="registrarEmpleado()" id="formularioRegistro" class="contenedorRegistroEmpleado" ref="form">
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
    <v-btn class="me-10" color="green-darken-4" type="submit"> Registrar </v-btn>

    <v-btn @click="resetForm" class="botonLimpiar"> Limpiar </v-btn>
    </v-form>
    <v-progress-circular v-if="loading"
            :width="5"
            color="green"
            indeterminate
    ></v-progress-circular>
    <snack-bar v-if="submitFinalizado" :exitoso="esExitoso" @timeout-completado="submitFinalizado = false" />
    </div>
</template>

<script>
import SnackBar from '@/components/SnackBar/SnackBar.vue'

export default {
  components: {
        SnackBar
    },
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
      'Dueño',
      'Encargado general',
      'Referente de turno',
      'Colaborador',
    ]
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
    registrarEmpleado(){
      this.$refs.form.validate().then(response => {
            if (response.valid) {
                   let params = {
                      nombre: this.nombre,
                      apellido: this.apellido,
                      dni: this.dni,
                      numero: this.telefono,
                      direccion: this.direccion,
                      email: this.email,
                      posicion: this.puestos
              }
               this.loading = true;
                    this.$http.post('/employees', params).then((response) =>{
                        this.loading = false;
                        if(response.status == 200){
                            this.esExitoso = true;
                        }else{
                            this.esExitoso = false;
                        }
                        this.resetForm();
                        this.submitFinalizado = true

                    }).catch((error)=>{
                        this.esExitoso = false;
                    })
                }
            });
    }
  }
}
</script>

