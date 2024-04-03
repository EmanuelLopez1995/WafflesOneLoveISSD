<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { useTheme } from 'vuetify'

const empleados = ref({});
const vuetifyTheme = useTheme()


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


</script>

<template>
  <VCard>
    <VCardItem>
      <VTable>
        <thead>
          <tr>
            <th class="text-uppercase">
              ID
            </th>
            <th class="text-uppercase text-center">
              Nombre
            </th>
            <th class="text-uppercase text-center">
              Apellido
            </th>
            <th class="text-uppercase text-center">
              DNI
            </th>
            <th class="text-uppercase text-center">
              Teléfono
            </th>
            <th class="text-uppercase text-center">
              Dirección
            </th>
            <th class="text-uppercase text-center">
              Email
            </th>
            <th class="text-uppercase text-center">
              Puesto
            </th>
            <th class="text-uppercase text-center">
              Opciones
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
  </VCard>
</template>
