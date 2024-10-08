<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const props = defineProps({
    esModalDetalle: Boolean,
    datosRegistro: Object,
    esModal: Boolean
});
const emit = defineEmits(['cerrarDialogo']);

const nombre = ref('');
const stockMinimo = ref('');
const detalle = ref('');
const form = ref(null);
const formAgregarArticulo = ref(null);
const unidadDeMedida = ref(null);
const articulos = ref(null);
const articuloNuevo = ref(null);
const listadoArticulosNuevos = ref([]);

const dialog = ref(false);

const unidadesDeMedida = ref([]);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const cargarDatosRegistroIngrediente = () => {
    if (props.esModalDetalle) { 
        nombre.value = props.datosRegistro.nombreIngrediente;
        detalle.value = props.datosRegistro.detalleIngrediente;
        props.datosRegistro.idsArticulos.forEach((idArt) => {
            articuloNuevo.value = articulos.value.find(art => art.idArticulo == idArt)
            agregarArticulo();
        })
    }
}

const confirmarModificacion = () => {
    const params = crearParams();
    params.idIngrediente = props.datosRegistro.idIngrediente;
    emit('confirmarDialogo', params);
};

const crearParams = () => {
    let idsArticulos = listadoArticulosNuevos.value.map(art => art.idArticulo)
    return {
        nombreIngrediente: nombre.value,
        detalleIngrediente: detalle.value || '',
        idsArticulos
    };
}

