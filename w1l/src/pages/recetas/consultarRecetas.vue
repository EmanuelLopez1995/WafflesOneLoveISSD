<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { useTheme } from 'vuetify';
import descargarPDF from '@/components/pdfHelper.js';
import registrarReceta from '@/pages/recetas/registrarRecetas.vue';

const recetas = ref([]);
const proveedores = ref([]);
const productos = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditarReceta = ref({});
const search = ref('');
const loading = ref(false);

const titulosTabla = [
    {
        key: 'idReceta',
        sortable: false,
        title: 'ID'
    },
    {
        key: 'nombreReceta',
        sortable: true,
        title: 'NOMBRE'
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
    try {
        axios.get('/Receta').then(response => {
            recetas.value = response.data;
        });
        loading.value = false;
    } catch (error) {
        console.error('Error fetching Recetas:', error);
        algoSalioMalError(currentTheme.value);
        loading.value = false;
    }
};

onMounted(async () => {
    await fetchData();
});

const eliminar = function (id) {
    try {
        axios
            .delete(`/Receta/${id}`)
            .then(response => {
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
    itemEditarReceta.value = { ...item };
    dialog.value = true;
};

const descargarListado = () => {
    const contenidoPDF = JSON.parse(JSON.stringify(recetas.value));
    descargarPDF(titulosTabla, contenidoPDF, 'recetas');
};

const confirmarModificacion = event => {
    try {
        let params = {
            idReceta: event.idReceta,
            nombreReceta: event.nombreReceta,
            procedimiento: event.procedimiento,
            ingredientes: event.ingredientes
        };

        axios.put(`/Receta/${params.idReceta}`, params).then(async () => {
            dialog.value = false;
            await fetchData();
            registroExitosoMensaje('receta', currentTheme.value);
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
                <VIcon icon="ri-list-unordered"></VIcon> &nbsp; Listado de recetas
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
                :items="recetas"
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
                                item.idReceta,
                                'Receta(' + item.idReceta + ')',
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
            <registrarReceta
                :esModalDetalle="true"
                :datosRegistro="itemEditarReceta"
                @cerrarDialogo="dialog = !dialog"
                @confirmarDialogo="confirmarModificacion"
            />
        </VDialog>
    </VCard>
</template>
