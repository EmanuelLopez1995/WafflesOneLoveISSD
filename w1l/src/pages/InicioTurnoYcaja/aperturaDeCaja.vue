<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, defineEmits } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const emit = defineEmits(['inicioTurno'])

const billetes = ref([2000, 1000, 500, 200, 100, 50, 20, 10])
const cantidadBilletes = ref([])
const sumaBilletes = ref([])
const valorTotal = ref(null)

const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const sumarValores = (index) => {
    sumaBilletes.value[index] = cantidadBilletes.value[index] * billetes.value[index];
    valorTotal.value = sumaBilletes.value.reduce((suma, numero) => suma + numero, 0);
    verificarAperturaCorrecta();
}

const verificarAperturaCorrecta = () => {

}

const abrirCaja = () => {

}

</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="abrirCaja"
        class="pt-5"
        ref="form"
      >
        <VRow>
            <VCol
                cols="12"
                md="4"
            >
                <VTable>
                    <thead>
                    <tr>
                        <th class="text-uppercase">
                        Billete
                        </th>
                        <th class="text-uppercase text-center">
                        Cantidad
                        </th>
                    </tr>
                    </thead>

                    <tbody>
                        <tr v-for="(billete, index) in billetes" :key="billete">
                            <td>
                                ${{ billete }}
                            </td>
                            <td>
                                <VTextField
                                    v-model="cantidadBilletes[index]"
                                    type="number"
                                    @input="sumarValores(index)"
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total
                            </td>
                            <td>
                                ${{valorTotal}}
                            </td>
                        </tr>
                    </tbody>
                </VTable>
            </VCol>
        </VRow>
            <VBtn type="submit"> Registrar </VBtn>
      </VForm>
    </VCardItem>
  </VCard>
</template>
