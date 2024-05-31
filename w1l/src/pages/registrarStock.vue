<script setup>
import { useRoute } from 'vue-router'
import RegistrarMateriaPrima from '@/pages/Stock/registrarMateriaPrima.vue';
import RegistrarArticulo from '@/pages/Stock/registrarArticulo.vue';


const route = useRoute()
const activeTab = ref(route.params.tab)

// tabs
let tabs = [
  {
    title: 'Registrar Materia Prima',
    icon: 'ri-draft-line',
    tab: 'registrarMateriaPrima'
  },
  {
    title: 'Registrar Artículo',
    icon: 'ri-draft-line',
    tab: 'registrarArticulo'
  }
]


</script>

<template>
  <div>
    <VTabs
      v-model="activeTab"
      show-arrows
    >
      <VTab
        v-for="item in tabs"
        :key="item.icon"
        :value="item.tab"
        :disabled="item.disabled"
      >
        <VIcon
          size="20"
          start
          :icon="item.icon"
        />
        {{ item.title }}
      </VTab>
    </VTabs>

    <VWindow
      v-model="activeTab"
      class="mt-5 disable-tab-transition"
      :touch="false"
    >
      <!-- Inicio de turno -->
      <VWindowItem value="registrarMateriaPrima">
        <RegistrarMateriaPrima/>
      </VWindowItem>

      <!-- Apertura de caja -->
      <VWindowItem value="registrarArticulo">
        <RegistrarArticulo/>
      </VWindowItem>
    </VWindow>
  </div>
</template>
