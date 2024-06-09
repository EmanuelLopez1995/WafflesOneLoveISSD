<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { useTheme } from 'vuetify'
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';

const empleados = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditar = ref({});
const search = ref('');
const loading = ref(false);
const titulosTabla = [          
    {
      key: 'idEmpleado',
      sortable: false,
      title: 'ID',
    },
    {
      key: 'nombreEmpleado',
      sortable: false,
      title: 'NOMBRE',
    },
    {
      key: 'apellidoEmpleado',
      sortable: false,
      title: 'APELLIDO',
    },
    {
      key: 'dniEmpleado',
      sortable: false,
      title: 'DNI',
    },      
    {
      key: 'telefonoEmpleado',
      sortable: false,
      title: 'TELÉFONO',
    },
    {
      key: 'direccionEmpleado',
      sortable: false,
      title: 'DIRECCIÓN',
    }, 
    {
      key: 'mailEmpleado',
      sortable: false,
      title: 'EMAIL',
    },
    {
      key: 'puesto',
      sortable: false,
      title: 'PUESTO',
    },
    {
      key: 'opciones',
      sortable: false,
      title: 'OPCIONES',
    }
]

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
]

const paginas = [
    {value: 10, title: '10'},
    {value: 25, title: '25'},
    {value: 50, title: '50'},
    {value: 100, title: '100'},
    {value: -1, title: 'Todos'}
]

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const fetchData = async () => {
  loading.value = true;
  axios.get('/Empleado/GetAllEmpleados')
    .then((response) => {
        empleados.value = response.data.map(empleado => {
            const puesto = itemsPuestos.find(p => p.id === empleado.idPuestoEmpleado);
            return {
                ...empleado,
                puesto: puesto ? puesto.nombre : 'Puesto desconocido'
            };
        });
        loading.value = false;
    })
    .catch((error) => {
        console.error('Error fetching empleados:', error);
        loading.value = false;
    });
};

onMounted(fetchData);

const eliminar = function (id) {
    try {
        axios.delete(`/Empleado/DeleteEmpleado/${id}`).then((response) => {
            fetchData();
        }).catch(() => {
          algoSalioMalError(currentTheme.value)
        })
    } catch {
        algoSalioMalError(currentTheme.value)
    }
}

const openDialog = (item) => {
  itemEditar.value = {...item};
  dialog.value = true;

}
const closeDialog = () => {
  dialog.value = false;
}

const guardarEdicionEmpleado = () => {
  dialog.value = false;
  try {
    let params = {
        nombreEmpleado: itemEditar.value.nombreEmpleado,
        apellidoEmpleado: itemEditar.value.apellidoEmpleado,
        DNIEmpleado: itemEditar.value.dniEmpleado,
        telefonoEmpleado: itemEditar.value.telefonoEmpleado,
        direccionEmpleado: itemEditar.value.direccionEmpleado,
        mailEmpleado: itemEditar.value.mailEmpleado,
        idPuestoEmpleado: itemEditar.value.puesto.id || itemEditar.value.idPuestoEmpleado
      }
      axios.put(`/Empleado/UpdateEmpleado/${itemEditar.value.idEmpleado}`, params).then((response) => {
        fetchData();
      })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const descargarListado = () => {
  descargarPDF(titulosTabla, empleados.value, "empleados")
}


</script>

<template>
  <VCard>
    <VCardItem>
      <VCardTitle class="d-flex align-center pe-2">
        <VIcon icon="ri-list-unordered"></VIcon> &nbsp;
        Listado de empleados
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


      <VDataTable class="mt-5" 
        :search="search" 
        :items="empleados" 
        :headers="titulosTabla"
        :items-per-page-options="paginas"
        items-per-page-text="Items por página:"
        no-data-text="No hay registros"
        :loading="loading"
        loading-text="Cargando..."
      >
        <template v-slot:[`item.opciones`]="{ item }">
            <IconBtn
              icon="ri-edit-2-fill"
              color="primary"
              class="me-1"
              @click="openDialog(item)"
            />
            <IconBtn
              icon="ri-delete-bin-5-fill"
              color="error-darken-1"
              class="me-1"
              @click="eliminarRegistro(eliminar, item.idEmpleado, item.nombreEmpleado, currentTheme.value)"
            />
        </template>
      </VDataTable>
    </VCardItem>
    <VCol
      cols="12"
      class="d-flex gap-4 justify-end"
    >
      <VBtn prepend-icon="ri-download-fill" @click="descargarListado"> Descargar PDF</VBtn>
    </VCol>
    

    <!-- MODAL EDITAR -->
    <EditModal :dialog="dialog" @cerrarDialogo="closeDialog" @confirmarDialogo="guardarEdicionEmpleado">
      <!-- Primera fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.idEmpleado"
            :rules="[reglaObligatoria()]"
            label="ID"
            disabled
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.nombreEmpleado"
            :rules="[reglaObligatoria()]"
            label="Nombre"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.apellidoEmpleado"
            :rules="[reglaObligatoria()]"
            label="Apellido"
          />
        </VCol>
      </VRow>

      <!-- Segunda fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.dniEmpleado"
            :rules="[reglaObligatoria()]"
            label="DNI"
            type="number"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.telefonoEmpleado"
            :rules="[reglaObligatoria()]"
            label="Teléfono"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.direccionEmpleado"
            :rules="[reglaObligatoria()]"
            label="Dirección"
          />
        </VCol>
      </VRow>

      <!-- Tercera fila -->
      <VRow>
        <VCol cols="12" md="6" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.mailEmpleado"
            :rules="[reglaObligatoria()]"
            label="Email"
            type="email"
          />
        </VCol>
        <VCol cols="12" md="6">
          <VSelect
            v-model="itemEditar.puesto"
            :items="itemsPuestos"
            item-title="nombre"
            return-object
            :rules="[reglaObligatoria()]"
            label="Puesto"
          />
        </VCol>
      </VRow>
    </EditModal>

  </VCard>
</template>
