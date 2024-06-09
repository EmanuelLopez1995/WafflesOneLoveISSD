<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const nombre = ref('')
const apellido = ref('')
const dni = ref('')
const telefono = ref('')
const direccion = ref('')
const email = ref('')
const puesto = ref('')
const form = ref(null)

const itemsPuestos = [
    {
      id: 1,
      nombre: 'Colaborador'
    },
    {
      id: 2,
      nombre: 'Encargado general'
    },
    {
      id: 3,
      nombre: 'Dueño'
    }
]

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarEmpleado = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            let params = {
              nombreEmpleado: nombre.value,
              apellidoEmpleado: apellido.value,
              dniEmpleado: dni.value,
              telefonoEmpleado: telefono.value,
              direccionEmpleado: direccion.value,
              mailEmpleado: email.value,
              idPuestoEmpleado: puesto.value.id
            }
            try {
                axios.post('/Empleado/AddEmpleado', params).then(() => {
                    registroExitosoMensaje('empleado', currentTheme.value)
                    form.value.reset();
                })
            } catch {
                algoSalioMalError(currentTheme.value)
            }
        }
    })
}

</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar Empleado</h2>
      <VForm @submit.prevent="registrarEmpleado" ref="form" class="pt-2">
        <VRow>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="nombre"
              :rules="[reglaObligatoria()]"
              label="Nombre"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="apellido"
              :rules="[reglaObligatoria()]"
              label="Apellido"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="dni"
              :rules="[reglaObligatoria()]"
              label="DNI"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="telefono"
              :rules="[reglaObligatoria()]"
              label="Teléfono"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="direccion"
              :rules="[reglaObligatoria()]"
              label="Dirección"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="email"
              type="email"
              :rules="[validarEmail(), reglaObligatoria()]"
              label="Email"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VSelect
              v-model="puesto"
              :rules="[reglaObligatoria()]"
              :items="itemsPuestos"
              item-title="nombre"
              return-object
              label="Puesto"
            />
          </VCol>

          <VCol
            cols="12"
            class="d-flex gap-4"
          >
            <VBtn type="submit"> Registrar </VBtn>

            <VBtn
              type="reset"
              color="secondary"
              variant="outlined"
            >
              Limpiar
            </VBtn>
          </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
