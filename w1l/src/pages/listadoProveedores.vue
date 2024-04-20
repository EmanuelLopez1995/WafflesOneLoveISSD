<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { useTheme } from 'vuetify'
import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import descargarPDF from '@/components/pdfHelper.js';

const proveedores = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditar = ref({});
const search = ref('');


const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors) // se accede con currentTheme.value.primary x ej
})

const fetchData = async () => {
  try {
    axios.get('/suppliers/get-all').then((response) => {
        proveedores.value = response.data;
    })
  } catch (error) {
    algoSalioMalError(currentTheme.value)
  }
};

// Llama a la función fetchData cuando se monta el componente
onMounted(fetchData);

const eliminar = function (id) {
    try {
        axios.delete(`/suppliers?id=${id}`).then((response) => {
            fetchData();
        })
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

const openDialog = (item) => {
  itemEditar.value = {...item};
  dialog.value = true;

}
const closeDialog = () => {
  dialog.value = false;
}

const guardarEdicionProveedor = () => {
  dialog.value = false;
  try {
    let params = {
        id: itemEditar.value.id,
        nombre: itemEditar.value.nombre,
        razonSocial: itemEditar.value.razonSocial,
        direccion: itemEditar.value.direccion,
        numero: itemEditar.value.numero,
        cuit: itemEditar.value.cuit,
        email: itemEditar.value.email,
        detalle: itemEditar.value.detalle
      }
      axios.put(`/suppliers`, params).then((response) => {
        fetchData();
      })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const descargarListado = () => {
  descargarPDF(titulosTabla, proveedores.value, "proveedores")
}

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
      key: 'razonSocial',
      sortable: false,
      title: 'RAZÓN SOCIAL',
    },
    {
      key: 'direccion',
      sortable: false,
      title: 'DIRECCIÓN',
    },      
    {
      key: 'numero',
      sortable: false,
      title: 'TELÉFONO',
    },
    {
      key: 'cuit',
      sortable: false,
      title: 'CUIT',
    }, 
    {
      key: 'email',
      sortable: false,
      title: 'EMAIL',
    },
    {
      key: 'detalle',
      sortable: false,
      title: 'DETALLE',
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
</script>

<template>
  <VCard>
    <VCardItem>
      <VCardTitle class="d-flex align-center pe-2">
        <VIcon icon="ri-list-unordered"></VIcon> &nbsp;
        Listado de proveedores

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
        :items="proveedores" 
        :headers="titulosTabla"
        :items-per-page-options="paginas"
        items-per-page-text="Items por página:"
        no-data-text="No hay registros"
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
    <EditModal :dialog="dialog" @cerrarDialogo="closeDialog" @confirmarDialogo="guardarEdicionProveedor">
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
            v-model="itemEditar.razonSocial"
            :rules="[reglaObligatoria()]"
            label="Razón Social"
          />
        </VCol>
      </VRow>

      <!-- Segunda fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.direccion"
            :rules="[reglaObligatoria()]"
            label="Dirección"
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
            v-model="itemEditar.cuit"
            :rules="[reglaObligatoria()]"
            label="CUIT"
            type="number"
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
          <VTextField
            v-model="itemEditar.detalle"
            :rules="[reglaObligatoria()]"
            label="Detalle"
          />
        </VCol>
      </VRow>
    </EditModal>
  </VCard>
</template>
