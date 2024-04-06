<script setup>
import { useRoute } from 'vue-router'
import InicioTurno from '@/pages/InicioTurnoYcaja/inicioDeTurno.vue'
import AperturaCaja from '@/pages/InicioTurnoYcaja/aperturaDeCaja.vue'
import Resumen from '@/pages/InicioTurnoYcaja/resumen.vue'


const route = useRoute()
const activeTab = ref(route.params.tab)

const empleadosSeleccionados = ref([])
const datosDeInicioTurno = ref({})
const datosDeAperturaDeCaja = ref({})

// tabs
let tabs = [
  {
    title: 'Inicio de turno',
    icon: 'ri-door-open-line',
    tab: 'inicioTurno',
    disabled: false
  },
  {
    title: 'Apertura de caja',
    icon: 'ri-money-dollar-box-line',
    tab: 'caja',
    disabled: true
  },
  {
    title: 'Finalizar',
    icon: 'ri-check-double-line',
    tab: 'resumen',
    disabled: true
  }
]

const iniciarAperturaDeCaja = (datosInicioTurno) => {
    tabs[1].disabled = false;
    tabs[0].disabled = true;
    tabs[2].disabled = true;
    activeTab.value = 'caja';
    empleadosSeleccionados.value = datosInicioTurno.empleadosSeleccionados;
    datosDeInicioTurno.value = datosInicioTurno;
}

const backToInicioDeTurno = () => {
    tabs[0].disabled = false;
    activeTab.value = 'inicioTurno';
    tabs[1].disabled = true;
    tabs[2].disabled = true;
}

const irAResumen = (datosAperturaDeCaja) => {
    datosDeAperturaDeCaja.value = datosAperturaDeCaja;
    tabs[2].disabled = false;
    tabs[0].disabled = true;
    activeTab.value = 'resumen';
    tabs[1].disabled = true;
}

const backToCaja = () => {
    tabs[1].disabled = false;
    tabs[0].disabled = true;
    tabs[2].disabled = true;
    activeTab.value = 'caja';
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
        <AperturaCaja :empleadosPresentes="empleadosSeleccionados.value" @backToInicioDeTurno="backToInicioDeTurno" @irAResumen="irAResumen"/>
      </VWindowItem>

      <!-- Resumen -->
      <VWindowItem value="resumen">
        <Resumen :turno="datosDeInicioTurno" :caja="datosDeAperturaDeCaja" @backToCaja="backToCaja"/>
      </VWindowItem>
    </VWindow>
  </div>
</template>
