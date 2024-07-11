<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { useTheme } from 'vuetify';
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';
import RegistrarIngrediente from '@/pages/Stock/registrarMateriaPrima.vue';

const ingredientes = ref([]);
const proveedores = ref([]);
const productos = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditarIngrediente = ref({});
const search = ref('');
const loading = ref(false);
const titulosTabla = [
    {
        key: 'idIngrediente',
        sortable: false,
        title: 'ID'
    },
    {
        key: 'nombreIngrediente',
        sortable: false,
        title: 'NOMBRE'
    },
    {
        key: 'detalleIngrediente',
        sortable: false,
        title: 'DETALLE'
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

// const fetchProveedores = async () => {
//     try {
//         axios.get('/Proveedor/GetAllProveedores').then(response => {
//             proveedores.value = response.data;
//             compras.value = compras.value.map(compra => ({
//                 proveedor: proveedores.value.find(prov => prov.id == compra.idProveedor),
//                 ...compra
//             }));
//         });
//     } catch (error) {
//         algoSalioMalError(currentTheme.value);
//     }
// };

const fetchArticulos = () => {
    try {
        axios.get('/Articulo/GetAllArticulo').then(response => {
            productos.value = response.data;
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

onMounted(async () => {
    await fetchArticulos();
    fetchData();
    // fetchProveedores();
});

const eliminar = function (id) {
    try {
        axios
            .delete(`/Ingrediente/${id}`)
            .then(async (response) => {
                await fetchArticulos();
                fetchData();
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
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
    descargarPDF(titulosTabla, ingredientes.value, 'ingredientes');
};

const confirmarModificacion = event => {
    try {
        // axios.put(`/Ingrediente/${event.idIngrediente}`, event).then(async () => {
        //     dialog.value = false;
        //     fetchArticulos();
        //     await fetchData();
        //     registroExitosoMensaje('ingrediente', currentTheme.value);
        // });
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
                <VIcon icon="ri-list-unordered"></VIcon> &nbsp; Listado de ingredientes
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
                :items="ingredientes"
                :headers="titulosTabla"
                :items-per-page-options="paginas"
                items-per-page-text="Items por página:"
                no-data-text="No hay registros"
                :loading="loading"
                loading-text="Cargando..."
            >
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
                                item.idIngrediente,
                                'Ingrediente(' + item.idIngrediente + ')',
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
            <RegistrarIngrediente
                :esModalDetalle="true"
                :datosRegistro="itemEditarIngrediente"
                @cerrarDialogo="dialog = !dialog"
                @confirmarDialogo="confirmarModificacion"
            />
        </VDialog>
    </VCard>
</template>
