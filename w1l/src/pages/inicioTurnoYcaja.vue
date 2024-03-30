<script setup>
import { useRoute } from 'vue-router'
import InicioTurno from '@/pages/InicioTurnoYcaja/inicioDeTurno.vue'
import AperturaCaja from '@/pages/InicioTurnoYcaja/aperturaDeCaja.vue'


const route = useRoute()
const activeTab = ref(route.params.tab)

const empleadosSeleccionados = ref(null)

// tabs
let tabs = [
  {
    title: 'Inicio de turno',
    icon: 'ri-door-open-line',
    tab: 'inicioTurno',
  },
  {
    title: 'Apertura de caja',
    icon: 'ri-money-dollar-box-line',
    tab: 'caja',
    disabled: true
  }
]

const iniciarAperturaDeCaja = (empleados) => {
    tabs[1].disabled = false;
    activeTab.value = 'caja';
    empleadosSeleccionados.value = empleados;
}
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
      <VWindowItem value="inicioTurno">
        <InicioTurno @inicioTurno="iniciarAperturaDeCaja"/>
      </VWindowItem>

      <!-- Apertura de caja -->
      <VWindowItem value="caja">
        <AperturaCaja />
      </VWindowItem>
    </VWindow>
  </div>
</template>
