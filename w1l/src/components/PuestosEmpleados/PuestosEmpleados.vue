<template>
  <div class="contenedorpuestos">
        <v-container>
            <v-row>
                <v-col cols="5">
                    <v-data-table
                        :headers="headers"
                        :items="puestos"
                        :loading="loading"
                        hide-default-footer disable-pagination
                        item-value="puesto"
                    >
                        <template v-slot:loading>
                            <v-skeleton-loader type="table-row@5"></v-skeleton-loader>
                        </template>
                        <template v-slot:item="{ item }">
                            <tr class="text-left">
                                <td>{{ item.puesto }}</td>
                                <td>
                                    <span class="editButton" @click="editar(item.id)"></span>
                                    <span class="deleteButton" @click="confirmarEliminacion(item.id)"></span>
                                </td>
                            </tr>
                        </template>
                        <template #bottom></template>
                    </v-data-table>
                </v-col>
                <v-col cols="1"></v-col>
                <v-col cols="5">
                    <v-form @submit.prevent="agregarPuesto" ref="form">
                        <h2 class="agregarPuestoTitulo">Agregar puesto</h2>
                        <v-text-field
                            v-model="puestoAagregar"
                            :rules="reglas.notNull"
                            label="Puesto"
                        ></v-text-field>
                        <v-btn v-if="!loading" class="me-10 d-flex" type="submit" color="green-darken-4"> Agregar </v-btn>
                    </v-form>
                </v-col>
                
            </v-row>
        </v-container>
    
  </div>
</template>

<script>
import './PuestosEmpleados.scss'

export default {
    components: {

    },
    computed: {
        reglas() {
            return this.$store.getters['validaciones/reglas'];
        }
    },
    data () {
        return {
            headers: [
                {
                    title: 'Puestos',
                    key: 'puestos'
                },
                {
                    title: 'Acciones',
                    key: 'acciones'
                }
            ],
            puestos: [
                {
                    id: 1,
                    puesto: 'Gerente'
                },
                {
                    id: 2,
                    puesto: 'Cocinero'
                },
                            {
                    id: 3,
                    puesto: 'Limpieza'
                },
                            {
                    id: 4,
                    puesto: 'Dueño'
                }
            ],
            loading: false,
            puestoAagregar: ''
        }
    },
    methods: {
        agregarPuesto() {
            console.log('pasó')
        }
    }
}
</script>

<style>

</style>