<template>
    <v-card>
        <v-tabs class="tabsStyles" v-model="tab" color="#42b883" >
            <v-tab value="1">Prueba 1</v-tab>
            <v-tab value="2">Prueba 2</v-tab>
            <v-tab value="3">Prueba 3</v-tab>
            <v-tab value="4">Prueba 4</v-tab>
        </v-tabs>

        <v-card-text class="h-screen">
            <v-window v-model="tab">
                <v-window-item value="1" class="h-screen">
                    <h1>Puto el que lee</h1>
                </v-window-item> 
            </v-window>
        </v-card-text>
    </v-card>
  
</template>

<script>
    import './Stock.scss';
    import '@/css/tabs.scss';
    export default {
        data: () => ({
            tab: null,
        })
    };
</script>

<style>

</style>