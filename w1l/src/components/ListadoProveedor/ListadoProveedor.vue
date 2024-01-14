<template>
    <div>
        <Table v-if="proveedores" :contenido="proveedores" :titulos="titulosTabla"/>
    </div>
</template>

<script>
import Table from '../Table/Table.vue';

export default {
    components: {
        Table
    },
    data() {
        return {
            proveedores: [],
            titulosTabla: ['ID', 'Nombre', 'Razón Social', 'Dirección', 'Teléfono', 'CUIT', 'Email']
        }
    },
    created() {
        this.getDatosProveedor();
    },
    methods: {
        getDatosProveedor() {
            this.$http.get('/suppliers/get-all').then((response) =>{
                this.proveedores = response.data;
            }).catch((error)=>{
                console.error(error);
            })
        },
    }
}
</script>