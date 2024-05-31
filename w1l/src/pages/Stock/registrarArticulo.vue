<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';


const nombre = ref('')
const stockMinimo = ref('')
const stockInicial = ref(0);
const detalle = ref('')
const unidadDeMedida = ref(null)
const marca = ref('')
const pesoVolumen = ref(0);
const esMateriaPrima = ref(false)
const materiaPrima = ref(null)

const form = ref(null)

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

const fetchMateriaPrima = async () => {
  // try {
  //   axios.get('/employees/get-all').then((response) => {
  //       empleados.value = response.data;
  //       loading.value = false;
  //   })
  // } catch {
  //   algoSalioMalError(currentTheme.value);
  //   loading.value = false;
  // }
};

onMounted(fetchMateriaPrima);

const registrarArticulo = () => {
    form.value.validate().then(response => {
        // if (response.valid) {
        //     let params = {
        //       productName: nombre.value,
        //       productBrand: marca.value,
        //       actualStock: 0,
        //       minimunStock: stockMinimo.value,
        //       unitOfMeasurement: unidadDeMedida.value.id,
        //       detail: detalle.value
        //     }
        //     try {
        //         axios.post('/stock', params).then(() => {
        //             registroExitosoMensaje('stock', currentTheme.value)
        //             form.value.reset();
        //         })
        //     } catch {
        //         algoSalioMalError(currentTheme.value)
        //     }
        // }
    })
}

</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar Artículo</h2>
      <VForm @submit.prevent="registrarArticulo" ref="form" class="pt-2">
        <VRow>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="nombre"
              :rules="[reglaObligatoria()]"
              label="Nombre del artículo"
              placeholder='Manteca 500grs, Leche larga vida 1lt, etc'
            />
          </VCol>
          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="marca"
              :rules="[reglaObligatoria()]"
              label="Marca del artículo"
              placeholder='La Paulina, Ilolay, Coca Cola, etc.'
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="stockMinimo"
              :rules="[reglaObligatoria()]"
              label="Stock mínimo"
              type="number"
            />
          </VCol>

          <VCol
            cols="12"
            md="6"
          >
            <VTextField
              v-model="stockInicial"
              :rules="[reglaObligatoria()]"
              type="number"
              label="Stock Inicial"
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
            md="6"
          >
            <VTextField
              v-if="esMateriaPrima"
              v-model="pesoVolumen"
              :rules="[reglaObligatoria()]"
              type="number"
              label="Peso/Volúmen"
            />
          </VCol>
          <VCol
            cols="12"
            md="12"
          >
            <VCheckbox
              class="font-weight-medium pl-3"
              v-model="esMateriaPrima"
              label="ES MATERIA PRIMA?"
            />
          </VCol>
          <VCol
            v-if="esMateriaPrima"
            cols="12"
            md="6"
          >
            <VSelect
              :items="['MP 1','MP 2','MP 3']"
              :rules="[reglaObligatoria()]"
              v-model="materiaPrima"
              label="Seleccione la materia prima"
            />
          </VCol>
          <VCol
            v-if="esMateriaPrima"
            cols="12"
            md="6"
            class="d-flex pt-5"
          >
            <p><b>Desea registrar una nueva materia prima?</b></p>
            <b
              class="text-primary px-3 cursor-pointer"
              @click="dialogRegistrarArticulo = !dialogRegistrarArticulo"
              >CLICK AQUÍ</b
            >
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
