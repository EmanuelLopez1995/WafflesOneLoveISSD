<script setup>
import { useRoute } from 'vue-router'


const route = useRoute()
const activeTab = ref(route.params.tab)
// tabs
let tabs = [
  {
    title: 'Usuarios',
    icon: 'ri-door-open-line',
    tab: 'usuarios',
  },
  {
    title: 'Finanzas',
    icon: 'ri-money-dollar-box-line',
    tab: 'finanzas',
  },
  {
    title: 'Empleados',
    icon: 'ri-check-double-line',
    tab: 'empleados',
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
      <VWindowItem value="usuarios" to="/asd">
        <h1>Usuarios</h1>
      </VWindowItem>

      <!-- Apertura de caja -->
      <VWindowItem value="finanzas">
        <h1>Finanzas</h1>
      </VWindowItem>

      <!-- Resumen -->
      <VWindowItem value="empleados">
        <h1>Empleados</h1>
      </VWindowItem>
    </VWindow>
  </div>
</template>