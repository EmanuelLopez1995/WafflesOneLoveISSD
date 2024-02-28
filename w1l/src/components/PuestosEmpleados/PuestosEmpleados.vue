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
                                    <v-dialog max-width="500" persistent>
                                        <template v-slot:activator="{ props: activatorProps }">
                                            <span
                                                class="editButton" 
                                                v-bind="activatorProps"
                                                @click="setNombrePuestoAEditar(item)"
                                            ></span>
                                        </template>

                                        <template v-slot:default="{ isActive }">
                                            <v-card title="Editar...">
                                                <v-form @submit.prevent="editarPuestoEmpleado" ref="form">
                                                    <v-text-field
                                                        class="inputPuestoEmpleado"
                                                        v-model="nombrePuestoAEditar"
                                                        :rules="reglas.notNull"
                                                        label="Nombre del puesto"
                                                    ></v-text-field>

                                                    <v-card-actions>
                                                        <v-spacer></v-spacer>
                                                        <v-btn
                                                            text="Cancelar"
                                                            @click="isActive.value = false"
                                                        ></v-btn>
                                                        <v-btn
                                                            type="submit"
                                                            text="Confirmar"  
                                                            variant="tonal"
                                                            @click="isActive.value = false"
                                                        ></v-btn>
                                                    </v-card-actions>
                                                </v-form>
                                            </v-card>
                                        </template>
                                    </v-dialog>
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
            puestoAagregar: '',
            nombrePuestoAEditar: ''
        }
    },
    methods: {
        agregarPuesto() {
            console.log('pasó')
        },
        editarPuestoEmpleado() { // hacer con datos reales
            this.$refs.form.validate().then(response => {
                if (response.valid) {
                    console.log('form valido')
                }
            });
        },
        setNombrePuestoAEditar(item) { //validar con datos reales
            const idPuesto = item.id;
            const nombrePuesto = item.puesto;
            this.nombrePuestoAEditar = item.puesto;
        }
    }
}
</script>

<style>

</style>