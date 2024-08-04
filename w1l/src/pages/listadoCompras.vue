<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { useTheme } from 'vuetify';
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';
import registrarCompra from '@/pages/registrarCompra.vue';

const compras = ref([]);
const proveedores = ref([]);
const productos = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditarCompra = ref({});
const search = ref('');
const loading = ref(false);
const titulosTabla = ref([]);

const itemsPuestos = [
    {
        id: 1,
        nombre: 'Colaborador'
    },
    {
        id: 2,
        nombre: 'Encargado general'
    },
    {
        id: 3,
        nombre: 'Dueño'
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
        .get('/Compra/GetAllCompras')
        .then(response => {
            compras.value = response.data.map(compra => {
                compra.fechaCompra = compra.fechaCompra.slice(0, 10);
                compra.totalConSigno = `$${compra.total}`;
                return compra;
            });
            loading.value = false;
        })
        .catch(error => {
            console.error('Error fetching compras:', error);
            loading.value = false;
        });
};

const fetchProveedores = async () => {
    try {
        axios.get('/Proveedor/GetAllProveedores').then(response => {
            proveedores.value = response.data;
            compras.value = compras.value.map(compra => ({
                proveedor: proveedores.value.find(prov => prov.id == compra.idProveedor),
                ...compra
            }));
            titulosTabla.value = [
                {
                    key: 'idCompra',
                    sortable: false,
                    title: 'ID'
                },
                {
                    key: 'fechaCompra',
                    sortable: false,
                    title: 'FECHA'
                },
                {
                    key: 'codigoComprobante',
                    sortable: false,
                    title: 'CÓDIGO DE COMPROBANTE'
                },
                {
                    key: 'proveedor.nombre',
                    sortable: false,
                    title: 'PROVEEDOR'
                },
                {
                    key: 'totalConSigno',
                    sortable: false,
                    title: 'TOTAL'
                },
                {
                    key: 'opciones',
                    sortable: false,
                    title: 'OPCIONES'
                }
            ];
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

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
    await fetchData();
    await fetchArticulos();
    await fetchProveedores();
});

const eliminar = function (id) {
    try {
        axios
            .delete(`/Compra/DeleteCompra/${id}`)
            .then(response => {
                fetchData();
                fetchProveedores();
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
            });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const openDialog = item => {
    itemEditarCompra.value = { ...item };
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
    const contenidoPDF = JSON.parse(JSON.stringify(compras.value));
    contenidoPDF.forEach((con) => {
        con.proveedor = con.proveedor.nombre
    })
    descargarPDF(titulosTabla.value, contenidoPDF, 'compras');
};

const confirmarModificacion = event => {
    try {
        let params = {
            idCompra: event.idCompra,
            fechaCompra: event.fechaCompra,
            idProveedor: event.idProveedor,
            codigoComprobante: event.codigoComprobante,
            total: event.total,
            detallesCompra: event.detallesCompra
        };
        console.log(params);
        axios.put(`/Compra/UpdateCompra/${params.idCompra}`, params).then(async () => {
            dialog.value = false;
            await fetchData();
            fetchProveedores();
            fetchArticulos();
            registroExitosoMensaje('compra', currentTheme.value);
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
                <VIcon icon="ri-list-unordered"></VIcon> &nbsp; Listado de compras
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
                :items="compras"
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
                                item.idCompra,
                                'Compra(' + item.idCompra + ')',
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
            <registrarCompra
                :esModalDetalle="true"
                :datosRegistro="itemEditarCompra"
                @cerrarDialogo="dialog = !dialog"
                @confirmarDialogo="confirmarModificacion"
            />
        </VDialog>
    </VCard>
</template>
