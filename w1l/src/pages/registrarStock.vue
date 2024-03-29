<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const nombre = ref('')
const marca = ref('')
const stockMinimo = ref('')
const detalle = ref('')
const form = ref(null)

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarStock = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            console.log('formulario valido')
        }
    })
}

</script>

<template>
  <VForm @submit.prevent="registrarStock" ref="form">
    <VRow>
      <VCol
        cols="12"
        md="6"
      >
        <VTextField
          v-model="nombre"
          :rules="[reglaObligatoria()]"
          label="Nombre del producto"
        />
      </VCol>

      <VCol
        cols="12"
        md="6"
      >
        <VTextField
          v-model="marca"
          label="Marca"
        />
      </VCol>

      <VCol
        cols="12"
        md="6"
      >
        <VTextField
          v-model="stockMinimo"
          type="number"
          label="Stock mínimo"
        />
      </VCol>

      <VCol
        cols="12"
        md="6"
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
</template>
