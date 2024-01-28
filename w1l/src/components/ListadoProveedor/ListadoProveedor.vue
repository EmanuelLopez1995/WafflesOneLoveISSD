<template>
    <div>
        <Table :contenido="proveedores" :titulos="titulosTabla" titulo="Proveedores" :loading="loading" @eliminar="eliminarPorId"/>
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
            search: '',
            titulosTabla: [
                {key: 'id', title: 'Id',},
                {key: 'nombre', title: 'Nombre' },
                {key: 'razonSocial', title: 'Razón Social' },
                {key: 'direccion', title: 'Dirección' },
                {key: 'numero', title: 'Teléforno' },
                {key: 'cuit', title: 'Cuit' },
                {key: 'email', title: 'Email' },
                {key: 'detalle', title: 'Detalles' },
                {key: 'opciones', title: 'Opciones' },
            ],
            loading: false
        }
    },
    created() {
        this.getDatosProveedor();
    },
    methods: {
        getDatosProveedor() {
            this.loading = true;
            this.$http.get('/suppliers/get-all').then((response) =>{
                this.proveedores = response.data;
                this.loading = false;
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