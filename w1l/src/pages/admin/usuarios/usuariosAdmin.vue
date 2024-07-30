<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import useUsuarios from '@/composables/useUsuarios.js';
import axios from 'axios';

const dialogRegistrar = ref(false);
const formRegistro = ref(null);
const vuetifyTheme = useTheme();
const { secciones, obtenerUsuarios } = useUsuarios();
const seccionesSeleccionadas = ref([]);
const esEditar = ref(false);

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const usuarios = ref([]);

const nuevoUsuario = ref({
    nombreUsuario: '',
    claveUsuario: ''
});

const abrirRegistrar = item => {
    esEditar.value = false;
    dialogRegistrar.value = !dialogRegistrar.value;
};

const cancelarDialogRegistrar = () => {
    nuevoUsuario.value = ref({
        nombreUMD: '',
        nombreCortoUMD: ''
    });
    seccionesSeleccionadas.value = [];
    dialogRegistrar.value = !dialogRegistrar.value;
};

const abrirEditar = item => {
    esEditar.value = true;
    nuevoUsuario.value = {
        nombreUsuario: item.nombreUsuario,
        claveUsuario: item.claveUsuario
    };
    let usuarioEditar = usuarios.value.find(us => us.idUsuario == item.idUsuario);
    seccionesSeleccionadas.value = usuarioEditar.idsSecciones;
    console.log(seccionesSeleccionadas.value);
    dialogRegistrar.value = !dialogRegistrar.value;
};

const primeraParteSecciones = computed(() => secciones.value.slice(0, Math.ceil(secciones.value.length / 2)));

const segundaParteSecciones = computed(() => secciones.value.slice(Math.ceil(secciones.value.length / 2)));

