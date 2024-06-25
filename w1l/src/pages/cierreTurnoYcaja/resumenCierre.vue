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

const finalizarTurno = () => {
    let params = {
        idTurno: props.turno.idTurno,
        horaCierre: obtenerHoraActualHHMMSS(),
        notasCierre: props.turno.notasCierre,
        empleados: props.turno.empleadosSeleccionados.map((empleado) => ({
            idEmpleado: empleado.idEmpleado,
            horaEgresoEmpleado: empleado.horaEgresoEmpleado + ':00',
            descripcionEgreso: empleado.descripcionEgreso,
            esRespDeCierreCaja: (() => {
                if(empleado.idEmpleado == props.caja.encargadoDeCierreDeCaja.idEmpleado){
                    return true;
                } else {
                    return false;
                }
            })(),
            sueldoTotalDelDia: 0
        })),
        caja: {
            retiroCaja: props.caja.valorTotal - props.caja.activoInicial,
            importeFinal: props.caja.valorTotal
        }
    }

    try {
        axios.put('/Turno/FinalizarTurnoEnCurso', params)
        .then((response) => {
                store.finalizarTurno();
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
        @submit.prevent="finalizarTurno"
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
                                Notas de inicio
                            </th>
                            <th class="text-uppercase text-center">
                                Notas de cierre
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
                                {{ turno.encargadoDeTurno ? turno.encargadoDeTurno.nombreCompleto : '-' }}
                            </td>
                            <td class="text-center">
                                {{ turno.notasInicio ? turno.notasInicio : '-' }}
                            </td>
                            <td class="text-center">
                                {{ turno.notasCierre ? turno.notasCierre : '-' }}
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
                                Hora de egreso
                            </th>
                            <th class="text-uppercase text-center">
                                Notas de ingreso
                            </th>
                            <th class="text-uppercase text-center">
                                Notas de egreso
                            </th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr
                            v-for="item in turno.empleadosSeleccionados"
                            :key="item.id"
                        >
                            <td>
                                {{ item.nombreCompleto }}
                            </td>
                            <td class="text-center">
                                {{ item.horaIngresoEmpleado.slice(0, 5) }}
                            </td>
                            <td class="text-center">
                                {{ item.horaEgresoEmpleado }}
                            </td>
                            <td class="text-center">
                                {{ item.descripcionIngreso ? item.descripcionIngreso : '-' }}
                            </td>
                            <td class="text-center">
                                {{ item.descripcionEgreso ? item.descripcionEgreso : '-' }}
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
                                Activo inicial prox. turno
                            </th>
                            <th class="text-uppercase text-center">
                                Efectivo en caja
                            </th>
                            <th class="text-uppercase text-center">
                                Encargado de cierre de caja
                            </th>
                            <th class="text-uppercase text-center">
                                Estado
                            </th>
                            <th class="text-uppercase text-center">
                                Retiro de efectivo
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
                                {{ caja.encargadoDeCierreDeCaja.nombreCompleto }}
                            </td>
                            <td class="text-center">
                                {{ caja.cierreCorrecto ? 'Cierre correcto' : 'Cierre incorrecto' }}
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
