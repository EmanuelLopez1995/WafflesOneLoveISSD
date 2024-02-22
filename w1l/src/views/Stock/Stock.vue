<template>
    <v-card>
        <v-tabs class="tabsStyles" v-model="tab">
            <v-tab value="1">Listado</v-tab>
            <v-tab value="2">Registrar producto</v-tab>
        </v-tabs>

        <v-card-text class="">
            <v-window v-model="tab">
                <v-window-item value="1" class="">
                    <h1>Listado</h1>
                </v-window-item> 
                <v-window-item value="2" class="">
                    <registrarStock />
                </v-window-item> 
            </v-window>
        </v-card-text>
    </v-card>
  
</template>

<script>
    import './Stock.scss';
    import '@/css/tabs.scss';
    import registrarStock from '@/components/registrarStock/registrarStock.vue'

    export default {
        components: {
            registrarStock
        },
        data: () => ({
            tab: null,
        })
    };
</script>