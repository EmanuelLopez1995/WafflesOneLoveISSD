<template>

    <div class="contenedorTable">
        <h3 v-if="contenido.length == 0">No hay datos para mostrar</h3>
        <v-table v-else hover density="comfortable">
            <thead>
            <tr>
                <th v-for="titulo in titulos" :key="titulo" class="text-center">
                    {{ titulo }}
                </th>
                <th>
                    Opciones
                </th>
            </tr>
            </thead>
            <tbody>
                <tr v-for="(item) in contenido" :key="item" >
                    <td v-for="campo in camposDeContenido" :key="campo">
                        {{ item[campo] }}
                    </td>
                    <td class="options">
                        <span class="editButton" @click="editar"></span>
                        <span class="deleteButton" @click="confirmarEliminacion(item.id)"></span>
                    </td>
                </tr>
            </tbody>
        </v-table>
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
        }
    },
    watch: {
        contenido: {
            immediate: true,
            handler() {
                this.obtenerCamposDeContenido();
            },
        },
    },
    data () {
        return {
            camposDeContenido: [],
            mostrarModalConfirmacion: false
        }
    },
    methods: {
        obtenerCamposDeContenido(){
            this.camposDeContenido = [];
            const primerDato = this.contenido[0];

            for (const key in primerDato) {
                this.camposDeContenido.push(key);
            }
        },
        editar() {
            console.log("Editar")
        },
        confirmarEliminacion(id) {
            console.log(`Estás intentando eliminar al empleado con el ID: ${id}`)
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
                showLoaderOnConfirm: true,
                }).then((result) => {
                    if (result.isConfirmed) {
                        console.log(`Estás intentando eliminar al empleado con el ID: ${id}`)
                        this.$emit("eliminarEmpleado", id); // Pasar el ID del empleado al emitir el evento
                        Swal.fire({
                            title: "Eliminado!",
                            text: `El empleado con id ${id} fue eliminado`,
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