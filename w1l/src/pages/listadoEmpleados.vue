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
      key: 'id',
      sortable: false,
      title: 'ID',
    },
    {
      key: 'nombre',
      sortable: false,
      title: 'NOMBRE',
    },
    {
      key: 'apellido',
      sortable: false,
      title: 'APELLIDO',
    },
    {
      key: 'dni',
      sortable: false,
      title: 'DNI',
    },      
    {
      key: 'numero',
      sortable: false,
      title: 'TELÉFONO',
    },
    {
      key: 'direccion',
      sortable: false,
      title: 'DIRECCIÓN',
    }, 
    {
      key: 'email',
      sortable: false,
      title: 'EMAIL',
    },
    {
      key: 'posicion',
      sortable: false,
      title: 'PUESTO',
    },
    {
      key: 'opciones',
      sortable: false,
      title: 'OPCIONES',
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
  try {
    axios.get('/employees/get-all').then((response) => {
        empleados.value = response.data;
        loading.value = false;
    })
  } catch {
    algoSalioMalError(currentTheme.value);
    loading.value = false;
  }
};

onMounted(fetchData);

const eliminar = function (id) {
    try {
        axios.delete(`/employees?id=${id}`).then((response) => {
            fetchData();
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
        id: itemEditar.value.id,
        nombre: itemEditar.value.nombre,
        apellido: itemEditar.value.apellido,
        dni: itemEditar.value.dni,
        numero: itemEditar.value.numero,
        direccion: itemEditar.value.direccion,
        email: itemEditar.value.email,
        posicion: itemEditar.value.posicion
      }
      axios.put(`/employees`, params).then((response) => {
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
              @click="eliminarRegistro(eliminar, item.id, item.nombre, currentTheme.value)"
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
            v-model="itemEditar.id"
            :rules="[reglaObligatoria()]"
            label="ID"
            disabled
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.nombre"
            :rules="[reglaObligatoria()]"
            label="Nombre"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.apellido"
            :rules="[reglaObligatoria()]"
            label="Apellido"
          />
        </VCol>
      </VRow>

      <!-- Segunda fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.dni"
            :rules="[reglaObligatoria()]"
            label="DNI"
            type="number"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.numero"
            :rules="[reglaObligatoria()]"
            label="Teléfono"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.direccion"
            :rules="[reglaObligatoria()]"
            label="Dirección"
          />
        </VCol>
      </VRow>

      <!-- Tercera fila -->
      <VRow>
        <VCol cols="12" md="6" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.email"
            :rules="[reglaObligatoria()]"
            label="Email"
            type="email"
          />
        </VCol>
        <VCol cols="12" md="6">
          <VSelect
            v-model="itemEditar.posicion"
            :rules="[reglaObligatoria()]"
            label="Puesto"
          />
        </VCol>
      </VRow>
    </EditModal>

  </VCard>
</template>
