<script setup>
import { useTheme } from 'vuetify';
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref, computed } from 'vue';
import axios from 'axios';
import RegistrarArticulo from '@/pages/Stock/registrarArticulo.vue';

//TODO: Sacar de la lista de agregar productos a los productos que ya se han agregado, sino va a funcionar mal el eliminar de la lista

//props

const props = defineProps({
    esModalDetalle: Boolean,
    datosRegistro: Object
});

// Agregar producto

const dialog = ref(false);
const productoSeleccionado = ref({});
const productos = ref([]);

// Registrar prodcuto nuevo

const dialogRegistrarProducto = ref(false);

// Editar
const esEditar = ref(false);
const indexAEditar = ref(null);

// Principal
const proveedor = ref('');
const fecha = ref(null);
const totalFactura = ref(0);
const productosSeleccionados = ref([]);
const formAgregarProducto = ref(null);
const proveedores = ref([]);
// const archivo = ref(null);

const vuetifyTheme = useTheme();

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors);
});

const emit = defineEmits(['cerrarDialogo', 'confirmarDialogo']);

const cargarDatosRegistroCompra = () => {
    if (props.esModalDetalle) {
        proveedor.value = props.datosRegistro.proveedor;
        proveedor.value.nombreYid = `${props.datosRegistro.proveedor.nombre} (${props.datosRegistro.proveedor.id})`;
        fecha.value = props.datosRegistro.fechaCompra;
        productosSeleccionados.value = props.datosRegistro.detallesCompra.map(detalle => {
            const articulo = productos.value.find(prod => prod.idArticulo === detalle.idArticulo);
            return {
                ...detalle,
                producto: articulo || {}
            };
        });
    }
};

const confirmarModificacion = () => {
    const params = crearParams();
    emit('confirmarDialogo', params);
};

const crearParams = () => {
    const productosParams = productosSeleccionados.value.map(el => ({
        idArticulo: el.producto.idArticulo,
        cantidad: el.cantidad,
        precioUnitario: el.precioUnitario,
        subtotal: el.subtotal
    }));

    return {
        idCompra: props.datosRegistro ? props.datosRegistro.idCompra : 0,
        fechaCompra: fecha.value,
        // archivo: archivoBase64,
        idProveedor: proveedor.value.id,
        total: calcularTotal(),
        detallesCompra: productosParams
    };
};

const registrarCompra = async () => {
    try {
        const params = crearParams();
        axios.post('/Compra/AddCompra', params).then(() => {
            registroExitosoMensaje('compra', currentTheme.value);
        });
    } catch (error) {
        console.log(error);
        algoSalioMalError(currentTheme.value);
    }
};

const obtenerFechaActual = () => {
    const fechaActual = new Date();

    const dia = fechaActual.getDate();
    const mes = fechaActual.getMonth() + 1;
    const anio = fechaActual.getFullYear();

    const diaFormateado = dia < 10 ? '0' + dia : dia;
    const mesFormateado = mes < 10 ? '0' + mes : mes;

    fecha.value = `${anio}-${mesFormateado}-${diaFormateado}`;
};

const calcularSubtotal = (cantidad, precioUnitario, id) => {
    if (cantidad !== undefined && precioUnitario !== undefined) {
        productosSeleccionados.value.forEach(item => {
            if (item.id == id) {
                item.subtotal = cantidad * precioUnitario;
            }
        });
        calcularTotal();
        return cantidad * precioUnitario;
    } else {
        calcularTotal();
        return 0;
    }
};

const deseleccionarProducto = producto => {
    const index = productosSeleccionados.value.findIndex(e => e.id === producto.id);
    if (index !== -1) {
        productosSeleccionados.value.splice(index, 1);
    }
};

const calcularTotal = () => {
    const subtotales = productosSeleccionados.value.map(item => item.subtotal);
    return subtotales.reduce((acumulador, elemento) => acumulador + elemento, 0);
};

