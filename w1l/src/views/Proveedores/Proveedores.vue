<template>
    <v-card>
        <v-tabs class="tabsStyles" v-model="tab">
            <v-tab value="1">Listado</v-tab>
            <v-tab value="2">Registrar Proveedor</v-tab>
        </v-tabs>

        <v-card-text class="h-screen">
            <v-window v-model="tab">
                <v-window-item value="1" class="h-screen">
                    <listado-proveedor />
                </v-window-item> 
                <v-window-item value="2" class="h-screen">
                    <registrar-proveedor />
                </v-window-item> 
            </v-window>
        </v-card-text>
    </v-card>
  
</template>

<script>
    import './Proveedores.scss';
    import '@/css/tabs.scss';
    import RegistrarProveedor from '../../components/RegistrarProveedor/RegistrarProveedor.vue';
    import ListadoProveedor from '../../components/ListadoProveedor/ListadoProveedor.vue';

    export default {
        components: {
            RegistrarProveedor,
            ListadoProveedor
        },
        data: () => ({
            tab: null,
        })
    };
</script>