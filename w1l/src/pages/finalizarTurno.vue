<script setup>
import { useRoute } from 'vue-router'
import CierreTurno from '@/pages/cierreTurnoYcaja/cierreDeTurno.vue'
import CierreCaja from '@/pages/cierreTurnoYcaja/cierreDeCaja.vue'
import ResumenCierre from '@/pages/cierreTurnoYcaja/resumenCierre.vue'
import { ref, defineEmits, defineProps, computed, onBeforeMount, onMounted } from 'vue'
import { useTheme } from 'vuetify'
import { algoSalioMalError, registroExitosoMensaje, warningMessage } from '@/components/SwalCustom.js'


const route = useRoute()
const activeTab = ref(route.params.tab)

const empleadosSeleccionados = ref([])
const datosDeCierreTurno = ref({})
const datosDeCierreDeCaja = ref({})

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

// tabs
let tabs = [
  {
    title: 'Cierre de turno',
    icon: 'ri-door-open-line',
    tab: 'cierreTurno',
    disabled: false
  },
  {
    title: 'Cierre de caja',
    icon: 'ri-money-dollar-box-line',
    tab: 'cierreCaja',
    disabled: true
  },
  {
    title: 'Finalizar',
    icon: 'ri-check-double-line',
    tab: 'resumen',
    disabled: true
  }
]

const iniciarCierreDeCaja = (datosCierreTurno) => {
    tabs[1].disabled = false;
    tabs[0].disabled = true;
    tabs[2].disabled = true;
    activeTab.value = 'cierreCaja';
    empleadosSeleccionados.value = datosCierreTurno.empleadosSeleccionados;
    datosDeCierreTurno.value = datosCierreTurno;
}

const backToCierreDeTurno = () => {
    tabs[0].disabled = false;
    activeTab.value = 'cierreTurno';
    tabs[1].disabled = true;
    tabs[2].disabled = true;
}

const irAResumen = (datosCierreDeCaja) => {
    datosDeCierreDeCaja.value = datosCierreDeCaja;
    tabs[2].disabled = false;
    tabs[0].disabled = true;
    activeTab.value = 'resumen';
    tabs[1].disabled = true;
    // console.log(datosDeCierreDeCaja.value);
    // console.log(datosDeCierreTurno.value);
}

const backToCaja = () => {
    activeTab.value = 'cierreCaja';
    tabs[1].disabled = false;
    tabs[0].disabled = true;
    tabs[2].disabled = true;
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
      <!-- Cierre de turno -->
      <VWindowItem value="cierreTurno">
        <CierreTurno @cierreTurno="iniciarCierreDeCaja"/>
      </VWindowItem>

      <!-- Cierre de caja -->
      <VWindowItem value="cierreCaja">
        <CierreCaja :empleadosPresentes="empleadosSeleccionados.value" @backToCierreDeTurno="backToCierreDeTurno" @irAResumen="irAResumen"/>
      </VWindowItem>

      <!-- Resumen -->
      <VWindowItem value="resumen">
        <ResumenCierre :turno="datosDeCierreTurno" :caja="datosDeCierreDeCaja" @backToCaja="backToCaja"/>
      </VWindowItem>
    </VWindow>
  </div>
</template>