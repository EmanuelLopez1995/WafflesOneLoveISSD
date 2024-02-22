<template>
    <v-card>
        <v-tabs class="tabsStyles" v-model="tab">
            <v-tab value="1">Listado</v-tab>
            <v-tab value="2">Registrar</v-tab>
        </v-tabs>

        <v-card-text class="">
            <v-window v-model="tab">
                <v-window-item value="1" class="">
                    1
                </v-window-item> 
                <v-window-item value="2" class="">
                    2
                </v-window-item> 
            </v-window>
        </v-card-text>
    </v-card>
  
</template>

<script>
    import './Compras.scss';
    import '@/css/tabs.scss';

    export default {
        components: {
        },
        data: () => ({
            tab: null,
        })
    };
</script>