const eliminar = async function (id) {
    try {
        await axios.delete(`/Usuario/DeleteUsuario/${id}`);
        usuarios.value = await obtenerUsuarios();
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

const confirmarRegistroEdicionUsuario = () => {
    formRegistro.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    nombreUsuario: nuevoUsuario.value.nombreUsuario,
                    claveUsuario: nuevoUsuario.value.claveUsuario,
                    idsSecciones: seccionesSeleccionadas.value
                };
                if (esEditar.value) {
                    // axios
                    //     .put(`/Usuario/AddUsuario`, params)
                    //     .then(async () => {
                    //         formRegistro.value.reset();
                    //         nuevoUsuario.value = ref({
                    //             nombreUMD: '',
                    //             nombreCortoUMD: ''
                    //         });
                    //         seccionesSeleccionadas.value = [];
                    //         usuarios.value = await obtenerUsuarios();
                    //         dialogRegistrar.value = !dialogRegistrar.value;
                    //         registroExitosoMensaje('Sección', currentTheme.value);
                    //     })
                    //     .catch(() => {
                    //         algoSalioMalError(currentTheme.value);
                    //     });
                } else {
                    axios
                        .post(`/Usuario/AddUsuario`, params)
                        .then(async () => {
                            formRegistro.value.reset();
                            nuevoUsuario.value = ref({
                                nombreUMD: '',
                                nombreCortoUMD: ''
                            });
                            seccionesSeleccionadas.value = [];
                            usuarios.value = await obtenerUsuarios();
                            dialogRegistrar.value = !dialogRegistrar.value;
                            registroExitosoMensaje('Sección', currentTheme.value);
                        })
                        .catch(() => {
                            algoSalioMalError(currentTheme.value);
                        });
                }
            } catch (error) {
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};

onMounted(async () => {
    usuarios.value = await obtenerUsuarios();
});
</script>

<template>
    <VCard>
        <VCardItem>
            <VRow>
                <VCol
                    cols="12"
                    md="4"
                >
                    <h2 class="pb-3 mt-3">Usuarios</h2>
                    <VTable>
                        <thead>
                            <tr>
                                <th class="text-uppercase">NOMBRE</th>
                                <th class="text-uppercase">CLAVE</th>
                                <th class="text-uppercase">OPCIONES</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr
                                v-for="item in usuarios"
                                :key="item.idUsuario"
                            >
                                <td>{{ item.nombreUsuario }}</td>
                                <td>{{ item.claveUsuario }}</td>
                                <td>
                                    <IconBtn
                                        icon="ri-edit-2-fill"
                                        color="primary"
                                        class="me-1"
                                        @click="abrirEditar(item)"
                                    />
                                    <IconBtn
                                        icon="ri-delete-bin-5-fill"
                                        color="error-darken-1"
                                        class="me-1"
                                        @click="
                                            eliminarRegistro(
                                                eliminar,
                                                item.idUsuario,
                                                item.nombreUsuario,
                                                currentTheme.value
                                            )
                                        "
                                    />
                                </td>
                            </tr>
                        </tbody>
                    </VTable>
                </VCol>
                <!-- Registro -->
                <VCol
                    cols="12"
                    md="8"
                >
                    <h2 class="pb-3 mt-3">Registrar nuevo usuario</h2>
                    <VBtn
                        class="mt-1"
                        @click="abrirRegistrar"
                    >
                        REGISTRAR
                    </VBtn>
                </VCol>

                <!-- Modales editar y agregar -->
                <VDialog
                    persistent
                    v-model="dialogRegistrar"
                    width="40%"
                >
                    <VCard
                        prepend-icon="ri-edit-2-fill"
                        title="Registrar nuevo usuario"
                        class="px-5 py-5"
                    >
                        <VForm
                            @submit.prevent="confirmarRegistroEdicionUsuario"
                            ref="formRegistro"
                        >
                            <VRow>
                                <VCol
                                    cols="12"
                                    md="6"
                                >
                                    <VTextField
                                        v-model="nuevoUsuario.nombreUsuario"
                                        :rules="[reglaObligatoria()]"
                                        label="Nombre"
                                    />
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="6"
                                >
                                    <VTextField
                                        v-model="nuevoUsuario.claveUsuario"
                                        :rules="[reglaObligatoria()]"
                                        label="Contraseña"
                                    />
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="12"
                                >
                                    <h3 class="mx-3 mt-3">Permisos:</h3>
                                </VCol>

                                <v-col
                                    cols="12"
                                    md="6"
                                >
                                    <v-checkbox
                                        class="px-3"
                                        v-for="seccion in primeraParteSecciones"
                                        :key="seccion.idSeccion"
                                        v-model="seccionesSeleccionadas"
                                        :label="seccion.nombreSeccion"
                                        :value="seccion.idSeccion"
                                    ></v-checkbox>
                                </v-col>
                                <v-col
                                    cols="12"
                                    md="6"
                                >
                                    <v-checkbox
                                        v-for="seccion in segundaParteSecciones"
                                        :key="seccion.idSeccion"
                                        v-model="seccionesSeleccionadas"
                                        :label="seccion.nombreSeccion"
                                        :value="seccion.idSeccion"
                                    ></v-checkbox>
                                </v-col>
                                <VCol
                                    cols="12"
                                    md="12"
                                    class="d-flex justify-end"
                                >
                                    <VBtn
                                        color="secondary"
                                        variant="outlined"
                                        @click="cancelarDialogRegistrar"
                                    >
                                        CANCELAR
                                    </VBtn>
                                    <VBtn
                                        v-if="!esEditar"
                                        class="ml-5"
                                        type="submit"
                                    >
                                        AGREGAR
                                    </VBtn>
                                    <VBtn
                                        v-else
                                        class="ml-5"
                                        type="submit"
                                    >
                                        EDITAR
                                    </VBtn>
                                </VCol>
                            </VRow>
                        </VForm>
                    </VCard>
                </VDialog>
            </VRow>
        </VCardItem>
    </VCard>
</template>