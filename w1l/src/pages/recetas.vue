<script setup>
import { useRoute } from 'vue-router'
import RegistrarRecetas from '@/pages/recetas/registrarRecetas.vue';
import ConsultarRecetas from '@/pages/recetas/consultarRecetas.vue';


const route = useRoute()
const activeTab = ref(route.params.tab)

// tabs
let tabs = [
  {
    title: 'Registrar receta',
    icon: 'ri-draft-line',
    tab: 'registrar'
  },
  {
    title: 'Consultar recetas',
    icon: 'ri-menu-search-line',
    tab: 'consultar'
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
      <VWindowItem value="registrar">
        <RegistrarRecetas/>
      </VWindowItem>

      <!-- Apertura de caja -->
      <VWindowItem value="consultar">
        <ConsultarRecetas/>
      </VWindowItem>
    </VWindow>
  </div>
</template>
