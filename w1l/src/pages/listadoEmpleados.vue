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
            <th v-for="titulo in titulosTabla" :key="titulo.ID" class="text-uppercase">
              {{titulo}}
            </th>
            <th>
              OPCIONES
            </th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="item in empleados"
            :key="item.id"
          >
            <td>
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

    <EditModal :dialog="dialog" @cerrarDialogo="closeDialog" @confirmarDialogo="guardarEdicionEmpleado">
        <VRow>
          <VCol
            cols="12"
            class="d-flex gap-4"
          >
              <VTextField
                v-model="itemEditar.id"
                :rules="[reglaObligatoria()]"
                label="ID"
                disabled
              />
              <VTextField
                v-model="itemEditar.nombre"
                :rules="[reglaObligatoria()]"
                label="Nombre"
              />
              <VTextField
                v-model="itemEditar.apellido"
                :rules="[reglaObligatoria()]"
                label="Apellido"
              />
          </VCol>
          <VCol
            cols="12"
            class="d-flex gap-4"
          >
              <VTextField
                v-model="itemEditar.dni"
                :rules="[reglaObligatoria()]"
                label="DNI"
                type="number"
              />
              <VTextField
                v-model="itemEditar.numero"
                :rules="[reglaObligatoria()]"
                label="Teléfono"
              />
              <VTextField
                v-model="itemEditar.direccion"
                :rules="[reglaObligatoria()]"
                label="Dirección"
              />
          </VCol>
          <VCol
            cols="12"
            class="d-flex gap-4"
          >
              <VTextField
                v-model="itemEditar.email"
                :rules="[reglaObligatoria()]"
                label="Email"
                type="email"
              />
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
