<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, defineEmits, defineProps } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'
import { useRouter } from 'vue-router'
import { obtenerHoraActualHHMMSS } from '@/components/fechaYHora.js'
import { useGeneralStore } from '@/store/store.js';

const emit = defineEmits(['backToCaja'])

const store = useGeneralStore();
const router = useRouter();

const props = defineProps({
  turno: {
    type: Object,
    required: true
  },
  caja: {
    type: Object,
    required: true
  }
});

const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const comenzarTurno = () => {
    let horaActual = obtenerHoraActualHHMMSS();

    let empleadosParams = props.turno.empleadosSeleccionados.map((empleado) => ({
        idEmpleado: empleado.idEmpleado,
        horaIngresoEmpleado: empleado.horaLlegada + ':00',
        descripcionIngreso: empleado.notas || '',
        esRespDeApertCaja: props.caja.encargadoDeAperturaDeCaja.idEmpleado == empleado.idEmpleado ? true : false
    }));

    let cajaParams = {
        activoInicial: props.caja.activoInicial,
        importeInicial: props.caja.valorTotal
    }

    let params = {
        tipoTurno: props.turno.turno,
        fechaTurno: props.turno.fecha,
        horaDelInicio: horaActual,
        notasInicio: props.turno.notas || '',
        esFeriado: props.turno.esFeriado,
        idEncargadoTurno: props.turno.encargadoDeTurno.idEmpleado,
        empleados: empleadosParams,
        caja: cajaParams
    }

    try {
        axios.post('/Turno/IniciarTurno', params)
        .then((response) => {
                store.iniciarTurno();
                router.push('/dashboard');
            })
            .catch((error) => {
                console.log(error);
                algoSalioMalError(currentTheme.value);
            });
    } catch (error) {
        console.log(error);
    }

}

</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="comenzarTurno"
        class="pt-5"
        ref="form"
      >
        <VRow>
            <VCol
                cols="12"
                md="12"
            >
                <h2>Datos del turno</h2>
            </VCol>
            <VCol
                cols="12"
                md="12"
            >
                <VTable>
                    <thead>
                        <tr>
                            <th class="text-uppercase">
                                Fecha
                            </th>
                            <th class="text-uppercase text-center">
                                Turno
                            </th>
                            <th class="text-uppercase text-center">
                                Encargado de turno
                            </th>
                            <th class="text-uppercase text-center">
                                Notas
                            </th>
                            <th class="text-uppercase text-center">
                                Feriado
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                {{ turno.fecha }}
                            </td>
                            <td class="text-center">
                                {{ turno.turno }}
                            </td>
                            <td class="text-center">
                                {{ turno.encargadoDeTurno ? turno.encargadoDeTurno.nombreCompletoYid : '-' }}
                            </td>
                            <td class="text-center">
                                {{ turno.notas ? turno.notas : '-' }}
                            </td>
                            <td class="text-center">
                                {{ turno.esFeriado ? 'Si' : 'No' }}
                            </td>
                        </tr>
                    </tbody>
                </VTable>
            </VCol>

            <VCol
                cols="12"
                md="12"
            >
                <h2>Empleados presentes</h2>
            </VCol>
            <VCol
                cols="12"
                md="12"
            >
                <VTable>
                    <thead>
                        <tr>
                            <th class="text-uppercase">
                                Empleado
                            </th>
                            <th class="text-uppercase text-center">
                                Hora de ingreso
                            </th>
                            <th class="text-uppercase text-center">
                                Notas
                            </th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr
                            v-for="item in turno.empleadosSeleccionados"
                            :key="item.id"
                        >
                            <td>
                                {{ item.nombreCompletoYid }}
                            </td>
                            <td class="text-center">
                                {{ item.horaLlegada }}
                            </td>
                            <td class="text-center">
                                {{ item.notas ? item.notas : '-' }}
                            </td>
                        </tr>
                    </tbody>
                </VTable>
            </VCol>
            <VCol
                cols="12"
                md="12"
            >
                <h2>Detalle de apertura de caja</h2>
            </VCol>
            <VCol
                cols="12"
                md="12"
            >
                <VTable>
                    <thead>
                        <tr>
                            <th class="text-uppercase">
                                Activo inicial
                            </th>
                            <th class="text-uppercase text-center">
                                Efectivo en caja
                            </th>
                            <th class="text-uppercase text-center">
                                Encargado de apertura de caja
                            </th>
                            <th class="text-uppercase text-center">
                                Estado
                            </th>
                            <th class="text-uppercase text-center">
                                Diferencia de efectivo
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                ${{ caja.activoInicial }}
                            </td>
                            <td class="text-center">
                                ${{ caja.valorTotal }}
                            </td>
                            <td class="text-center">
                                {{ caja.encargadoDeAperturaDeCaja.nombreCompletoYid }}
                            </td>
                            <td class="text-center">
                                {{ caja.aperturaCorrecta ? 'Apertura correcta' : 'Apertura incorrecta' }}
                            </td>
                            <td class="text-center">
                                ${{ caja.valorTotal - caja.activoInicial }}
                            </td>
                        </tr>
                    </tbody>
                </VTable>
            </VCol>
            <VCol
                cols="12"
                md="8"
                class="d-flex gap-4"
            >
                <VBtn prepend-icon="ri-arrow-left-line" @click="emit('backToCaja')"> Atrás </VBtn>
                <VBtn type="submit" prepend-icon="ri-check-line"> Iniciar Turno </VBtn>
            </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
