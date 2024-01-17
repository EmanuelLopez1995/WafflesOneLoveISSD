<template>
    <div>
        <Table v-if="proveedores" :contenido="proveedores" :titulos="titulosTabla" @eliminar="eliminarPorId"/>
    </div>
</template>

<script>
import Table from '../Table/Table.vue';
import {algoSalioMalError} from '@/components/Swal/SwalCustom.js';

export default {
    components: {
        Table
    },
    props: {
        tabSelected: Boolean,
    },
    watch: {
        tabSelected(newValue) {
            if(newValue) {
                this.getDatosProveedor();
            }
        },
    },
    data() {
        return {
            proveedores: [],
            titulosTabla: ['ID', 'Nombre', 'Razón Social', 'Dirección', 'Teléfono', 'CUIT', 'Email', 'Detalles']
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
                algoSalioMalError();
            })
        },
        eliminarPorId(id){
            this.$http.delete(`/suppliers?id=${id}`).then(() =>{
                this.getDatosProveedor();
            }).catch(()=>{
                algoSalioMalError();
            })
        }
    }
}
</script>