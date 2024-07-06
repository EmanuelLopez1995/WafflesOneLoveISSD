<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { useTheme } from 'vuetify';
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';

const compras = ref([]);
const proveedores = ref([]);
const productos = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditarCompra = ref({});
const search = ref('');
const loading = ref(false);
const titulosTabla = [
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
    fetchProveedores();
    fetchArticulos();
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
    descargarPDF(titulosTabla, compras.value, 'compras');
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

        <!-- MODAL EDITAR -->
        <VDialog
            v-model="dialog"
            width="50%"
        >
            <VCard
                prepend-icon="ri-edit-2-fill"
                title="Detalle"
                class="px-5"
            >
                <VForm
                    @submit.prevent="guardarEdicionCompra"
                    ref="form"
                >
                    <VRow>
                        <VCol
                            cols="12"
                            md="2"
                            class="flex-sm-column"
                        >
                            <VTextField
                                v-model="itemEditarCompra.idCompra"
                                :rules="[reglaObligatoria()]"
                                label="ID"
                                readonly
                            />
                        </VCol>
                        <VCol
                            cols="12"
                            md="5"
                        >
                            <VTextField
                                v-model="itemEditarCompra.fechaCompra"
                                :rules="[reglaObligatoria()]"
                                label="Fecha"
                                type="date"
                                readonly
                            />
                        </VCol>
                        <VCol
                            cols="12"
                            md="5"
                        >
                            <VSelect
                                v-model="itemEditarCompra.proveedor"
                                :rules="[reglaObligatoria()]"
                                :items="proveedores"
                                item-title="nombre"
                                return-object
                                label="Proveedor"
                                readonly
                            />
                        </VCol>
                    </VRow>

                    <!-- Segunda fila -->
                    <h3 class="pt-5 pb-3">Productos</h3>
                    <VRow
                        v-for="(detalle, index) in itemEditarCompra.detallesCompra"
                        :key="index"
                    >
                        <VCol
                            cols="12"
                            md="3"
                        >
                            <VSelect
                                v-model="detalle.idArticulo"
                                :items="productos"
                                item-title="nombreArticulo"
                                item-value="idArticulo"
                                label="Producto"
                                readonly
                            />
                        </VCol>
                        <VCol
                            cols="12"
                            md="3"
                        >
                            <VTextField
                                v-model="detalle.cantidad"
                                label="Cantidad"
                                type="number"
                                readonly
                            />
                        </VCol>
                        <VCol
                            cols="12"
                            md="3"
                        >
                            <VTextField
                                v-model="detalle.precioUnitario"
                                label="Precio Unitario"
                                type="number"
                                readonly
                            />
                        </VCol>
                        <VCol
                            cols="12"
                            md="3"
                        >
                            <VTextField
                                v-model="detalle.subtotal"
                                label="Subtotal"
                                type="number"
                                readonly
                            />
                        </VCol>
                    </VRow>
                    <VRow>
                        <VCol cols="12" md="3"></VCol>
                        <VCol cols="12" md="3"></VCol>
                        <VCol cols="12" md="3"></VCol>
                        <VCol
                            cols="12"
                            md="3"
                        >
                            <VTextField
                                v-model="itemEditarCompra.total"
                                :rules="[reglaObligatoria()]"
                                label="Total"
                                type="number"
                                readonly
                            />
                        </VCol>
                    </VRow>
                    <VCol
                        cols="12"
                        class="d-flex gap-4 justify-end mt-3"
                    >
                        <VBtn
                            color="secondary"
                            variant="outlined"
                            @click="closeDialog"
                        >
                            CERRAR
                        </VBtn>
                        <VBtn
                            color="primary"
                            variant="outlined"
                            type="submit"
                        >
                            EDITAR
                        </VBtn>
                    </VCol>
                </VForm>
            </VCard>
        </VDialog>
        <!-- Primera fila -->

        <!-- Tercera fila -->
        <!-- <VRow>
                <VCol
                    cols="12"
                    md="6"
                    class="flex-sm-column"
                >
                    <VTextField
                        v-model="itemEditarCompra.mailEmpleado"
                        :rules="[reglaObligatoria()]"
                        label="Email"
                        type="email"
                    />
                </VCol>
                <VCol
                    cols="12"
                    md="6"
                >
                    <VSelect
                        v-model="itemEditarCompra.puesto"
                        :items="itemsPuestos"
                        item-title="nombre"
                        return-object
                        :rules="[reglaObligatoria()]"
                        label="Puesto"
                    />
                </VCol>
            </VRow> -->
    </VCard>
</template>
