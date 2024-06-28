<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const dialog = ref(false)
const form = ref(null)
const formRegistro = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const umds = ref([])
const umdSeleccionado = ref({
    id: 0,
    nombreUMD: '',
    nombreCortoUMD: ''
})
const nuevoUMD = ref({
    nombreUMD: '',
    nombreCortoUMD: ''
})

const obtenerUMDS = () => {
    try {
        axios
            .get('/UMD/GetAllUMDs')
            .then(response => {
                umds.value = response.data
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value)
            })
    } catch (error) {
        algoSalioMalError(currentTheme.value)
    }
}

const abrirEdicion = item => {
    umdSeleccionado.value = Object.assign({}, item)
    dialog.value = !dialog.value
}

const cancelarDialog = () => {
    dialog.value = !dialog.value
}

const eliminar = async function (id) {
    try {
        await axios.delete(`/UMD/DeleteUMD/${id}`)
        obtenerUMDS()
    } catch (error) {
        algoSalioMalError(currentTheme.value)
    }
}

const confirmarEdicionUMD = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    nombreUMD: umdSeleccionado.value.nombreUMD,
                    nombreCortoUMD: umdSeleccionado.value.nombreCortoUMD
                }
                axios.put(`/UMD/UpdateUMD/${umdSeleccionado.value.idUMD}`, params).then(() => {
                    obtenerUMDS()
                    dialog.value = !dialog.value
                    registroExitosoMensaje('UMD', currentTheme.value)
                })
            } catch (error) {
                algoSalioMalError(currentTheme.value)
            }
        }
    })
}

const confirmarRegistroUMD = () => {
    formRegistro.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    nombreUMD: nuevoUMD.value.nombreUMD,
                    nombreCortoUMD: nuevoUMD.value.nombreCortoUMD
                }
                axios.post(`/UMD/AddUMD`, params).then(() => {
                    formRegistro.value.reset();
                    nuevoUMD.value = ref({
                        nombreUMD: '',
                        nombreCortoUMD: ''
                    })
                    obtenerUMDS()
                    registroExitosoMensaje('UMD', currentTheme.value)
                }).catch(() => {
                    algoSalioMalError(currentTheme.value)
                })
            } catch (error) {
                algoSalioMalError(currentTheme.value)
            }
        }
    })
}

onMounted(() => {
    obtenerUMDS()
})
</script>

<template>
    <VCard>
        <VCardItem>
            <VRow>
                <VCol
                    cols="12"
                    md="4"
                >
                    <h2 class="pb-3 mt-3">Unidades de medida</h2>
                    <VTable>
                        <thead>
                            <tr>
                                <th class="text-uppercase">NOMBRE</th>
                                <th class="text-uppercase">ABREVIATURA</th>
                                <th class="text-uppercase">OPCIONES</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr
                                v-for="item in umds"
                                :key="item.idUMD"
                            >
                                <td>{{ item.nombreUMD.charAt(0).toUpperCase() + item.nombreUMD.slice(1) }}</td>
                                <td>{{ item.nombreCortoUMD.toUpperCase() }}</td>
                                <td>
                                    <IconBtn
                                        icon="ri-edit-2-fill"
                                        color="primary"
                                        class="me-1"
                                        @click="abrirEdicion(item)"
                                    />
                                    <IconBtn
                                        icon="ri-delete-bin-5-fill"
                                        color="error-darken-1"
                                        class="me-1"
                                        @click="
                                            eliminarRegistro(eliminar, item.idUMD, item.nombreUMD, currentTheme.value)
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
                    <VForm
                        @submit.prevent="confirmarRegistroUMD"
                        ref="formRegistro"
                    >
                        <h2 class="pb-3 mt-3">Registrar nueva UMD</h2>
                        <VRow>
                            <VCol
                                cols="12"
                                md="4"
                            >
                                <VTextField
                                    v-model="nuevoUMD.nombreUMD"
                                    :rules="[reglaObligatoria()]"
                                    label="Nombre"
                                />
                            </VCol>
                            <VCol
                                cols="12"
                                md="4"
                            >
                                <VTextField
                                    v-model="nuevoUMD.nombreCortoUMD"
                                    :rules="[reglaObligatoria()]"
                                    label="Nombre abreviado"
                                />
                            </VCol>
                            <VCol
                                cols="12"
                                md="4"
                            >
                                <VBtn
                                    class="mt-1"
                                    type="submit"
                                >
                                    REGISTRAR
                                </VBtn>
                            </VCol>
                        </VRow>
                    </VForm>
                </VCol>

                <!-- Modal -->
                <VDialog
                    persistent
                    v-model="dialog"
                    width="60%"
                >
                    <VCard
                        prepend-icon="ri-edit-2-fill"
                        title="Editar unidad de medida"
                        class="px-5 py-5"
                    >
                        <VForm
                            @submit.prevent="confirmarEdicionUMD"
                            ref="form"
                        >
                            <VRow>
                                <VCol
                                    cols="12"
                                    md="6"
                                >
                                    <VTextField
                                        v-model="umdSeleccionado.nombreUMD"
                                        :rules="[reglaObligatoria()]"
                                        label="Nombre"
                                    />
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="6"
                                >
                                    <VTextField
                                        v-model="umdSeleccionado.nombreCortoUMD"
                                        :rules="[reglaObligatoria()]"
                                        label="Nombre abreviado"
                                    />
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="12"
                                    class="d-flex justify-end"
                                >
                                    <VBtn
                                        color="secondary"
                                        variant="outlined"
                                        @click="cancelarDialog"
                                    >
                                        CANCELAR
                                    </VBtn>
                                    <VBtn
                                        class="ml-5"
                                        type="submit"
                                    >
                                        MODIFICAR
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