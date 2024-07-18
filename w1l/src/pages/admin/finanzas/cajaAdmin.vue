<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje, eliminarRegistro } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';
import useBilletes from '@/composables/useBilletes.js';

const dialogModificarActivoInicial = ref(false);
const dialogModificarBillete = ref(false);
const activoInicial = ref(0);
const activoInicialAmodificar = ref(0);
const formActivoInicial = ref(null);
const { fetchBilletes } = useBilletes();
const billetesBD = ref([]);
const nuevoBillete = ref(null);
const formRegistroBilletes = ref(null);
const valorBilleteAeditar = ref(null);
const formEdicionBilletes = ref(null);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const obtenerActivoInicial = () => {
    axios
        .get('/ActivoInicial/GetActivoInicial/1')
        .then(response => {
            activoInicial.value = response.data.montoActivoInicial;
        })
        .catch(error => {
            algoSalioMalError(currentTheme.value);
        });
};

const modificarActivoInicial = () => {
    formActivoInicial.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    id: 1,
                    montoActivoInicial: activoInicialAmodificar.value
                };
                axios
                    .put('/ActivoInicial/UpdateActivoInicial/1', params)
                    .then(response => {
                        obtenerActivoInicial();
                        cancelarDialogActivoInicial();
                        registroExitosoMensaje('activo inicial', currentTheme.value);
                    })
                    .catch(() => {
                        cancelarDialogActivoInicial();
                        algoSalioMalError(currentTheme.value);
                    });
            } catch (error) {
                cancelarDialogActivoInicial();
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};

onMounted(async () => {
    obtenerActivoInicial();
    billetesBD.value = await fetchBilletes();
});

const cancelarDialogActivoInicial = () => {
    dialogModificarActivoInicial.value = !dialogModificarActivoInicial.value;
};

const cancelarDialogEditarBillete = () => {
    dialogModificarBillete.value = !dialogModificarBillete.value;
};

const abrirDialogoActivoInicial = () => {
    activoInicialAmodificar.value = activoInicial.value;
    dialogModificarActivoInicial.value = !dialogModificarActivoInicial.value;
};

const eliminar = async function (id) {
    try {
        await axios.delete(`/Billetes/${id}`);
        billetesBD.value = await fetchBilletes();
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

const abrirEdicionBillete = item => {
    valorBilleteAeditar.value = Object.assign({}, item);
    dialogModificarBillete.value = !dialogModificarBillete.value;
};

const confirmarEdicionBillete = () => {
    formEdicionBilletes.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    valorBillete: valorBilleteAeditar.value.valorBillete
                };
                axios
                    .put(`/Billetes/${valorBilleteAeditar.value.idBillete}`, params)
                    .then(async () => {
                        billetesBD.value = await fetchBilletes();
                        dialogModificarBillete.value = !dialogModificarBillete.value;
                        registroExitosoMensaje('billete', currentTheme.value);
                    })
                    .catch(() => {
                        algoSalioMalError(currentTheme.value);
                    });
            } catch (error) {
                algoSalioMalError(currentTheme.value)
            }
        }
    });
};

const confirmarRegistroBillete = () => {
    formRegistroBilletes.value.validate().then(response => {
        if (response.valid) {
            try {
                let params = {
                    valorBillete: nuevoBillete.value
                };
                axios
                    .post(`/Billetes`, params)
                    .then(async () => {
                        formRegistroBilletes.value.reset();
                        nuevoBillete.value = ref(null);
                        billetesBD.value = await fetchBilletes();
                        registroExitosoMensaje('billete', currentTheme.value);
                    })
                    .catch(() => {
                        algoSalioMalError(currentTheme.value);
                    });
            } catch (error) {
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};
</script>

<template>
    <VCard>
        <VCardItem>
            <div>
                <div class="d-flex align-center">
                    <h2>Activo inicial actual: ${{ activoInicial }}</h2>
                    <VBtn
                        class="mx-5"
                        @click="abrirDialogoActivoInicial"
                        >Modificar</VBtn
                    >
                </div>

                <!-- Dialog modificar activo inicial -->
                <VDialog
                    v-model="dialogModificarActivoInicial"
                    width="30%"
                >
                    <VCard
                        prepend-icon="ri-edit-2-line"
                        title="Modificar Activo Inicial"
                        class="px-5"
                    >
                        <VForm
                            @submit.prevent="modificarActivoInicial"
                            ref="formActivoInicial"
                        >
                            <VCol
                                cols="12"
                                class="d-flex gap-4 justify-end"
                            >
                                <VRow>
                                    <VCol
                                        cols="12"
                                        md="12"
                                    >
                                        <VTextField
                                            v-model="activoInicialAmodificar"
                                            label="Activo Inicial"
                                            active
                                            :rules="[reglaObligatoria()]"
                                            type="number"
                                        >
                                        </VTextField>
                                    </VCol>
                                    <VCol
                                        cols="12"
                                        md="12"
                                        class="d-flex gap-4 justify-end"
                                    >
                                        <VBtn
                                            color="secondary"
                                            variant="outlined"
                                            @click="cancelarDialogActivoInicial"
                                        >
                                            CANCELAR
                                        </VBtn>
                                        <VBtn type="submit"> MODIFICAR </VBtn>
                                    </VCol>
                                </VRow>
                            </VCol>
                        </VForm>
                    </VCard>
                </VDialog>
                <VRow>
                    <VCol
                        cols="12"
                        md="4"
                    >
                        <h2 class="pb-3 mt-8">Configuración de billetes</h2>
                        <VTable>
                            <thead>
                                <tr>
                                    <th class="text-uppercase">VALOR</th>
                                    <th class="text-uppercase">OPCIONES</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr
                                    v-for="item in billetesBD"
                                    :key="item.idBillete"
                                >
                                    <td>{{ item.valorBillete }}</td>
                                    <td>
                                        <IconBtn
                                            icon="ri-edit-2-fill"
                                            color="primary"
                                            class="me-1"
                                            @click="abrirEdicionBillete(item)"
                                        />
                                        <IconBtn
                                            icon="ri-delete-bin-5-fill"
                                            color="error-darken-1"
                                            class="me-1"
                                            @click="
                                                eliminarRegistro(
                                                    eliminar,
                                                    item.idBillete,
                                                    item.valorBillete,
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
                        <VForm
                            @submit.prevent="confirmarRegistroBillete"
                            ref="formRegistroBilletes"
                        >
                            <h2 class="pb-3 mt-8">Registrar nuevo billete</h2>
                            <VRow>
                                <VCol
                                    cols="12"
                                    md="4"
                                >
                                    <VTextField
                                        v-model="nuevoBillete"
                                        :rules="[reglaObligatoria()]"
                                        label="Valor del billete"
                                        type="number"
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
                </VRow>
                <!-- Dialogo modificar billete -->
                <VDialog
                    persistent
                    v-model="dialogModificarBillete"
                    width="30%"
                >
                    <VCard
                        prepend-icon="ri-edit-2-fill"
                        title="Editar Billete"
                        class="px-5 py-5"
                    >
                        <VForm
                            @submit.prevent="confirmarEdicionBillete"
                            ref="formEdicionBilletes"
                        >
                            <VRow>
                                <VCol
                                    cols="12"
                                    md="12"
                                >
                                    <VTextField
                                        v-model="valorBilleteAeditar.valorBillete"
                                        :rules="[reglaObligatoria()]"
                                        label="Valor del billete"
                                        type="number"
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
                                        @click="cancelarDialogEditarBillete"
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
            </div>
        </VCardItem>
    </VCard>
</template>

