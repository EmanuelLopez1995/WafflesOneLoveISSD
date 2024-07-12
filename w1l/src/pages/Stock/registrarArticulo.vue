<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref, watch } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const props = defineProps({
    esModalDetalle: Boolean,
    datosRegistro: Object,
    esModal: Boolean
});
const emit = defineEmits(['cerrarDialogo']);

const nombre = ref('');
const stockMinimo = ref(0);
const stockInicial = ref(0);
const detalle = ref('');
const unidadDeMedida = ref(null);
const marca = ref('');
const pesoVolumen = ref(0);
const esIngrediente = ref(false);
const ingrediente = ref(null);
const ingredientes = ref(null);

const form = ref(null);

const unidadesDeMedida = ref([]);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const cargarDatosRegistroArticulo = () => {
    if (props.esModalDetalle) {
        nombre.value = props.datosRegistro.nombreArticulo;
        marca.value = props.datosRegistro.marcaArticulo;
        stockMinimo.value = props.datosRegistro.stockMinimo;
        stockInicial.value = props.datosRegistro.stockActual;
        unidadDeMedida.value = props.datosRegistro.umd;
        detalle.value = props.datosRegistro.detalleArticulo;
        esIngrediente.value = props.datosRegistro.esMateriaPrima;
        pesoVolumen.value = props.datosRegistro.pesoArticulo;
        ingrediente.value = props.datosRegistro.ingrediente;
    }
};

const confirmarModificacion = () => {
    const params = crearParams();
    params.idArticulo = props.datosRegistro.idArticulo;
    emit('confirmarDialogo', params);
};

const crearParams = () => {
    let params = {
        nombreArticulo: nombre.value,
        marcaArticulo: marca.value,
        stockMinimo: stockMinimo.value,
        stockActual: stockInicial.value,
        esMateriaPrima: esIngrediente.value,
        detalleArticulo: detalle.value,
        idUMD: unidadDeMedida.value.idUMD
    };
    if (esIngrediente.value) {
        params.pesoArticulo = pesoVolumen.value;
        params.idIngrediente = ingrediente.value.idIngrediente;
    }

    return params;
};

const fetchUMD = () => {
    try {
        axios
            .get('/UMD/GetAllUMDs')
            .then(response => {
                unidadesDeMedida.value = response.data;
                unidadesDeMedida.value.forEach(item => {
                    item.nombreUMD = item.nombreUMD.charAt(0).toUpperCase() + item.nombreUMD.slice(1);
                });
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
            });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const fetchIngredientes = async () => {
    try {
        await axios
            .get('/Ingrediente')
            .then(response => {
                ingredientes.value = response.data;
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
            });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

onMounted(async () => {
    await fetchIngredientes();
    await fetchUMD();
    cargarDatosRegistroArticulo();
});

const registrarArticulo = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = crearParams();
                axios
                    .post('/Articulo/AddArticulo', params)
                    .then(() => {
                        registroExitosoMensaje('artículo', currentTheme.value);
                        form.value.reset();
                    })
                    .catch(() => {
                        algoSalioMalError(currentTheme.value);
                    });
            } catch (error) {
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};
</script>

<template>
    <VCard>
        <VCardItem>
            <h2 class="pb-3 mt-3">Registrar Artículo</h2>
            <VForm
                @submit.prevent="registrarArticulo"
                ref="form"
                class="pt-2"
            >
                <VRow>
                    <VCol
                        cols="12"
                        md="6"
                    >
                        <VTextField
                            v-model="nombre"
                            :rules="[reglaObligatoria()]"
                            label="Nombre del artículo"
                            placeholder="Manteca 500grs, Leche larga vida 1lt, etc"
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
                            placeholder="La Paulina, Ilolay, Coca Cola, etc."
                        />
                    </VCol>

                    <VCol
                        cols="12"
                        md="6"
                    >
                        <VTextField
                            v-model="stockMinimo"
                            :rules="[reglaObligatoria()]"
                            :label="unidadDeMedida ? 'Stock mínimo en ' + unidadDeMedida.nombreUMD : 'Stock mínimo'"
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
                            :label="unidadDeMedida ? 'Stock inicial en ' + unidadDeMedida.nombreUMD : 'Stock inicial'"
                        />
                    </VCol>

                    <VCol
                        cols="12"
                        md="6"
                    >
                        <VSelect
                            :items="unidadesDeMedida"
                            item-title="nombreUMD"
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
                        <VCheckbox
                            class="font-weight-medium pl-3"
                            v-model="esIngrediente"
                            label="ES UN INGREDIENTE?"
                        />
                    </VCol>

                    <VCol
                        cols="12"
                        md="6"
                    >
                        <VTextField
                            v-if="esIngrediente"
                            v-model="pesoVolumen"
                            :rules="[reglaObligatoria()]"
                            type="number"
                            label="Peso/Volúmen"
                        />
                    </VCol>
                    <VCol
                        v-if="esIngrediente"
                        cols="12"
                        md="6"
                    >
                        <VSelect
                            :items="ingredientes"
                            :rules="[reglaObligatoria()]"
                            v-model="ingrediente"
                            label="Seleccione el ingrediente"
                            item-title="nombreIngrediente"
                            return-object
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
                            Registrar
                        </VBtn>
                    </VCol>
                </VRow>
            </VForm>
        </VCardItem>
    </VCard>
</template>
