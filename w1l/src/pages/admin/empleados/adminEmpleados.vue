<script setup>
import { useRoute } from 'vue-router'
import RegistrarSueldos from '@/pages/admin/empleados/registrarSueldos.vue'
import RegistrarAdelantos from '@/pages/admin/empleados/registrarAdelantos.vue'

const route = useRoute()
const activeTab = ref(route.params.tab)
const contenidoActivoSueldos = ref(0)

//Contenidos sueldos
const contenidos = [
  {
    title: 'Registrar sueldos',
    id: 1,
  },
  {
    title: 'Registrar adelantos',
    id: 2,
  },
]
</script>


<template>
  <div>
    <VTabs
      v-model="activeTab"
      show-arrows
    >
      <VMenu>
        <template v-slot:activator="{ props }">
          <VTab
            value="sueldos"
            :disabled="false"
            v-bind="props"
          >
            <VIcon
              size="20"
              start
              icon="ri-wallet-3-line"
            />
            Sueldos
          </VTab>
        </template>
        <VList>
          <VListItem
            v-for="item in contenidos"
            :key="item.id"
            :value="item.id"
          >
            <VListItemTitle @click="contenidoActivoSueldos = item.id">{{ item.title }}</VListItemTitle>
          </VListItem>
        </VList>
      </VMenu>
      <VTab
        value="otro"
        :disabled="false"
      >
        <VIcon
          size="20"
          start
          icon="ri-wallet-3-line"
        />
        asdasd
      </VTab>
    </VTabs>

    <VWindow
      v-model="activeTab"
      class="mt-5 disable-tab-transition"
      :touch="false"
    >
      <!-- Sueldos -->
      <VWindowItem value="sueldos">
        <RegistrarSueldos v-if="contenidoActivoSueldos == 1" />
        <RegistrarAdelantos v-if="contenidoActivoSueldos == 2" />
      </VWindowItem>

      <!-- Otro -->
      <VWindowItem value="otro">
        <h1>Otro</h1>
      </VWindowItem>
    </VWindow>
  </div>
</template>