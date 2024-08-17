<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref, watch} from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje, warningMessage } from '@/components/SwalCustom.js';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { useTheme } from 'vuetify';
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';
import RegistrarArticulo from '@/pages/Stock/registrarArticulo.vue';

const props = defineProps({
    tabKey: String
});

watch(() => props.tabKey, async (newVal, oldVal) => {
    if(newVal == 'listArt') {
        await fetchIngredientes();
        await obtenerUMDS();
        await fetchData();
    }
});

const articulos = ref([]);
const ingredientes = ref([]);
const umds = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditarIngrediente = ref({});
const search = ref('');
const loading = ref(false);
const titulosTabla = [
    {
        key: 'idArticulo',
        sortable: false,
        title: 'ID'
    },
    {
        key: 'nombreArticulo',
        sortable: false,
        title: 'NOMBRE'
    },
    {
        key: 'marcaArticulo',
        sortable: false,
        title: 'MARCA'
    },
    {
        key: 'detalleArticulo',
        sortable: false,
        title: 'DETALLE'
    },
    {
        key: 'ingrediente.nombreIngrediente',
        sortable: false,
        title: 'INGREDIENTE'
    },
    {
        key: 'stockMinimo',
        sortable: false,
        title: 'STOCK MÍNIMO'
    },
    {
        key: 'stockActual',
        sortable: false,
        title: 'EXISTENCIAS'
    },
    {
        key: 'umd.nombreCortoUMD',
        sortable: false,
        title: 'UMD'
    },
    {
        key: 'estado',
        sortable: false,
        title: 'ESTADO'
    },
    {
        key: 'opciones',
        sortable: false,
        title: 'OPCIONES'
    }
];

const paginas = [
    { value: 10, title: '10' },
    { value: 25, title: '25' },
    { value: 50, title: '50' },
    { value: 100, title: '100' },
    { value: -1, title: 'Todos' }
];

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const fetchData = async () => {
    loading.value = true;
    axios
        .get('/Articulo/GetAllArticulo')
        .then(response => {
            articulos.value = response.data;
            //setear ingredientes
            articulos.value = articulos.value.map(art => {
                let ingrediente = ingredientes.value.find(ing => ing.idIngrediente == art.idIngrediente);
                if (ingrediente) {
                    return {
                        ...art,
                        ingrediente
                    };
                } else {
                    return {
                        ...art,
                        ingrediente: { //Ver si hace falta
                            nombreIngrediente: '-------'
                        }
                    };
                }
            });
            //setear UMDS
            articulos.value = articulos.value.map(art => {
                let umd = umds.value.find(umd => umd.idUMD == art.idUMD);
                if (umd) {
                    return {
                        ...art,
                        umd
                    };
                } else {
                    return {
                        ...art
                    };
                }
            });
            //Sobrante
            articulos.value.forEach(art => {
                if (art.stockActual < art.stockMinimo) {
                    art.estado = 'Faltante';
                } else {
                    art.estado = 'Sobrante';
                }
            });
            loading.value = false;
        })
        .catch(error => {
            console.error('Error fetching articulos:', error);
            loading.value = false;
        });
};

const fetchIngredientes = async () => {
    loading.value = true;
    axios
        .get('/Ingrediente')
        .then(response => {
            ingredientes.value = response.data;
            loading.value = false;
        })
        .catch(error => {
            console.error('Error fetching ingredientes:', error);
            loading.value = false;
        });
};

const obtenerUMDS = () => {
    try {
        axios
            .get('/UMD/GetAllUMDs')
            .then(response => {
                umds.value = response.data;
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
            });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

onMounted(async () => {
    await fetchIngredientes();
    await obtenerUMDS();
    await fetchData();
});

const eliminar = function (id) {
    try {
        axios
            .delete(`/Articulo/DeleteArticulo/${id}`)
            .then(async response => {
                await fetchIngredientes();
                await obtenerUMDS();
                await fetchData();
            })
            .catch((error) => {
                if(error.response.data == 'No se puede eliminar el artículo porque está asociado a registros de compra.') {
                    warningMessage('No se puede eliminar el artículo porque está asociado a registros de compra.', currentTheme.value)
                } else {
                    algoSalioMalError(currentTheme.value);
                }
            });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const openDialog = item => {
    itemEditarIngrediente.value = { ...item };
    dialog.value = true;
};
const closeDialog = () => {
    dialog.value = false;
};

const guardarEdicionCompra = () => {
    dialog.value = false;
    try {
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const descargarListado = () => {
    const contenidoPDF = JSON.parse(JSON.stringify(articulos.value));
    contenidoPDF.forEach((con) => {
        con.ingrediente = con.ingrediente.nombreIngrediente;
        con.umd = con.umd.nombreCortoUMD;
    })
    descargarPDF(titulosTabla, contenidoPDF, 'articulos');
};

const confirmarModificacion = event => {
    try {
        axios.put(`/Articulo/UpdateArticulo/${event.idArticulo}`, event).then(async () => {
            dialog.value = false;
            await fetchIngredientes();
            await obtenerUMDS();
            await fetchData();
            registroExitosoMensaje('articulo', currentTheme.value);
        });
    } catch (error) {
        console.log(error);
        algoSalioMalError(currentTheme.value);
    }
};
</script>

<template>
    <VCard>
        <VCardItem>
            <VCardTitle class="d-flex align-center pe-2">
                <VIcon icon="ri-list-unordered"></VIcon> &nbsp; Listado de articulos
                <VSpacer></VSpacer>
                <VSpacer></VSpacer>

                <VTextField
                    v-model="search"
                    density="compact"
                    label="Buscar"
                    prepend-inner-icon="ri-search-line"
                    variant="solo-filled"
                    flat
                    hide-details
                    single-line
                ></VTextField>
            </VCardTitle>

            <VDataTable
                class="mt-5"
                :search="search"
                :items="articulos"
                :headers="titulosTabla"
                :items-per-page-options="paginas"
                items-per-page-text="Items por página:"
                no-data-text="No hay registros"
                :loading="loading"
                loading-text="Cargando..."
            >
                <template v-slot:[`item.estado`]="{ item }">
                    <VChip
                        :color="item.estado == 'Sobrante' ? 'success' : 'error'"
                        size="small"
                        variant="tonal"
                    >
                        {{ item.estado }}
                    </VChip>
                </template>
                <template v-slot:[`item.opciones`]="{ item }">
                    <IconBtn
                        icon="ri-menu-search-line"
                        color="primary"
                        class="me-1"
                        @click="openDialog(item)"
                    />
                    <IconBtn
                        icon="ri-delete-bin-5-fill"
                        color="error-darken-1"
                        class="me-1"
                        @click="
                            eliminarRegistro(
                                eliminar,
                                item.idArticulo,
                                'Ingrediente(' + item.idArticulo + ')',
                                currentTheme.value
                            )
                        "
                    />
                </template>
            </VDataTable>
        </VCardItem>
        <VCol
            cols="12"
            class="d-flex gap-4 justify-end"
        >
            <VBtn
                prepend-icon="ri-download-fill"
                @click="descargarListado"
            >
                Descargar PDF</VBtn
            >
        </VCol>

        <!-- MODAL PRIMER TEST -->
        <VDialog
            v-model="dialog"
            width="60%"
        >
            <RegistrarArticulo
                :esModalDetalle="true"
                :datosRegistro="itemEditarIngrediente"
                @cerrarDialogo="dialog = !dialog"
                @confirmarDialogo="confirmarModificacion"
            />
        </VDialog>
    </VCard>
</template>
