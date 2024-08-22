<script setup>
import graficoDona from '@/components/charts/graficoDona.vue';
import axios from 'axios';
import { ref } from 'vue';
import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { useTheme } from 'vuetify';
import useProveedores from '@/composables/useProveedores.js';
import useRandom from '@/composables/useRandom.js';

const compras = ref([]);
const proveedoresDB = ref([]);
const proveedoresArray = ref([]);
const { obtenerProveedores } = useProveedores();
const { obtenerColorAleatorio } = useRandom();

//Grafico
const chartData = ref({
    labels: [],
    datasets: [
        {
            backgroundColor: [],
            data: []
        }
    ]
});

const fetchData = async () => {
    await axios
        .get('/Compra/GetAllCompras')
        .then(response => {
            compras.value = response.data.map(compra => {
                compra.fechaCompra = compra.fechaCompra.slice(0, 10);
                compra.totalConSigno = `$${compra.total}`;
                return compra;
            });
        })
        .catch(error => {
            console.error('Error fetching compras:', error);
        });
};

const formatProveedores = () => {
    proveedoresArray.value = proveedoresDB.value.map(prov => prov.nombre);
};

const contarComprasPorProveedor = () => {
    // Inicializar el array con ceros para cada proveedor
    const comprasPorProveedor = new Array(proveedoresDB.value.length).fill(0);

    // Contar las compras por proveedor
    proveedoresDB.value.forEach((proveedor, index) => {
        compras.value.forEach(compra => {
            // Evaluar si el id del proveedor coincide con el idProveedor de la compra
            if (compra.idProveedor === proveedor.id) {
                // Sumar uno a la posición correspondiente
                comprasPorProveedor[index]++;
            }
        });
    });

    return comprasPorProveedor;
};

const formatGraficos = () => {
    const comprasPorProveedor = contarComprasPorProveedor();

    const backgroundColors = proveedoresArray.value.map(() => obtenerColorAleatorio());

    const datos = {
        labels: proveedoresArray.value,
        datasets: [
            {
                backgroundColor: backgroundColors,
                data: comprasPorProveedor
            }
        ]
    };
    chartData.value = datos;
};

onMounted(async () => {
    proveedoresDB.value = await obtenerProveedores();
    formatProveedores();
    await fetchData();
    formatGraficos();
});
</script>

<template>
    <div>
        <VCard class="py-3 px-3">
            <VRow>
                <VCol
                    cols="12"
                    md="3"
                >
                    <h3 class="d-flex justify-center">Cantidad de compras por proveedor</h3>
                    <div>
                        <graficoDona :chart-data="chartData"></graficoDona>
                    </div>
                </VCol>
            </VRow>
        </VCard>
    </div>
</template>
