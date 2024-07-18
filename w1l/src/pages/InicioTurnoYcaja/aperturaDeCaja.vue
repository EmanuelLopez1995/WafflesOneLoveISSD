<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref, defineEmits, defineProps } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';
import useBilletes from '@/composables/useBilletes.js';

const emit = defineEmits(['inicioTurno', 'backToInicioDeTurno', 'irAResumen']);

const props = defineProps({
    empleadosPresentes: {
        type: Array,
        required: true
    }
});

const billetes = ref([]);
const cantidadBilletes = ref([]);
const sumaBilletes = ref([]);
const valorTotal = ref(0);
const encargadoDeAperturaDeCaja = ref(null);
const activoInicial = ref(0);
const aperturaCorrecta = ref(null);
const { fetchBilletes } = useBilletes();
const billetesBD = ref([]);

const form = ref(null);
const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const obtenerActivoInicial = () => {
    axios
        .get('/ActivoInicial/GetActivoInicial/1')
        .then(response => {
            activoInicial.value = response.data.montoActivoInicial;
        })
        .catch(error => {
            algoSalioMalError(currentTheme.value);
        });
};

onMounted(async () => {
    obtenerActivoInicial();
    obtenerBilletes();
});

const obtenerBilletes = async () => {
    billetesBD.value = await fetchBilletes();
    billetes.value = billetesBD.value.map(billete => billete.valorBillete)
}

const sumarValores = index => {
    sumaBilletes.value[index] = cantidadBilletes.value[index] * billetes.value[index];
    valorTotal.value = sumaBilletes.value.reduce((suma, numero) => suma + numero, 0);
    verificarAperturaCorrecta();
};

const verificarAperturaCorrecta = () => {
    if (valorTotal.value === activoInicial.value) {
        aperturaCorrecta.value = true;
    } else {
        aperturaCorrecta.value = false;
    }
};

const irAResumen = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            emit('irAResumen', { valorTotal, encargadoDeAperturaDeCaja, activoInicial, aperturaCorrecta });
        }
    });
};
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
                        md="4"
                    >
                        <VTable>
                            <thead>
                                <tr>
                                    <th class="text-uppercase">Billete</th>
                                    <th class="text-uppercase text-center">Cantidad</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr
                                    v-for="(billete, index) in billetes"
                                    :key="billete"
                                >
                                    <td>
                                        <b> ${{ billete }} </b>
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
                                    <td><b> Total </b></td>
                                    <td>
                                        <b> ${{ valorTotal }} </b>
                                    </td>
                                </tr>
                            </tbody>
                        </VTable>
                    </VCol>
                    <VCol
                        cols="12"
                        md="8"
                    >
                        <VRow>
                            <VCol
                                cols="12"
                                md="12"
                            >
                                <VSelect
                                    v-model="encargadoDeAperturaDeCaja"
                                    :items="props.empleadosPresentes"
                                    item-title="nombreCompletoYid"
                                    return-object
                                    :rules="[reglaObligatoria()]"
                                    label="Encargado de apertura de caja"
                                    placeholder="Encargado de apertura de caja"
                                />
                            </VCol>
                            <VCol
                                cols="12"
                                md="12"
                            >
                                <h3>Activo inicial: ${{ activoInicial }}</h3>
                            </VCol>
                            <VCol
                                cols="12"
                                md="12"
                            >
                                <VChip
                                    v-if="aperturaCorrecta"
                                    color="success"
                                >
                                    Apertura correcta
                                </VChip>
                                <VChip
                                    v-else-if="aperturaCorrecta == false"
                                    color="error"
                                >
                                    Apertura incorrecta
                                </VChip>
                            </VCol>
                        </VRow>
                    </VCol>
                    <VCol
                        cols="12"
                        md="8"
                        class="d-flex gap-4"
                    >
                        <VBtn
                            prepend-icon="ri-arrow-left-line"
                            @click="emit('backToInicioDeTurno')"
                        >
                            Atrás
                        </VBtn>
                        <VBtn
                            type="submit"
                            append-icon="ri-arrow-right-line"
                        >
                            Siguiente
                        </VBtn>
                    </VCol>
                </VRow>
            </VForm>
        </VCardItem>
    </VCard>
</template>
