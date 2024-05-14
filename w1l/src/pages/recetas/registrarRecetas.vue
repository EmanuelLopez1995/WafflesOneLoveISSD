<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const props = defineProps(['esModal']);
const emit = defineEmits(['cerrarDialogo']);

const nombreReceta = ref(null);
const ingredienteSeleccionado = ref(null);
const cantidadSeleccionada = ref(0);

//TODO: Como nos manejamos con el agua? Hacer objetos que representen al stock, hacer otro form para validar que se seleccione ingrediente y cantidad

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarReceta = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            console.log('formulario valido')
        }
    })
}

</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar Receta</h2>
      <VForm @submit.prevent="registrarReceta" ref="form" class="pt-2">
        <VRow>
          <VCol
            cols="12"
            md="4"
          >
            <VTextField
              v-model="nombreReceta"
              :rules="[reglaObligatoria()]"
              label="Nombre de la receta"
              placeholder="Ejemplos: Masa, Salsa de verdeo, Bon o Bon, etc"
            />
          </VCol>
          <VCol cols="12" md="8"></VCol>

          <VCol
            cols="12"
            md="3"
          >
            <VSelect
              :items="['Harina', 'Leche', 'Azucar', 'Agua', 'Sal']"
              :rules="[reglaObligatoria()]"
              v-model="ingredienteSeleccionado"
              label="Ingrediente"
            />
          </VCol>
          <VCol
            cols="12"
            md="2"
          >
            <VTextField
                v-model="cantidadSeleccionada"
                :rules="[reglaObligatoria()]"
                label="Cantidad"
                type="number"
            >
                <template v-slot:append-inner>
                    <span>ml</span>
                </template>
            </VTextField>
          </VCol>
            <VCol
            cols="12"
            md="2"
          >
            <VBtn
              color="primary"
              class="mt-1"
              type="submit"
              variant="outlined"
            >
              Agregar
            </VBtn>
          </VCol>
        
          <VCol
            cols="12"
            class="d-flex justify-end gap-4"
          >
            <VBtn
              type="reset"
              color="secondary"
              variant="outlined"
            >
              Limpiar
            </VBtn>
            <VBtn type="submit"> Registrar </VBtn>
          </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
