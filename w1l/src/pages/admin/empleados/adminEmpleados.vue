<script setup>

import { useRoute } from 'vue-router'


const route = useRoute()
const activeTab = ref(route.params.tab)
// tabs
let tabs = [
  {
    title: 'Puestos',
    icon: 'ri-team-line',
    tab: 'puestos',
  },
  {
    title: 'otro',
    icon: 'ri-money-dollar-box-line',
    tab: 'otro',
  },
  {
    title: 'otrox2',
    icon: 'ri-check-double-line',
    tab: 'otrox2',
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
      <!-- Resumen -->
      <VWindowItem value="puestos">
        
      </VWindowItem>

      <!-- Inicio de turno -->
      <VWindowItem value="otrox2" to="/asd">
        <h1>otrox2</h1>
      </VWindowItem>

      <!-- Apertura de caja -->
      <VWindowItem value="otro">
        <h1>otro</h1>
      </VWindowItem>

    </VWindow>
  </div>
</template>