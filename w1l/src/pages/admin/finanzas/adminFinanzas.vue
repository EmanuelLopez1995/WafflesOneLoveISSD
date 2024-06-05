<script setup>
import { useRoute } from 'vue-router'
import CajaAdmin from '@/pages/admin/finanzas/cajaAdmin.vue'

const route = useRoute()
const activeTab = ref(route.params.tab)
const contenidoActivoSueldos = ref(0)

let tabs = [
  {
    title: 'Caja',
    icon: 'ri-door-lock-box-line',
    tab: 'cajaAdmin',
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
      <!-- Activo inicial -->
      <VWindowItem value="cajaAdmin">
        <CajaAdmin />
      </VWindowItem>
    </VWindow>
  </div>
</template>