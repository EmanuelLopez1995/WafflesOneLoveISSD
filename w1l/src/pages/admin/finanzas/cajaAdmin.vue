<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const dialogModificarActivoInicial = ref(false);
const activoInicial = ref(0)
const activoInicialAmodificar = ref(0);
const formActivoInicial = ref(null)

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})


const obtenerActivoInicial = () => {
  axios.get('/paymentBoxInitialActive?id=1')
    .then((response) => {
      activoInicial.value = response.data.initialActive;
    })
    .catch((error) => {
      algoSalioMalError(currentTheme.value);
    });
}


const modificarActivoInicial = () => {
    formActivoInicial.value.validate().then(response => {
      if (response.valid) {
        try {
          let params = {
            id: 1,
            initialActive: activoInicialAmodificar.value
          }
          axios.put('/paymentBoxInitialActive', params).then((response) => {
              obtenerActivoInicial();
              cancelarDialogActivoInicial();
              registroExitosoMensaje('activo inicial', currentTheme.value)
          }). catch(() => {
            cancelarDialogActivoInicial();
            algoSalioMalError(currentTheme.value);
          })
        } catch (error) {
          cancelarDialogActivoInicial();
          algoSalioMalError(currentTheme.value);
        }
      }
    })
}

onMounted(() => {
  obtenerActivoInicial();
})

const cancelarDialogActivoInicial = () => {
  dialogModificarActivoInicial.value = !dialogModificarActivoInicial.value
}

const abrirDialogoActivoInicial = () => {
  activoInicialAmodificar.value = activoInicial.value;
  dialogModificarActivoInicial.value = !dialogModificarActivoInicial.value
}

</script>

<template>
  <div>
    <div class="d-flex align-center">
      <h2>Activo inicial actual: ${{activoInicial}}</h2>
      <VBtn class="mx-5" @click="abrirDialogoActivoInicial">Modificar</VBtn>
    </div>

    <!-- Dialog modificar activo inicial -->
    <VDialog
      v-model="dialogModificarActivoInicial"
      width="30%"
    >
      <VCard
        prepend-icon="ri-edit-2-line"
        title="Modificar Activo Inicial"
        class="px-5"
      >
        <VForm
          @submit.prevent="modificarActivoInicial"
          ref="formActivoInicial"
        >
          <VCol
            cols="12"
            class="d-flex gap-4 justify-end"
          >
            <VRow>
              <VCol
                cols="12"
                md="12"
              >
                <VTextField
                  v-model="activoInicialAmodificar"
                  label="Activo Inicial"
                  active
                  :rules="[reglaObligatoria()]"
                  type="number"
                >
                </VTextField>
              </VCol>
              <VCol
                cols="12"
                md="12"
                class="d-flex gap-4 justify-end"
              >
                <VBtn
                  color="secondary"
                  variant="outlined"
                  @click="cancelarDialogActivoInicial"
                >
                  CANCELAR
                </VBtn>
                <VBtn
                  type="submit"
                >
                  MODIFICAR
                </VBtn>
              </VCol>
            </VRow>
          </VCol>
        </VForm>
      </VCard>
    </VDialog>
    <h1 class="mt-16 border">Configuración de billetes pendiente</h1>
  </div>
</template>