const fetchArticulos = async () => {
    try {
        await axios.get('/Articulo/GetAllArticulo').then(response => {
            articulos.value = response.data;
        });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

const fetchUMD = () => {
    try {
        axios
            .get('/UMD/GetAllUMDs')
            .then(response => {
                unidadesDeMedida.value = response.data;
                unidadesDeMedida.value.forEach(item => {
                    item.nombreUMD = item.nombreUMD.charAt(0).toUpperCase() + item.nombreUMD.slice(1);
                });
            })
            .catch(() => {
                algoSalioMalError(currentTheme.value);
            });
    } catch {
        algoSalioMalError(currentTheme.value);
    }
};

onMounted(async () => {
    fetchUMD();
    await fetchArticulos();
    cargarDatosRegistroIngrediente();
});

const registrarIngrediente = () => {
    form.value.validate().then(response => {
        if (response.valid) {
            try {
                // TODO: Agregar un informe diciendo que las unidades de medida de los articulos se convertiran a la del ingrediente
                let params = crearParams();
                axios.post('/Ingrediente', params).then(() => {
                    registroExitosoMensaje('ingrediente', currentTheme.value);
                    form.value.reset();
                    listadoArticulosNuevos.value = [];
                });
            } catch (error) {
                algoSalioMalError(currentTheme.value);
            }
        }
    });
};

const abrirModalAgregarArticulo = () => {
    dialog.value = !dialog.value;
};

const agregarArticuloModal = () => {
    formAgregarArticulo.value.validate().then(response => {
        if (response.valid) {
            agregarArticulo();
        }
    });
};

const agregarArticulo = () => {
    articuloNuevo.value.umd = unidadesDeMedida.value.find((umd) => {
        if(umd.idUMD == articuloNuevo.value.idUMD) {
            return umd.nombreUMD
        }
    })
    listadoArticulosNuevos.value.push(articuloNuevo.value);
    articuloNuevo.value = null;
    dialog.value = !dialog;
}

const eliminarArticulo = idArticulo => {
    const index = listadoArticulosNuevos.value.findIndex(a => a.idArticulo === idArticulo);
    if (index !== -1) {
        listadoArticulosNuevos.value.splice(index, 1);
    }
};

const cerrarModal = () => {
    emit('cerrarDialogo');
};

const cancelarDialog = () => {
    articuloNuevo.value = null;
    dialog.value = !dialog.value;
};
</script>

<template>
    <VCard>
        <VCardItem>
            <h2 class="pb-3 mt-3">Registrar Ingrediente</h2>
            <VForm
                @submit.prevent="registrarIngrediente"
                ref="form"
                class="pt-2"
            >
                <VRow>
                    <VCol
                        cols="12"
                        md="6"
                    >
                        <VTextField
                            v-model="nombre"
                            :rules="[reglaObligatoria()]"
                            label="Nombre del ingrediente"
                            placeholder="Leche, Manteca, etc."
                        />
                    </VCol>
                    <!-- <VCol
                        cols="12"
                        md="3"
                    >
                        <VSelect
                            :items="unidadesDeMedida"
                            item-title="nombreUMD"
                            return-object
                            :rules="[reglaObligatoria()]"
                            v-model="unidadDeMedida"
                            label="Unidad de medida"
                        />
                    </VCol> -->
<!-- 
                    <VCol
                        cols="12"
                        md="3"
                    >
                        <VTextField
                            v-model="stockMinimo"
                            :rules="[reglaObligatoria()]"
                            :label="unidadDeMedida ? 'Stock mínimo en ' + unidadDeMedida.nombreUMD : 'Stock mínimo'"
                            type="number"
                        />
                    </VCol> -->

                    <VCol
                        cols="12"
                        md="12"
                    >
                        <VTextField
                            v-model="detalle"
                            label="Detalle"
                        />
                    </VCol>
                    <!-- Tabla de artículos -->
                    <VCol
                        v-if="listadoArticulosNuevos.length > 0"
                        cols="12"
                        md="12"
                    >
                        <h2 class="pb-3 mt-3">Artículos relacionados</h2>
                        <VTable>
                            <thead>
                                <tr>
                                    <th class="text-uppercase">NOMBRE</th>
                                    <th class="text-uppercase">MARCA</th>
                                    <th class="text-uppercase">STOCK ACTUAL</th>
                                    <th class="text-uppercase">UMD</th>
                                    <th class="text-uppercase">OPCIONES</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr
                                    v-for="item in listadoArticulosNuevos"
                                    :key="item.idArticulo"
                                >
                                    <td>
                                        {{ item.nombreArticulo.charAt(0).toUpperCase() + item.nombreArticulo.slice(1) }}
                                    </td>
                                    <td>{{ item.marcaArticulo.toUpperCase() }}</td>
                                    <td>{{ item.stockActual }}</td>
                                    <td>{{ item.umd.nombreUMD }}</td>
                                    <td>
                                        <IconBtn
                                            icon="ri-delete-bin-5-fill"
                                            color="error-darken-1"
                                            class="me-1"
                                            @click="eliminarArticulo(item.idArticulo)"
                                        />
                                    </td>
                                </tr>
                            </tbody>
                        </VTable>
                    </VCol>
                    <!-- Botones -->
                    <VCol
                        cols="12"
                        class="d-flex justify-end gap-4"
                    >
                        <VBtn
                            color="primary"
                            @click="abrirModalAgregarArticulo"
                        >
                            Agregar artículo
                        </VBtn>
                        <VBtn
                            v-if="props.esModalDetalle"
                            color="secondary"
                            variant="outlined"
                            @click="emit('cerrarDialogo')"
                        >
                            CANCELAR
                        </VBtn>
                        <VBtn
                            v-if="props.esModalDetalle"
                            @click="confirmarModificacion"
                        >
                            MODIFICAR
                        </VBtn>
                        <VBtn v-if="!props.esModalDetalle" type="submit"> Registrar </VBtn>
                    </VCol>
                </VRow>
            </VForm>

            <!-- Modal carga producto -->
            <VDialog
                persistent
                v-model="dialog"
                width="40%"
            >
                <VCard
                    prepend-icon="ri-add-large-fill"
                    title="Agregar artículo"
                    class="px-5"
                >
                    <VForm
                        @submit.prevent="agregarArticuloModal"
                        ref="formAgregarArticulo"
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
                                    <VSelect
                                        :items="articulos"
                                        :rules="[reglaObligatoria()]"
                                        item-title="nombreArticulo"
                                        v-model="articuloNuevo"
                                        return-object
                                        label="Artículo"
                                        placeholder="Seleccione el artículo a agregar"
                                    >
                                    </VSelect>
                                </VCol>
                                <VCol
                                    cols="12"
                                    md="12"
                                    class="d-flex gap-4 justify-end"
                                >
                                    <VBtn
                                        color="secondary"
                                        variant="outlined"
                                        @click="cancelarDialog"
                                    >
                                        CANCELAR
                                    </VBtn>
                                    <VBtn type="submit"> AGREGAR </VBtn>
                                </VCol>
                            </VRow>
                        </VCol>
                    </VForm>
                </VCard>
            </VDialog>
        </VCardItem>
    </VCard>
</template>
