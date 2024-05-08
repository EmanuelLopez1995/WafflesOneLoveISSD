<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const horaNormal = ref(null)
const horaDomFeriado = ref(null)
const plusEncargado = ref(null)
const basicoDuenio = ref(null)

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const registrarSueldos = () => {
//     form.value.validate().then(response => {
//         if (response.valid) {
//             let params = {
//               nombre: nombre.value,
//               apellido: apellido.value,
//               dni: dni.value,
//               numero: telefono.value,
//               direccion: direccion.value,
//               email: email.value,
//               posicion: puesto.value
//             }
//             try {
//                 axios.post('/employees', params).then(() => {
//                     registroExitosoMensaje('empleado', currentTheme.value)
//                     form.value.reset();
//                 })
//             } catch {
//                 algoSalioMalError(currentTheme.value)
//             }
//         }
//     })
}
</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar sueldos</h2>
      <VForm
        @submit.prevent="registrarSueldos"
        ref="form"
        class="pt-2"
      >
        <VRow>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="horaNormal"
              prepend-inner-icon="ri-money-dollar-circle-line"
              :rules="[reglaObligatoria()]"
              label="Hora estándar"
              type="number"
            />
          </VCol>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="horaDomFeriado"
              prepend-inner-icon="ri-money-dollar-circle-line"
              :rules="[reglaObligatoria()]"
              label="Hora domingo/feriado"
              type="number"
            />
          </VCol>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="plusEncargado"
              prepend-inner-icon="ri-money-dollar-circle-line"
              :rules="[reglaObligatoria()]"
              label="Plus de encargado General"
              type="number"
            />
          </VCol>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="basicoDuenio"
              prepend-inner-icon="ri-money-dollar-circle-line"
              :rules="[reglaObligatoria()]"
              label="Básico dueño"
              type="number"
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
