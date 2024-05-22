<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const props = defineProps(['esModal']);
const emit = defineEmits(['cerrarDialogo']);

const nombre = ref('')
const marca = ref('')
const stockMinimo = ref('')
const detalle = ref('')
const form = ref(null)
const unidadDeMedida = ref(null)

const unidadesDeMedida = [
  {
    id: 1,
    nombre: 'Kilogramos'
  },
  {
    id: 2,
    nombre: 'Gramos'
  },
  {
    id: 3,
    nombre: 'Litros'
  },
  {
    id:5,
    nombre: 'Mililitros'
  },
  {
    id:4,
    nombre: 'Unidad'
  }
]

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarStock = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            let params = {
              productName: nombre.value,
              productBrand: marca.value,
              actualStock: 0,
              minimunStock: stockMinimo.value,
              unitOfMeasurement: unidadDeMedida.value.id,
              detail: detalle.value
            }
            try {
                axios.post('/stock', params).then(() => {
                    registroExitosoMensaje('stock', currentTheme.value)
                    form.value.reset();
                })
            } catch {
                algoSalioMalError(currentTheme.value)
            }
        }
    })
}

const cerrarModal = () => {
  emit('cerrarDialogo');
}

</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar producto</h2>
      <VForm @submit.prevent="registrarStock" ref="form" class="pt-2">
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
              :rules="[reglaObligatoria()]"
              label="Marca"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="stockMinimo"
              :rules="[reglaObligatoria()]"
              type="number"
              label="Stock mínimo"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VSelect
              :items="unidadesDeMedida"
              item-title="nombre"
              return-object
              :rules="[reglaObligatoria()]"
              v-model="unidadDeMedida"
              label="Unidad de medida"
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
            class="d-flex justify-end gap-4"
          >

            <VBtn 
              v-if="props.esModal"
              color="secondary"
              variant="outlined"
              @click="cerrarModal"
            >
              CANCELAR
            </VBtn>
            <VBtn
              v-else
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
