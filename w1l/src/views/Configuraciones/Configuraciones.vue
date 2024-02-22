<template>
      <v-card>
        <v-tabs class="tabsStyles" v-model="tab">
            <v-tab value="1">Puestos de empleados</v-tab>
            <v-tab value="2">Config 2</v-tab>
        </v-tabs>

        <v-card-text class="">
            <v-window v-model="tab">
                <v-window-item value="1" class="">
                    <PuestosEmpleados />
                </v-window-item> 
                <v-window-item value="2" class="">
                    <h1>Config 2</h1>
                </v-window-item> 
            </v-window>
        </v-card-text>
    </v-card>
</template>

<script>

    import '@/css/tabs.scss';
    import PuestosEmpleados from '@/components/PuestosEmpleados/PuestosEmpleados.vue'

    export default {
        components: {
            PuestosEmpleados
        },
        data: () => ({
            tab: null,
        })
    };
</script>

<style>

</style>