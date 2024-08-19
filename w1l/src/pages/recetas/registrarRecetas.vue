<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje, warningMessage } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const props = defineProps({
    esModalDetalle: Boolean,
    datosRegistro: Object
});

const emit = defineEmits(['cerrarDialogo', 'confirmarDialogo']);
const form = ref(null);
const formIngredientes = ref(null);

const nombreReceta = ref(null);
const procedimiento = ref(null);
const ingredienteSeleccionado = ref(null);
const cantidadSeleccionada = ref(null);
const unidadDeMedidaSeleccionada = ref(null);

const ingredientesAgregados = ref([]);

const ingredientesDB = ref([]);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const fetchIngredientes = async () => {
    try {
        await axios.get('/Ingrediente').then(response => {
            ingredientesDB.value = response.data;
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

const cargarDatosRegistroReceta = async () => {
    if (props.esModalDetalle) {
        nombreReceta.value = props.datosRegistro.nombreReceta;
        procedimiento.value = props.datosRegistro.procedimiento;

        ingredientesAgregados.value = await Promise.all(
            props.datosRegistro.ingredientes.map(async ing => {
                const ingredienteCompleto = ingredientesDB.value.find(item => ing.idIngrediente == item.idIngrediente);
                const umd = await verificarUMD(ingredienteCompleto);
                return {
                    idIngrediente: ing.idIngrediente,
                    cantidadSeleccionada: ing.cantidad,
                    nombreIngrediente: ingredienteCompleto.nombreIngrediente,
                    udm: umd
                };
            })
        );
    }
};

const crearParams = () => {
    const ingredientesFormat = ingredientesAgregados.value.map(ingrediente => {
        return {
            cantidad: parseInt(ingrediente.cantidadSeleccionada),
            idIngrediente: ingrediente.idIngrediente
        };
    });
    return {
        idReceta: props.datosRegistro ? props.datosRegistro.idReceta : 0,
        nombreReceta: nombreReceta.value,
        procedimiento: procedimiento.value || '',
        ingredientes: ingredientesFormat
    };
};

const registrarReceta = () => {
    form.value.validate().then(async response => {
        if (response.valid) {
            if (ingredientesAgregados.value.length == 0) {
                warningMessage('Debe agregar al menos 1 ingrediente', currentTheme.value.value);
                return;
            } else {
                try {
                    const params = crearParams();
                    await axios.post('/Receta', params).then(() => {
                        form.value.reset();
                        ingredientesAgregados.value = [];
                        registroExitosoMensaje('compra', currentTheme.value);
                    });
                } catch (error) {
                    algoSalioMalError(currentTheme.value);
                }
            }
        }
    });
};
const agregarIngrediente = () => {
    formIngredientes.value.validate().then(response => {
        if (response.valid) {
            ingredienteSeleccionado.value.cantidadSeleccionada = cantidadSeleccionada.value;
            ingredienteSeleccionado.value.udm = unidadDeMedidaSeleccionada.value;
            ingredientesAgregados.value.push({ ...ingredienteSeleccionado.value });
            //Limpiamos
            ingredienteSeleccionado.value = null;
            cantidadSeleccionada.value = null;
            unidadDeMedidaSeleccionada.value = null;
        }
    });
};

const actualizarUnidadDeMedida = async () => {
    const nombreCortoUMD = await verificarUMD();
    unidadDeMedidaSeleccionada.value = nombreCortoUMD;
};

const verificarUMD = async ingrediente => {
    try {
        let primerArticuloId;
        if (ingrediente) {
            primerArticuloId = ingrediente.idsArticulos[0];
        } else {
            primerArticuloId = ingredienteSeleccionado.value.idsArticulos[0];
        }
        //VALIDAR SI NO TIENE ARTICULOS RELACIONADOS
        const results = await axios.get(`/Articulo/GetArticuloPorId/${primerArticuloId}`);
        const idUMD = results.data.idUMD;
        const resultsUMD = await axios.get(`/UMD/GetUMDPorId/${idUMD}`);
        return resultsUMD.data.nombreCortoUMD;
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

const eliminarProductoDeLista = index => {
    if (index !== -1) {
        ingredientesAgregados.value.splice(index, 1);
    }
};

const confirmarModificacion = () => {
    if (ingredientesAgregados.value.length == 0) {
        warningMessage('Debe agregar al menos 1 ingrediente', currentTheme.value.value);
        return;
    } else {
        const params = crearParams();
        emit('confirmarDialogo', params);
    }
};

onMounted(async () => {
    await fetchIngredientes();
    await cargarDatosRegistroReceta();
});
</script>

<template>
    <VCard>
        <VCardItem>
            <h2 class="pb-3 mt-3">Registrar Receta</h2>
            <VForm
                @submit.prevent="registrarReceta"
                ref="form"
                class="pt-2"
            >
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
                    <VCol
                        cols="12"
                        md="8"
                    ></VCol>
                    <VCol
                        cols="12"
                        md="8"
                    >
                        <VTextarea
                            label="Procedimiento"
                            placeholder="Ingrese los pasos a seguir"
                            v-model="procedimiento"
                        ></VTextarea>
                    </VCol>
                    <VCol
                        cols="12"
                        md="12"
                    >
                        <VForm
                            @submit.prevent="agregarIngrediente"
                            ref="formIngredientes"
                        >
                            <VRow>
                                <VCol
                                    cols="12"
                                    md="3"
                                >
                                    <VSelect
                                        :items="ingredientesDB"
                                        item-title="nombreIngrediente"
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
                                            <span>{{ unidadDeMedidaSeleccionada }}</span>
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
                                        {{ item.nombreIngrediente }}
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
                        <VBtn
                            v-if="props.esModalDetalle"
                            color="secondary"
                            variant="outlined"
                            @click="emit('cerrarDialogo')"
                        >
                            CANCELAR
                        </VBtn>
                        <VBtn
                            v-if="props.esModalDetalle"
                            @click="confirmarModificacion"
                        >
                            MODIFICAR
                        </VBtn>
                        <VBtn
                            v-if="!props.esModalDetalle"
                            type="submit"
                        >
                            REGISTRAR
                        </VBtn>
                    </VCol>
                </VRow>
            </VForm>
        </VCardItem>
    </VCard>
</template>
