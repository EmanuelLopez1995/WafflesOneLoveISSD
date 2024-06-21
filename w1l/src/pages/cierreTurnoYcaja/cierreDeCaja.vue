<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, defineEmits, defineProps } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const emit = defineEmits(['inicioTurno', 'backToCierreDeTurno', 'irAResumen'])

const props = defineProps({
  empleadosPresentes: {
    type: Array,
    required: true
  }
});

const billetes = ref([2000, 1000, 500, 200, 100, 50, 20, 10])
const cantidadBilletes = ref([])
const sumaBilletes = ref([])
const valorTotal = ref(0)
const encargadoDeCierreDeCaja = ref(null)
const activoInicial = ref(0)
const cierreCorrecto = ref(null)
const totalVentas = ref(90100)

const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const obtenerActivoInicial = () => {
  axios.get('/ActivoInicial/GetActivoInicial/1')
    .then((response) => {
      activoInicial.value = response.data.montoActivoInicial;
    })
    .catch((error) => {
      algoSalioMalError(currentTheme.value);
    });
}

onMounted(() => {
  obtenerActivoInicial();
})

const sumarValores = (index) => {
    sumaBilletes.value[index] = cantidadBilletes.value[index] * billetes.value[index];
    valorTotal.value = sumaBilletes.value.reduce((suma, numero) => suma + numero, 0);
    verificarcierreCorrecto();
}

const verificarcierreCorrecto = () => {
    if(valorTotal.value === totalVentas.value) {
        cierreCorrecto.value = true;
    } else {
        cierreCorrecto.value = false;
    }
}

const irAResumen = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            emit('irAResumen', {valorTotal, encargadoDeCierreDeCaja, activoInicial, cierreCorrecto })
        } 
    })
}

</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="irAResumen"
        class="pt-5"
        ref="form"
      >
        <VRow>
            <VCol
                cols="12"
                md="6"
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
                            <td><b>
                                ${{ billete }}
                            </b></td>
                            <td>
                                <VTextField
                                    v-model="cantidadBilletes[index]"
                                    type="number"
                                    @input="sumarValores(index)"
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><b>
                                Total
                            </b></td>
                            <td><b>
                                ${{valorTotal}}
                            </b></td>
                        </tr>
                    </tbody>
                </VTable>
            </VCol>
            <VCol
                cols="12"
                md="6"
            >
                <VRow>
                    <VCol
                        cols="12"
                        md="12"
                    >
                        <VSelect
                            v-model="encargadoDeCierreDeCaja"
                            :items="props.empleadosPresentes"
                            item-title="nombreCompleto"
                            return-object
                            :rules="[reglaObligatoria()]"
                            label="Encargado de cierre de caja"
                            placeholder="Encargado de cierre de caja"
                        />
                    </VCol>
                    <VCol
                        cols="12"
                        md="12"
                    >
                        <h3>Activo inicial próximo turno: ${{activoInicial}}</h3>
                        <h3>Total en ventas del turno: ${{ totalVentas }}</h3>
                        <h3>Retiro de efectivo: ${{ valorTotal - activoInicial }}</h3>
                    </VCol>
                    <VCol
                        cols="12"
                        md="12"
                    >
                        <VChip v-if="cierreCorrecto" color="success">
                            Cierre correcto
                        </VChip>
                        <VChip v-else-if="cierreCorrecto == false" color="error">
                            Cierre incorrecto
                        </VChip>
                    </VCol>
                </VRow>
            </VCol>
            <VCol
                cols="12"
                md="8"
                class="d-flex gap-4"
            >
                <VBtn prepend-icon="ri-arrow-left-line" @click="emit('backToCierreDeTurno')"> Atrás </VBtn>
                <VBtn type="submit" append-icon="ri-arrow-right-line"> Siguiente </VBtn>
            </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
