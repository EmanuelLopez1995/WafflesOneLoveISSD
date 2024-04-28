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
    'Dueño',
    'Encargado general',
    'Referente de turno',
    'Colaborador',
]

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarEmpleado = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            let params = {
              nombre: nombre.value,
              apellido: apellido.value,
              dni: dni.value,
              numero: telefono.value,
              direccion: direccion.value,
              email: email.value,
              posicion: puesto.value
            }
            try {
                axios.post('/employees', params).then(() => {
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
