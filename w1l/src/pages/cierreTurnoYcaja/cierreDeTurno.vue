<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, defineEmits } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'
import CardBasic from '@/views/pages/cards/card-basic/CardBasic.vue'

const allEmpleados = ref([])
const empleadosSeleccionados = ref([])

const emit = defineEmits(['cierreTurno'])

const fecha = ref(null)
const turno = ref('')
const notasInicio = ref('')
const notasCierre = ref('')
const esFeriado = ref(false)
const encargadoDeTurno = ref({nombreCompleto: ''})
const idTurno = ref(null)

const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const cerrarTurno = () => {
  form.value.validate().then(response => {
    if (response.valid) {
        emit('cierreTurno', {empleadosSeleccionados, fecha, turno, notasInicio, notasCierre, esFeriado, encargadoDeTurno, idTurno})
    } 
  })
}

const getEmpleados = async () => {
  try {
    await axios.get('/Empleado/GetAllEmpleados').then(response => {
      allEmpleados.value = response.data.map(empleado => {
        return {
          ...empleado,
          nombreCompleto: `${empleado.nombreEmpleado} ${empleado.apellidoEmpleado}`,
        }
      })
    })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const fetchDatosTurno = async () => {
  try {
    const response = await axios.get('/Turno/GetTurnoEnCurso')
    let { empleados, caja, ...turnoCall } = response.data
    fecha.value = turnoCall.fechaTurno.slice(0,10);
    turno.value = turnoCall.tipoTurno;
    idTurno.value = turnoCall.idTurno;
    esFeriado.value = turnoCall.esFeriado;
    empleadosSeleccionados.value = empleados;
    notasInicio.value = turnoCall.notasInicio;
    agregarNombreYapellidoEmpleados(empleados);
    obtenerEncargadoTurno(turnoCall.idEncargadoTurno);
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const agregarNombreYapellidoEmpleados = (empleados) => {
  empleadosSeleccionados.value = empleados.map(empleadoDB => {
      const empleado = allEmpleados.value.find(emp => emp.idEmpleado === empleadoDB.idEmpleado)
      if (empleado) {
          empleadoDB.nombreEmpleado = empleado.nombreEmpleado;
          empleadoDB.apellidoEmpleado = empleado.apellidoEmpleado;
          empleadoDB.nombreCompleto = empleado.nombreEmpleado + ' ' + empleado.apellidoEmpleado
      }
      return empleadoDB;
  })
}

const obtenerEncargadoTurno = (idEncargadoTurno) => {
  encargadoDeTurno.value = allEmpleados.value.find(emp => emp.idEmpleado === idEncargadoTurno);
}

onMounted(() => {
  getEmpleados();
  fetchDatosTurno();
})
</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="cerrarTurno"
        class="pt-5"
        ref="form"
      >
        <VRow>
          <!-- Header -->
          <VCol
            cols="12"
            md="2"
          >
            <VTextField
              v-model="fecha"
              type="date"
              :rules="[reglaObligatoria()]"
              label="Fecha"
              :readonly="true"
            />
          </VCol>

          <VCol
            cols="12"
            md="2"
          >
            <VSelect
              v-model="turno"
              :items="[1, 2]"
              :rules="[reglaObligatoria()]"
              label="Turno"
              :readonly="true"
            />
          </VCol>
          <VCol
            cols="12"
            md="2"
          >
            <VTextField
                v-model="encargadoDeTurno.nombreCompleto"
                :rules="[reglaObligatoria()]"
                label="Encargado de turno"
                readonly
            />
          </VCol>
          <VCol
            cols="12"
            md="4"
          >
            <VTextField
              v-model="notasCierre"
              label="Notas del cierre"
            />
          </VCol>

          <VCol
            cols="12"
            md="2"
          >
            <VCheckbox
              v-model="esFeriado"
              label="Feriado"
              :readonly="true"
            />
          </VCol>
          <!-- Body -->

          <VCol
            cols="12"
            md="12"
          >
                <VCol
                  cols="12"
                  md="12"
                  v-for="empleado in empleadosSeleccionados"
                  :key="empleado.idEmpleado"
                >
                  <VRow>
                    <VCol cols="2">
                      <VTextField readonly>
                        {{ `${empleado.nombreEmpleado} ${empleado.apellidoEmpleado}` }}
                      </VTextField>
                    </VCol>
                    <VCol cols="2">
                      <VTextField
                        v-model="empleado.horaIngresoEmpleado"
                        :label="`Hora de ingreso`"
                        :rules="[reglaObligatoria()]"
                        type="time"
                        readonly
                      ></VTextField>
                    </VCol>
                    <VCol cols="2">
                      <VTextField
                        v-model="empleado.horaEgresoEmpleado"
                        :label="`Hora de egreso`"
                        :rules="[reglaObligatoria()]"
                        type="time"
                      ></VTextField>
                    </VCol>
                    <VCol cols="4">
                      <VTextField
                        v-model="empleado.descripcionEgreso"
                        :label="`Notas de egreso`"
                      ></VTextField>
                    </VCol>
                  </VRow>
                </VCol>
          </VCol>

          <!-- Footer Botones -->
          <VCol
            cols="12"
            class="d-flex gap-4"
          >
            <VBtn type="submit" append-icon="ri-arrow-right-line"> Siguiente </VBtn>
          </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
