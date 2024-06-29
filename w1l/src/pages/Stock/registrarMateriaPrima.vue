<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const props = defineProps(['esModal']);
const emit = defineEmits(['cerrarDialogo']);

const nombre = ref('');
const stockMinimo = ref('');
const stockInicial = ref(0);
const detalle = ref('');
const form = ref(null);
const unidadDeMedida = ref(null);

const dialog = ref(false);

const unidadesDeMedida = ref([]);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const fetchArticulos = async () => {
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

onMounted(() => {
    fetchArticulos();
    fetchUMD();
});

const registrarIngrediente = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    nombreIngrediente: nombre.value,
                    stockMinimo: stockMinimo.value || 0,
                    stockActual: stockInicial.value || 0,
                    detalleIngrediente: detalle.value || "",
                    idUMD: unidadDeMedida.value.idUMD
                };
                axios.post('/Ingrediente', params).then(() => {
                    registroExitosoMensaje('ingrediente', currentTheme.value);
                    form.value.reset();
                });
            } catch (error) {
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};

const abrirModalAgregarArticulo = () => {
    dialog.value = !dialog.value;
};

const agregarArticuloModal = () => {
    formAgregarProducto.value.validate().then(response => {
        if (response.valid) {
            // productoSeleccionado.value.subtotal =
            //   productoSeleccionado.value.cantidad * productoSeleccionado.value.precioUnitario
            //   const copiaProducto = { ...productoSeleccionado.value }
            // if(!esEditar.value) {
            //   //AGREGAR NUEVO PRODUCTO A LISTA
            //   productosSeleccionados.value.push(copiaProducto)
            // } else {
            //   // MODIFICAR PRODUCTO DE LISTA
            //   productosSeleccionados.value[indexAEditar.value] = copiaProducto;
            // }
            // productoSeleccionado.value = {}
            // calcularTotal()
            // dialog.value = !dialog
        }
    });
};

const cerrarModal = () => {
    emit('cerrarDialogo');
};

const cancelarDialog = () => {
    // productoSeleccionado.value = {}
    dialog.value = !dialog.value;
};
</script>

<template>
    <VCard>
        <VCardItem>
            <h2 class="pb-3 mt-3">Registrar Ingrediente</h2>
            <VForm
                @submit.prevent="registrarIngrediente"
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
                            label="Nombre del ingrediente"
                            placeholder="Leche, Manteca, etc."
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
                        <VTextField
                            v-model="stockInicial"
                            :rules="[reglaObligatoria()]"
                            type="number"
                            label="Stock Inicial"
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
                            color="primary"
                            @click="abrirModalAgregarArticulo"
                        >
                            Agregar artículo
                        </VBtn>
                        <VBtn
                            v-if="props.esModal"
                            color="secondary"
                            variant="outlined"
                            @click="cerrarModal"
                        >
                            CANCELAR
                        </VBtn>
                        <VBtn type="submit"> Registrar </VBtn>
                    </VCol>
                </VRow>
            </VForm>

            <!-- Modal carga producto -->
            <VDialog
                persistent
                v-model="dialog"
                width="40%"
            >
                <VCard
                    prepend-icon="ri-add-large-fill"
                    title="Agregar artículo"
                    class="px-5"
                >
                    <VForm
                        @submit.prevent="agregarArticuloModal"
                        ref="formAgregarProducto"
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
                                    <VSelect
                                        :items="['asd', 'qwe']"
                                        :rules="[reglaObligatoria()]"
                                        label="Artículo"
                                        placeholder="Seleccione el artículo a agregar"
                                    >
                                    </VSelect>
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="12"
                                    class="d-flex gap-4 justify-end"
                                >
                                    <VBtn
                                        color="secondary"
                                        variant="outlined"
                                        @click="cancelarDialog"
                                    >
                                        CANCELAR
                                    </VBtn>
                                    <VBtn type="submit"> AGREGAR </VBtn>
                                </VCol>
                            </VRow>
                        </VCol>
                    </VForm>
                </VCard>
            </VDialog>
        </VCardItem>
    </VCard>
</template>
