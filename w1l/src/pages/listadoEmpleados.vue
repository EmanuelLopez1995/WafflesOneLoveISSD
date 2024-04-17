<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { useTheme } from 'vuetify'
import EditModal from '@/components/editModal/EditModal.vue';

const empleados = ref({});
const dialog = ref(false);
const vuetifyTheme = useTheme();
const titulosTabla = ['ID', 'Nombre', 'Apellido', 'DNI', 'Teléfono', 'Dirección', 'Email', 'Puesto']
const itemEditar = ref({})


const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const fetchData = async () => {
  try {
    axios.get('/employees/get-all').then((response) => {
        empleados.value = response.data;
    })
  } catch {
    algoSalioMalError(currentTheme.value)
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
            <th class="text-center">
              OPCIONES
            </th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="item in empleados"
            :key="item.id"
          >
            <td class="text-center">
              {{ item.id }}
            </td>
            <td class="text-center">
              {{ item.nombre }}
            </td>
            <td class="text-center">
              {{ item.apellido }}
            </td>
            <td class="text-center">
              {{ item.dni }}
            </td>
            <td class="text-center">
              {{ item.numero }}
            </td>
            <td class="text-center">
              {{ item.direccion }}
            </td>
            <td class="text-center">
              {{ item.email }}
            </td>
            <td class="text-center">
              {{ item.posicion }}
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

    <!-- MODAL EDITAR -->
    <EditModal :dialog="dialog" @cerrarDialogo="closeDialog" @confirmarDialogo="guardarEdicionEmpleado">
      <!-- Primera fila -->
      <VRow>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.id"
            :rules="[reglaObligatoria()]"
            label="ID"
            disabled
          />
        </VCol>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.nombre"
            :rules="[reglaObligatoria()]"
            label="Nombre"
          />
        </VCol>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.apellido"
            :rules="[reglaObligatoria()]"
            label="Apellido"
          />
        </VCol>
      </VRow>

      <!-- Segunda fila -->
      <VRow>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.dni"
            :rules="[reglaObligatoria()]"
            label="DNI"
            type="number"
          />
        </VCol>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.numero"
            :rules="[reglaObligatoria()]"
            label="Teléfono"
          />
        </VCol>
        <VCol cols="4">
          <VTextField
            v-model="itemEditar.direccion"
            :rules="[reglaObligatoria()]"
            label="Dirección"
          />
        </VCol>
      </VRow>

      <!-- Tercera fila -->
      <VRow>
        <VCol cols="6">
          <VTextField
            v-model="itemEditar.email"
            :rules="[reglaObligatoria()]"
            label="Email"
            type="email"
          />
        </VCol>
        <VCol cols="6">
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
