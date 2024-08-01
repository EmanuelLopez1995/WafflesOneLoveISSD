<script setup>
import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { useTheme } from 'vuetify';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import descargarPDF from '@/components/pdfHelper.js';
import resumenCierre from '@/pages/cierreTurnoYcaja/resumenCierre.vue';

const turnos = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemDetalle = ref({});
const search = ref('');
const loading = ref(false);
const allEmpleados = ref([]);

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors); // se accede con currentTheme.value.primary x ej
});

const fetchData = async () => {
    loading.value = true;
    try {
        axios.get('/Turno/GetAllTurnos').then(response => {
            turnos.value = response.data;
            agregarEncargadoDeTurnoCompleto();
            formatearGral();
            loading.value = false;
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
        loading.value = false;
    }
};

const getEmpleados = async () => {
    try {
        await axios.get('/Empleado/GetAllEmpleados').then(response => {
            allEmpleados.value = response.data.map(empleado => {
                return {
                    ...empleado,
                    nombreCompletoYid: `${empleado.nombreEmpleado} ${empleado.apellidoEmpleado} (${empleado.idEmpleado})`
                };
            });
        });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const agregarEncargadoDeTurnoCompleto = () => {
    turnos.value.forEach(turno => {
        turno.encargadoDeTurno = allEmpleados.value.find(emp => emp.idEmpleado === turno.idEncargadoTurno);
        turno.encargadoDeTurno.nombreCompleto = turno.encargadoDeTurno.nombreEmpleado + ' ' + turno.encargadoDeTurno.apellidoEmpleado;
    });
};

const formatearGral = () => {
  turnos.value.forEach((turno) => {
      turno.fecha = turno.fechaTurno.slice(0,10);
      turno.turno = turno.tipoTurno;
      turno.empleadosSeleccionados = turno.empleados;
      turno.empleadosSeleccionados.forEach((emp) => {
          const empleado = allEmpleados.value.find(empp => empp.idEmpleado == emp.idEmpleado);
          emp.nombreCompleto = empleado.nombreEmpleado + ' ' + empleado.apellidoEmpleado;
      })

      //encargados de apertura y cierre de caja
      turno.caja.encApertCaja = turno.empleadosSeleccionados.find(emp => emp.esRespDeApertCaja);
      turno.caja.encCierreCaja = turno.empleadosSeleccionados.find(emp => emp.esRespDeCierreCaja);
  })
}

onMounted(async () => {
    await getEmpleados();
    fetchData();
});

const openDialog = item => {
    itemDetalle.value = { ...item };
    dialog.value = true;
};
const closeDialog = () => {
    dialog.value = false;
};

const descargarListado = () => {
    console.log(turnos.value);
    const contenidoPDF = JSON.parse(JSON.stringify(turnos.value));
    contenidoPDF.forEach((tur) => {
        tur.esFeriado = tur.esFeriado ? 'Si' : 'No';
        tur.encargadoDeTurno = tur.encargadoDeTurno.nombreCompleto;
        tur.fechaTurno = tur.fechaTurno.slice(0,10);
    })
    descargarPDF(titulosTabla, contenidoPDF, 'turnos');
};

const titulosTabla = [
    {
        key: 'idTurno',
        sortable: false,
        title: 'ID'
    },
    {
        key: 'fechaTurno',
        sortable: true,
        title: 'FECHA'
    },
    {
        key: 'tipoTurno',
        sortable: false,
        title: 'TIPO'
    },
    {
        key: 'esFeriado',
        sortable: false,
        title: 'FERIADO'
    },
    {
        key: 'encargadoDeTurno',
        sortable: false,
        title: 'ENCARGADO'
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
</script>

<template>
    <VCard>
        <VCardItem>
            <VCardTitle class="d-flex align-center pe-2">
                <VIcon icon="ri-list-unordered"></VIcon> &nbsp; Listado de turnos

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
                :items="turnos"
                :headers="titulosTabla"
                :items-per-page-options="paginas"
                items-per-page-text="Items por página:"
                no-data-text="No hay registros"
                :loading="loading"
                loading-text="Cargando..."
            >
                <template v-slot:[`item.fechaTurno`]="{ item }">
                    {{ item.fechaTurno.slice(0, 10) }}
                </template>
                <template v-slot:[`item.esFeriado`]="{ item }">
                    {{ item.esFeriado ? 'Si' : 'No' }}
                </template>
                <template v-slot:[`item.encargadoDeTurno`]="{ item }">
                    {{ item.encargadoDeTurno.nombreCompletoYid }}
                </template>
                <template v-slot:[`item.opciones`]="{ item }">
                    <IconBtn
                        icon="ri-menu-search-line"
                        color="primary"
                        class="me-1"
                        @click="openDialog(item)"
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
        <!-- MODAL Detalle -->
        <VDialog
            v-model="dialog"
            width="60%"
        >
            <resumenCierre :turno="itemDetalle" :caja="itemDetalle.caja" :esModal="true" @cerrarModal="dialog = !dialog"/> 
        </VDialog>
    </VCard>
</template>
