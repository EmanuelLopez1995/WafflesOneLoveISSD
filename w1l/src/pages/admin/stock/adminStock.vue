<script setup>
import umd from '@/pages/admin/stock/umd.vue'

import { useRoute } from 'vue-router'

const route = useRoute()
const activeTab = ref(route.params.tab)
// tabs
let tabs = [
    {
        title: 'Unidades de medida',
        icon: 'ri-expand-width-fill',
        tab: 'umd'
    },
    {
        title: 'Otro',
        icon: 'ri-money-dollar-box-line',
        tab: 'otro'
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
            <VWindowItem value="umd">
                <umd />
            </VWindowItem>

            <VWindowItem value="otro">
                <h1>Otro</h1>
            </VWindowItem>
        </VWindow>
    </div>
</template>