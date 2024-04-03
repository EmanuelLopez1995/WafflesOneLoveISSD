<script setup>

import axios from 'axios';
import { defineAsyncComponent, ref } from 'vue';
import { eliminarRegistro , algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { useTheme } from 'vuetify'

const proveedores = ref({});
const vuetifyTheme = useTheme()


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
              Razón social
            </th>
            <th class="text-uppercase text-center">
              Dirección
            </th>
            <th class="text-uppercase text-center">
              Teléfono
            </th>
            <th class="text-uppercase text-center">
              CUIT
            </th>
            <th class="text-uppercase text-center">
              Email
            </th>
            <th class="text-uppercase text-center">
              Detalle
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