const agregarProducto = () => {
    formAgregarProducto.value.validate().then(response => {
        if (response.valid) {
            productoSeleccionado.value.subtotal =
                productoSeleccionado.value.cantidad * productoSeleccionado.value.precioUnitario;
            const copiaProducto = { ...productoSeleccionado.value };
            if (!esEditar.value) {
                //AGREGAR NUEVO PRODUCTO A LISTA
                productosSeleccionados.value.push(copiaProducto);
            } else {
                // MODIFICAR PRODUCTO DE LISTA
                productosSeleccionados.value[indexAEditar.value] = copiaProducto;
            }
            productoSeleccionado.value = {};
            calcularTotal();
            dialog.value = !dialog;
        }
    });
};

const abrirModalAgregarProducto = () => {
    esEditar.value = false;
    dialog.value = !dialog.value;
};

const eliminarProductoDeLista = index => {
    if (index !== -1) {
        productosSeleccionados.value.splice(index, 1);
        calcularTotal();
    }
};

const editarRegistro = index => {
    esEditar.value = true;
    indexAEditar.value = index;
    productoSeleccionado.value = { ...productosSeleccionados.value[index] };
    dialog.value = !dialog.value;
};

const cancelarDialog = () => {
    productoSeleccionado.value = {};
    dialog.value = !dialog.value;
};

