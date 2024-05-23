<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje, warningMessage } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const props = defineProps(['esModal']);
const emit = defineEmits(['cerrarDialogo']);
const form = ref(null)
const formIngredientes = ref(null)

const nombreReceta = ref(null);
const ingredienteSeleccionado = ref(null);
const cantidadSeleccionada = ref(null);
const unidadDeMedidaSeleccionada = ref(null);

const ingredientesAgregados = ref([]);

//TODO: Como nos manejamos con el agua?
// Como guardamos los datos en la bd? como mililitros o abreviado? (ml)

const stockProv = [
  {
    id: 1,
    nombre: 'Azucar',
    udm: 'grms'
  },
    {
    id: 2,
    nombre: 'Leche',
    udm: 'ml'
  },
    {
    id: 1,
    nombre: 'Harina',
    udm: 'grms'
  }
]

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarReceta = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            if(ingredientesAgregados.value.length == 0) {
              warningMessage('Debe agregar al menos 1 ingrediente', currentTheme.value.value);
              return;
            } else {
              console.log('registrar receta')
            }
        }
    })
}
const agregarIngrediente = () => {
  formIngredientes.value.validate().then(response => {
      if (response.valid) {
          ingredienteSeleccionado.value.cantidadSeleccionada = cantidadSeleccionada.value;
          ingredientesAgregados.value.push({ ...ingredienteSeleccionado.value })
          //Limpiamos
          ingredienteSeleccionado.value = null;
          cantidadSeleccionada.value = null;
          unidadDeMedidaSeleccionada.value = null;
      }
  })
}

const actualizarUnidadDeMedida = () => {
  unidadDeMedidaSeleccionada.value = ingredienteSeleccionado.value.udm;
}

const eliminarProductoDeLista = (index) => {
  if (index !== -1) {
    ingredientesAgregados.value.splice(index, 1)
  }
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
            md="8"
          >
            <VTextarea label="Procedimiento" placeholder="Ingrese los pasos a seguir"></VTextarea>
          </VCol>
          <VCol cols="12" md="12">
            <VForm @submit.prevent="agregarIngrediente" ref="formIngredientes">
              <VRow>
                <VCol
                  cols="12"
                  md="3"
                >
                  <VSelect
                    :items="stockProv"
                    item-title="nombre"
                    return-object
                    :rules="[reglaObligatoria()]"
                    v-model="ingredienteSeleccionado"
                    label="Ingrediente"
                    @update:modelValue="actualizarUnidadDeMedida"
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
                          <span>{{unidadDeMedidaSeleccionada}}</span>
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
              </VRow>
            </VForm>
          </VCol>
          <VCol
            cols="12"
            md="12"
            v-if="ingredientesAgregados.length != 0"
          >
            <VTable>
              <thead>
                <tr>
                  <th class="text-uppercase">INGREDIENTES</th>
                  <th class="text-uppercase">CANTIDAD</th>
                  <th class="text-uppercase">ACCIONES</th>
                </tr>
              </thead>

              <tbody>
                <tr
                  v-for="(item, index) in ingredientesAgregados"
                  :key="item.id"
                >
                  <td>
                    {{ item.nombre }}
                  </td>
                  <td>
                    {{ `${item.cantidadSeleccionada} ${item.udm}` }}
                  </td>
                  <td>
                    <IconBtn
                      icon="ri-delete-bin-5-fill"
                      color="error-darken-1"
                      class="me-1"
                      @click="eliminarProductoDeLista(index)"
                    />
                  </td>
                </tr>
              </tbody>
            </VTable>
          </VCol>
          <VCol
            cols="12"
            class="d-flex justify-end gap-4"
          >
            <VBtn type="submit"> Registrar </VBtn>
          </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
