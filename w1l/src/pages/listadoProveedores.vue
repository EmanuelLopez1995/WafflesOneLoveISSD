<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { useTheme } from 'vuetify'
import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import descargarPDF from '@/components/pdfHelper.js';

const proveedores = ref({});
const vuetifyTheme = useTheme();
const dialog = ref(false);
const itemEditar = ref({})


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

const titulosTabla = ["ID", "Nombre", "Razón Social", "Dirección", "Teléfono", "CUIT", "Email", "Detalle"]
</script>

<template>
  <VCard>
    <VCardItem>
      <VTable>
        <thead>
          <tr>
            <th v-for="titulo in titulosTabla" :key="titulo.ID" class="text-uppercase text-center">
              {{titulo}}
            </th>
            <th class="text-uppercase text-center">
              Opciones
            </th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="item in proveedores"
            :key="item.id"
          >
            <td>
              {{ item.id }}
            </td>
            <td class="text-center">
              {{ item.nombre }}
            </td>
            <td class="text-center">
              {{ item.razonSocial }}
            </td>
            <td class="text-center">
              {{ item.direccion }}
            </td>
            <td class="text-center">
              {{ item.numero }}
            </td>
            <td class="text-center">
              {{ item.cuit }}
            </td>
            <td class="text-center">
              {{ item.email }}
            </td>
            <td class="text-center">
              {{ item.detalle }}
            </td>
            <td class="text-center">
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

            </td>
          </tr>
        </tbody>
      </VTable>
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
