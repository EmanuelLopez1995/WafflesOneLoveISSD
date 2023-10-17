<template>
  <v-card>
    <v-tabs
      v-model="tab"
      bg-color="#29272A"
    >
      <v-tab value="1">Registrar</v-tab>
      <v-tab value="2">Consultas</v-tab>
      <v-tab value="3">Eliminar</v-tab>
      <v-tab value="4">Adelantos de sueldo</v-tab>
      
    </v-tabs>

    <v-card-text>


      <v-window v-model="tab" >


        <v-window-item value="1" >
         
              <!-- <v-sheet width="300"  id="formulario" > -->

                <!--NOMBRE-->
              <form @submit.prevent="submit" id="formulario">
                  <v-text-field
                    v-model="nombre.value.value"
                    :counter="15"
                    :error-messages="nombre.errorMessage.value"
                    label="Nombre"
                    id="inputsFormEmpleados"
                    
                  ></v-text-field>


                  <v-text-field
                    v-model="apellido.value.value"
                    :counter="15"
                    :error-messages="apellido.errorMessage.value"
                    label="Apellido"
                    id="inputsFormEmpleados"
                  ></v-text-field>


                  <v-text-field
                    v-model="dni.value.value"
                    :counter="15"
                    :error-messages="dni.errorMessage.value"
                    label="DNI"
                    id="inputsFormEmpleados"
                  ></v-text-field>


                  <v-text-field
                    v-model="telefono.value.value"
                    :counter="15"
                    :error-messages="telefono.errorMessage.value"
                    label="Telefono"
                    id="inputsFormEmpleados"
                  ></v-text-field>


                   <v-text-field
                    v-model="direccion.value.value"
                    :counter="10"
                    :error-messages="direccion.errorMessage.value"
                    label="Direccion"
                    id="inputsFormEmpleados"
                  ></v-text-field>


                  <v-text-field
                    v-model="email.value.value"
                    :error-messages="email.errorMessage.value"
                    label="E-mail"
                    id="inputsFormEmpleados"
                  ></v-text-field>

                  <v-select
                    v-model="puestos.value.value"
                    :items="itemsPuestos"
                    :error-messages="puestos.errorMessage.value"
                    label="Puesto"
                    id="inputsFormEmpleados"
                  ></v-select>

                  <v-checkbox
                    v-model="confirmarDatos.value.value"
                    :error-messages="confirmarDatos.errorMessage.value"
                    value="1"
                    label="Confirmar datos"
                    type="checkbox"
                    
                  ></v-checkbox>

                  <v-btn
                    class="me-10"
                    type="submit"
                    
                  >
                    Registrar
                  </v-btn>

                  <v-btn @click="handleReset" class="botonLimpiar">
                    Limpiar
                  </v-btn>
                </form>


              <!-- </v-sheet> -->
            
        </v-window-item>








        <v-window-item value="2">
          Two
        </v-window-item>

        <v-window-item value="3">
          Three
        </v-window-item>

        <v-window-item value="4">
          Four
        </v-window-item>
      
      </v-window>
    </v-card-text>
  </v-card>
</template>




<script>
import './Empleados.scss'

  export default {
    data: () => ({
      tab: null,
    }),
  }
</script>



<script setup>
  import { ref } from 'vue'
  import { useField, useForm } from 'vee-validate'

  const { handleSubmit, handleReset } = useForm({
    validationSchema: {
      nombre (value) {
        if (value?.length >= 2) return true

        return 'El nombre tiene que tener al menos dos caracteres.'
      },
      apellido (value) {
        if (value?.length >= 2) return true

        return 'El apellido tiene que tener al menos dos caracteres.'
      },
      dni (value) {
        if (value?.length >= 8) return true

        return 'El DNI tiene que tener al menos 8 caracteres.'
      },
      telefono (value) {
        if (value?.length > 9 && /[0-9-]+/.test(value)) return true

        return 'El numero de telefono tiene que tener al menos 9 caracteres'
      },
      direccion (value) {
         if (value?.length >= 10) return true

        return 'La direccion tiene que tener al menos 10 caracteres.'
      },
      email (value) {
        if (/^[a-z.-]+@[a-z.-]+\.[a-z]+$/i.test(value)) return true

        return 'Debe ser un correo electronico valido'
      },
      puesto (value) {
        if (value) return true

        return 'Seleccione un puesto'
      },
      checkbox (value) {
        if (value === '1') return true

        return 'Valide el formulario!!'
      },
    },
  })
  const nombre = useField('nombre')
  const apellido = useField('apellido')
  const dni = useField('dni')
  const telefono = useField('telefono')
  const direccion = useField('direccion')
  const email = useField('email')
  const puestos = useField('puestos')
  const confirmarDatos = useField('confirmarDatos')

  const itemsPuestos = ref([
    'Cocinero',
    'Encargado de turno',
    'Encargado general',
    'Limpieza',
  ])

  const submit = handleSubmit(values => {
    alert(JSON.stringify(values, null, 2))
  })

</script>


