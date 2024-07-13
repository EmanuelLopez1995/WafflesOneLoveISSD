<script setup>
import ListadoArticulos from '@/pages/Stock/listadoArticulos.vue'
import ListadoIngredientes from '@/pages/Stock/listadoIngredientes.vue'

import { useRoute } from 'vue-router' 

const route = useRoute()
const activeTab = ref(route.params.tab)
// tabs
let tabs = [
    {
        title: 'Listado de ingredientes',
        icon: 'ri-file-list-2-line',
        tab: 'listIng'
    },
    {
        title: 'Listado de artículos',
        icon: 'ri-file-list-2-line',
        tab: 'listArt'
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
            <VWindowItem value="listIng">
                <ListadoIngredientes :tabKey="activeTab"/>
            </VWindowItem>

            <VWindowItem value="listArt">
                <ListadoArticulos :tabKey="activeTab"/>
            </VWindowItem>
        </VWindow>
    </div>
</template>