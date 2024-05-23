<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { useTheme } from 'vuetify'
import EditModal from '@/components/editModal/EditModal.vue';
import descargarPDF from '@/components/pdfHelper.js';

const stock = ref([]);
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditar = ref({});
const search = ref('');
const loading = ref(false);
const estado = ref(null);
const titulosTabla = [          
    {
      key: 'id',
      sortable: false,
      title: 'ID',
    },
    {
      key: 'productName',
      sortable: false,
      title: 'NOMBRE',
    },
    {
      key: 'productBrand',
      sortable: false,
      title: 'MARCA',
    },
    {
        key: 'unitOfMeasurement',
      sortable: false,
      title: 'UNIDAD DE MEDIDA',
    }, 
    {
        key: 'detail',
      sortable: false,
      title: 'DETALLE',
    },
    {
        key: 'minimunStock',
        sortable: false,
        title: 'STOCK MÍNIMO',
    },
    {
        key: 'actualStock',
        sortable: false,
        title: 'Existencias',
    },      
    {
        key: 'estado',
        align: 'center',
        sortable: false,
        title: 'ESTADO',
    },      
    {
      key: 'opciones',
      sortable: false,
      title: 'OPCIONES',
    }
]

const unidadesDeMedida = [
  {
    id: 1,
    nombre: 'Kilogramos'
  },
  {
    id: 2,
    nombre: 'Gramos'
  },
  {
    id: 3,
    nombre: 'Litros'
  },
  {
    id:5,
    nombre: 'Mililitros'
  },
  {
    id:4,
    nombre: 'Unidad'
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
    axios.get('/stock/get-all').then((response) => {
        stock.value = response.data.map(item => {
            const unidad = unidadesDeMedida.find(unidad => unidad.id === item.unitOfMeasurement);
            const estado = item.actualStock > item.minimunStock ? 'Sobrante' : 'Faltante';
            return {
                ...item,
                unitOfMeasurement: unidad ? unidad.nombre : item.unitOfMeasurement,
                estado: estado
            };
        });
        loading.value = false;
    })
  } catch {
    algoSalioMalError(currentTheme.value);
    loading.value = false;
  }
};

onMounted(fetchData);

const eliminar = async function (id) {
    try {
        const response = await axios.delete(`/stock?id=${id}`);
        fetchData();
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
}

const openDialog = (item) => {
  itemEditar.value = {...item};
  dialog.value = true;

}
const closeDialog = () => {
  dialog.value = false;
}

const guardarEdicionStock = () => {
//   dialog.value = false;
  try {
    // let params = {
    //     id: itemEditar.value.id,
    //     nombre: itemEditar.value.nombre,
    //     apellido: itemEditar.value.apellido,
    //     dni: itemEditar.value.dni,
    //     numero: itemEditar.value.numero,
    //     direccion: itemEditar.value.direccion,
    //     email: itemEditar.value.email,
    //     posicion: itemEditar.value.posicion
    //   }
    //   axios.put(`/employees`, params).then((response) => {
    //     fetchData();
    //   })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const descargarListado = () => {
  descargarPDF(titulosTabla, stock.value, "stock")
}


</script>

<template>
  <VCard>
    <VCardItem>
      <VCardTitle class="d-flex align-center pe-2">
        <VIcon icon="ri-list-unordered"></VIcon> &nbsp;
        Listado de Stock
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
        :items="stock" 
        :headers="titulosTabla"
        :items-per-page-options="paginas"
        items-per-page-text="Items por página:"
        no-data-text="No hay registros"
        :loading="loading"
        loading-text="Cargando..."
      >
        <template v-slot:[`item.estado`]="{item}">
            <VChip :color="item.estado == 'Sobrante' ? 'success' : 'error'" size="small" variant="tonal">
                {{item.estado}}
            </VChip>  
        </template>
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
    <EditModal :dialog="dialog" @cerrarDialogo="closeDialog" @confirmarDialogo="guardarEdicionStock">
      <!-- Primera fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.id"
            label="ID"
            disabled
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.productName"
            :rules="[reglaObligatoria()]"
            label="Nombre"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.productBrand"
            :rules="[reglaObligatoria()]"
            label="Marca"
          />
        </VCol>
      </VRow>

      <!-- Segunda fila -->
      <VRow>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VSelect
            v-model="itemEditar.unitOfMeasurement"
            :items="unidadesDeMedida"
            item-title="nombre"
            :rules="[reglaObligatoria()]"
            label="Unidad de medida"
          />
        </VCol>
        <VCol cols="12" md="4">
          <VTextField
            v-model="itemEditar.minimunStock"
            :rules="[reglaObligatoria()]"
            label="Stock mínimo"
            type="number"
          />
        </VCol>
        <VCol cols="12" md="4" class="flex-sm-column">
          <VTextField
            v-model="itemEditar.actualStock"
            :rules="[reglaObligatoria()]"
            label="Stock actual"
            type="number"
          />
        </VCol>
      </VRow>

      <!-- Tercera fila -->
      <VRow>
        <VCol cols="12" md="12">
          <VTextField
            v-model="itemEditar.detail"
            label="Detalle"
          />
        </VCol>
      </VRow>
    </EditModal>

  </VCard>
</template>
