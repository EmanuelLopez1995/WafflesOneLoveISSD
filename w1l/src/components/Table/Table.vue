<template>

    <div class="contenedorTable">
        <v-card flat>
            <v-card-title class="d-flex align-center pe-2">
                {{titulo}}

                <v-spacer></v-spacer>
                <v-spacer></v-spacer>

                <v-text-field
                    v-model="search"
                    prepend-inner-icon="mdi-magnify"
                    density="comfortable"
                    label="Buscar"
                    single-line
                    flat
                    hide-details
                    variant="solo-filled"
                    class="searchInput"
                ></v-text-field>
            </v-card-title>


            <v-data-table
                :headers="titulos"
                :items="contenido"
                :search="search"
                :loading="loading"
                no-data-text="No hay datos para mostrar"
                class="tablaContenido"
                items-per-page-text="Items por página:"
                height=600
                :hover="true"
                
            >
                <template v-slot:loading>
                    <v-skeleton-loader type="table-row@10"></v-skeleton-loader>
                </template>
                <template v-slot:item.opciones="{ item }">
                    <span class="editButton" @click="editar(item.id)"></span>
                    <span class="deleteButton" @click="confirmarEliminacion(item.id)"></span>
                </template>
            </v-data-table>
        </v-card>
    </div>
    
</template>

<script>

import "./Table.scss";
import Swal from 'sweetalert2'

export default {
    components: {
    },
    props: {
        contenido: {
            type: Array,
            required: true
        } ,
        titulos: {
            type: Array,
            required: true
        },
        loading: {
            type: Boolean
        },
        titulo: {
            type: String
        }
    },
    data () {
        return {
            mostrarModalConfirmacion: false,
            search: ''
        }
    },
    methods: {
        editar(id) {
            console.log(id)
        },
        confirmarEliminacion(id) {
            Swal.fire({
                title: "Estas seguro?",
                text: " No podrás revertir esta acción!",
                icon: "warning",
                color: "#fff",
                background: "#212121",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Si, eliminar!",
                cancelButtonText: "Cancelar",
                }).then((result) => {
                    if (result.isConfirmed) {
                        this.$emit("eliminar", id); // Pasar el ID del registro al emitir el evento
                        Swal.fire({
                            title: "Eliminado!",
                            text: `El registro con id ${id} fue eliminado`,
                            icon: "success",
                            color: "#fff",
                            background: "#212121",
                            confirmButtonColor: "#3085d6"
                        });
                    }
            });
        }
    }
}
</script>