const obtenerProveedores = () => {
    try {
        axios.get('/Proveedor/GetAllProveedores').then(response => {
            proveedores.value = response.data.map(proveedor => {
                return {
                    ...proveedor,
                    nombreYid: `${proveedor.nombre} (${proveedor.id})`
                };
            });
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

const obtenerArticulos = async () => {
    try {
        await axios.get('/Articulo/GetAllArticulo').then(response => {
            productos.value = response.data;
        });
    } catch (error) {
        algoSalioMalError(currentTheme.value);
    }
};

onMounted(async () => {
    await obtenerProveedores();
    await obtenerArticulos();
    obtenerFechaActual();
    cargarDatosRegistroCompra();
});
</script>

<template>
    <VCard>
        <VCardItem>
            <h2
                v-if="!props.esModalDetalle"
                class="pb-5"
            >
                Registrar compra
            </h2>
            <h2
                v-else
                class="pb-5"
            >
                Detalle de la compra
            </h2>
            <VForm
                @submit.prevent="registrarCompra"
                ref="form"
                class="pt-2"
            >
                <VRow>
                    <VCol
                        cols="12"
                        md="2"
                    >
                        <VTextField
                            v-model="fecha"
                            type="date"
                            :rules="[reglaObligatoria()]"
                            readonly
                            label="Fecha"
                        />
                    </VCol>
                    <VCol
                        cols="12"
                        md="5"
                    >
                        <VSelect
                            v-model="proveedor"
                            :rules="[reglaObligatoria()]"
                            :items="proveedores"
                            item-title="nombreYid"
                            return-object
                            label="Proveedor"
                        />
                    </VCol>
                    <!-- <VCol
                        cols="12"
                        md="5"
                    >
                        <VRow>
                            <VCol cols="12">
                                <VFileInput
                                    label="Cargue el comprobante"
                                    variant="outlined"
                                    v-model="archivo"
                                ></VFileInput>
                            </VCol>
                        </VRow>
                    </VCol> -->

                    <VCol cols="12">
                        <h2>Productos</h2>
                    </VCol>
                    <VCol
                        v-if="productosSeleccionados.length != 0"
                        cols="12"
                    >
                        <VTable>
                            <thead>
                                <tr>
                                    <th class="text-uppercase">Cantidad</th>
                                    <th class="text-uppercase">Producto</th>
                                    <th class="text-uppercase">Precio unitario</th>
                                    <th class="text-uppercase">Subtotal</th>
                                    <th class="text-uppercase">Acciones</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr
                                    v-for="(item, index) in productosSeleccionados"
                                    :key="item.producto.id"
                                >
                                    <td>
                                        {{ item.cantidad }}
                                    </td>
                                    <td>
                                        {{ item.producto.nombreArticulo }}
                                    </td>
                                    <td>$ {{ item.precioUnitario }}</td>
                                    <td>$ {{ item.subtotal }}</td>
                                    <td>
                                        <IconBtn
                                            icon="ri-edit-2-fill"
                                            color="primary"
                                            class="me-1"
                                            @click="editarRegistro(index)"
                                        />
                                        <IconBtn
                                            icon="ri-delete-bin-5-fill"
                                            color="error-darken-1"
                                            class="me-1"
                                            @click="eliminarProductoDeLista(index)"
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <b>$ {{ calcularTotal() }}</b>
                                    </td>
                                </tr>
                            </tbody>
                        </VTable>
                    </VCol>
                    <VCol
                        cols="12"
                        class="d-flex gap-4"
                    >
                        <VBtn @click="abrirModalAgregarProducto"> Agregar producto </VBtn>
                    </VCol>
                    <VCol
                        v-if="productosSeleccionados.length != 0"
                        cols="12"
                        class="d-flex justify-end gap-4"
                    >
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
                        <VBtn
                            v-if="!props.esModalDetalle"
                            type="submit"
                        >
                            REGISTRAR
                        </VBtn>
                    </VCol>
                </VRow>
            </VForm>
        </VCardItem>

        <!-- Modal carga producto -->
        <VDialog
            persistent
            v-model="dialog"
            width="60%"
        >
            <VCard
                prepend-icon="ri-add-large-fill"
                title="Agregar producto"
                class="px-5"
            >
                <VForm
                    @submit.prevent="agregarProducto"
                    ref="formAgregarProducto"
                >
                    <VCol
                        cols="12"
                        class="d-flex gap-4 justify-end"
                    >
                        <VRow>
                            <VCol
                                cols="12"
                                md="3"
                            >
                                <VTextField
                                    v-model="productoSeleccionado.cantidad"
                                    label="Cantidad"
                                    active
                                    :rules="[reglaObligatoria()]"
                                    type="number"
                                >
                                </VTextField>
                            </VCol>
                            <VCol
                                cols="12"
                                md="6"
                            >
                                <VSelect
                                    v-model="productoSeleccionado.producto"
                                    :items="productos"
                                    item-title="nombreArticulo"
                                    return-object
                                    :rules="[reglaObligatoria()]"
                                    label="Producto"
                                    placeholder="Seleccione los productos comprados"
                                >
                                </VSelect>
                            </VCol>
                            <VCol
                                cols="12"
                                md="3"
                            >
                                <VTextField
                                    v-model="productoSeleccionado.precioUnitario"
                                    active
                                    :rules="[reglaObligatoria()]"
                                    label="Precio Unitario"
                                    prefix="$"
                                    type="number"
                                >
                                </VTextField>
                            </VCol>
                            <VCol
                                cols="12"
                                class="d-flex"
                            >
                                <p><b>Desea registrar un producto nuevo?</b></p>
                                <b
                                    class="text-primary px-3 cursor-pointer"
                                    @click="dialogRegistrarProducto = !dialogRegistrarProducto"
                                    >CLICK AQUÍ</b
                                >
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
                                <VBtn
                                    v-if="esEditar"
                                    type="submit"
                                >
                                    MODIFICAR
                                </VBtn>
                                <VBtn
                                    v-else
                                    type="submit"
                                >
                                    AGREGAR
                                </VBtn>
                            </VCol>
                        </VRow>
                    </VCol>
                </VForm>
            </VCard>
        </VDialog>

        <VDialog
            v-model="dialogRegistrarProducto"
            width="60%"
        >
            <RegistrarArticulo />
        </VDialog>
    </VCard>
</template>
