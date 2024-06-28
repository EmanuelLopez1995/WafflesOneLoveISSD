<script setup>
import EmpleadosAdmin from '@/pages/admin/empleados/adminEmpleados.vue'
import FinanzasAdmin from '@/pages/admin/finanzas/adminFinanzas.vue'
import StockAdmin from '@/pages/admin/stock/adminStock.vue'

import { useRoute } from 'vue-router'

const route = useRoute()
const activeTab = ref(route.params.tab)
// tabs
let tabs = [
    {
        title: 'Empleados',
        icon: 'ri-team-line',
        tab: 'empleados'
    },
    {
        title: 'Finanzas',
        icon: 'ri-money-dollar-box-line',
        tab: 'finanzas'
    },
    {
        title: 'Usuarios',
        icon: 'ri-check-double-line',
        tab: 'usuarios'
    },
    {
        title: 'Stock',
        icon: 'ri-archive-line',
        tab: 'stock'
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
            <VWindowItem value="empleados">
                <EmpleadosAdmin />
            </VWindowItem>

            <VWindowItem
                value="usuarios"
                to="/asd"
            >
                <h1>Usuarios</h1>
            </VWindowItem>

            <VWindowItem value="finanzas">
                <FinanzasAdmin />
            </VWindowItem>

            <VWindowItem value="stock">
                <StockAdmin />
            </VWindowItem>
        </VWindow>
    </div>
</template>