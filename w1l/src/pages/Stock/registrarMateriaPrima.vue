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
const formAgregarArticulo = ref(null);
const unidadDeMedida = ref(null);
const articulos = ref(null);
const articuloNuevo = ref(null);
const listadoArticulosNuevos = ref([]);

const dialog = ref(false);

const unidadesDeMedida = ref([]);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const fetchArticulos = async () => {
    try {
        axios.get('/Articulo/GetAllArticulo').then(response => {
            articulos.value = response.data;
        });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
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
    fetchUMD();
    fetchArticulos();
});

const registrarIngrediente = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    nombreIngrediente: nombre.value,
                    stockMinimo: stockMinimo.value || 0,
                    stockActual: stockInicial.value || 0,
                    detalleIngrediente: detalle.value || '',
                    idUMD: unidadDeMedida.value.idUMD
                };
                // axios.post('/Ingrediente', params).then(() => {
                //     registroExitosoMensaje('ingrediente', currentTheme.value);
                //     form.value.reset();
                // });
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
    formAgregarArticulo.value.validate().then(response => {
        if (response.valid) {
            listadoArticulosNuevos.value.push(articuloNuevo.value);
            articuloNuevo.value = null;
            dialog.value = !dialog;
        }
    });
};

const eliminarArticulo = (idArticulo) => {
    const index = listadoArticulosNuevos.value.findIndex(a => a.idArticulo === idArticulo)
    if (index !== -1) {
        listadoArticulosNuevos.value.splice(index, 1)
    }
}

const cerrarModal = () => {
    emit('cerrarDialogo');
};

const cancelarDialog = () => {
    articuloNuevo.value = null;
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
                    <!-- Tabla de artículos -->
                    <VCol
                        v-if="listadoArticulosNuevos.length > 0"
                        cols="12"
                        md="12"
                    >
                        <h2 class="pb-3 mt-3">Artículos relacionados</h2>
                        <VTable>
                            <thead>
                                <tr>
                                    <th class="text-uppercase">NOMBRE</th>
                                    <th class="text-uppercase">MARCA</th>
                                    <th class="text-uppercase">OPCIONES</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr
                                    v-for="(item) in listadoArticulosNuevos"
                                    :key="item.idArticulo"
                                >
                                    <td>{{ item.nombreArticulo.charAt(0).toUpperCase() + item.nombreArticulo.slice(1) }}</td>
                                    <td>{{ item.marcaArticulo.toUpperCase() }}</td>
                                    <td> 
                                        <IconBtn
                                            icon="ri-delete-bin-5-fill"
                                            color="error-darken-1"
                                            class="me-1"
                                            @click="
                                                eliminarArticulo(item.idArticulo)
                                            "
                                        />
                                    </td>
                                </tr>
                            </tbody>
                        </VTable>
                    </VCol>
                    <!-- Botones -->
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
                        ref="formAgregarArticulo"
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
                                        :items="articulos"
                                        :rules="[reglaObligatoria()]"
                                        item-title="nombreArticulo"
                                        v-model="articuloNuevo"
                                        return-object
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
