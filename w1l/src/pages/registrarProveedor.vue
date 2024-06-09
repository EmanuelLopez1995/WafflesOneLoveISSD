<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const nombre = ref('')
const razonSocial = ref('')
const direccion = ref('')
const telefono = ref('')
const cuit = ref('')
const email = ref('')
const detalle = ref('')
const form = ref(null)

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarProveedor = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            let params = {
                nombre: nombre.value,
                razonSocial: razonSocial.value,
                direccion: direccion.value,
                numero: telefono.value,
                cuit: cuit.value,
                email: email.value,
                detalle: detalle.value
            }
            try {
                axios.post('/Proveedor/AddProveedor', params).then(() => {
                    registroExitosoMensaje('proveedor', currentTheme.value)
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
      <h2 class="pb-3 mt-3">Registrar proveedor</h2>
      <VForm @submit.prevent="registrarProveedor" ref="form" class="pt-2">
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
              v-model="razonSocial"
              label="Razón Social"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="direccion"
              label="Dirección"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="telefono"
              label="Teléfono"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="cuit"
              type="number"
              label="CUIT"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="email"
              type="email"
              :rules="[validarEmail()]"
              label="Email"
            />
          </VCol>

          <VCol
            cols="12"
            md="12"
          >
            <VTextField
              v-model="detalle"
              label="Detalle"
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
