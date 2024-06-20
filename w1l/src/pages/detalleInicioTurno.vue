<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje, warningMessage } from '@/components/SwalCustom.js'
import { ref, defineEmits, defineProps, computed, onBeforeMount, onMounted } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'
import { useRouter } from 'vue-router'
import { useGeneralStore } from '@/store/store.js'

const emit = defineEmits(['backToCaja'])

const store = useGeneralStore()
const router = useRouter()

const dialogDatosTurno = ref(null)
const dialogDatosEmpleados = ref(null)
const dialogDatosCaja = ref(null)
const form = ref(null)
const formModTurno = ref(null)
const formModEmpleados = ref(null)
const formModCaja = ref(null)

////////////////////// DE LA DB
const datosTurnoDB = ref(null)
const datosCajaDB = ref(null)
const datosEmpleadosDB = ref(null)
const allEmpleados = ref(null)

/////////////////// DEL RESUMEN

const datosTurnoResumen = ref({
  idTurno: null,
  tipoTurno: null,
  fechaTurno: '',
  horaDelInicio: '',
  horaCierre: null,
  notasInicio: '',
  notasCierre: null,
  esFeriado: null,
  idEncargadoTurno: null,
  encargadoTurno: {
    nombreEmpleado: null,
    apellidoEmpleado: null,
  },
  idCaja: null,
})
const datosCajaResumen = ref({
  idCaja: null,
  activoInicial: null,
  retiroCaja: null,
  importeInicial: null,
  importeFinal: null,
  encargadoCaja: {
    nombreEmpleado: null,
    apellidoEmpleado: null,
  },
})
const datosEmpleadosResumen = ref(null)

const empleadosSeleccionados = ref([])

// Datos a editar
const datosTurnoAEditar = ref(null)
const datosEmpleadosAEditar = ref(null)
const datosCajaAEditar = ref(null)

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const fetchDatosTurno = async () => {
  try {
    const response = await axios.get('/Turno/GetTurnoEnCurso')
    let { empleados, caja, ...turno } = response.data
    datosTurnoDB.value = turno
    datosCajaDB.value = caja
    datosEmpleadosDB.value = empleados
    formatearDatosTurno(turno)
    formatearDatosEmpleados(empleados)
    datosCajaResumen.value = caja
    obtenerEncargadoDeCaja()
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const formatearDatosTurno = turno => {
  const encargadoTurno = allEmpleados.value.find(empleado => empleado.idEmpleado === turno.idEncargadoTurno)

  if (encargadoTurno) {
    datosTurnoResumen.value = {
      ...turno,
      encargadoTurno: encargadoTurno,
    }
  }
  delete datosTurnoResumen.value.idEncargadoTurno
}

const formatearDatosEmpleados = empleados => {
  datosEmpleadosResumen.value = empleados.map(empleadoDB => {
    // debugger;
    const empleado = allEmpleados.value.find(emp => emp.idEmpleado === empleadoDB.idEmpleado)
    // if (empleado) {
    //     empleadoDB.nombreEmpleado = empleado.nombreEmpleado;
    //     empleadoDB.apellidoEmpleado = empleado.apellidoEmpleado;
    // }
    return {
      ...empleadoDB,
      ...empleado,
    }
  })
}

const obtenerEncargadoDeCaja = async () => {
  //OBTIENE LOS DATOS DE DATOSEMPLEADOSRESUMEN
  const encargadoCaja = datosEmpleadosResumen.value.find(empleado => empleado.esRespDeApertCaja === true)

  datosCajaResumen.value = {
    ...datosCajaResumen.value,
    encargadoCaja,
  }
}

const getEmpleados = async () => {
  try {
    await axios.get('/Empleado/GetAllEmpleados').then(response => {
      allEmpleados.value = response.data.map(empleado => {
        return {
          ...empleado,
          nombreCompletoYid: `${empleado.nombreEmpleado} ${empleado.apellidoEmpleado} (${empleado.idEmpleado})`,
        }
      })
    })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

//DIALOG TURNO
const abrirDialogEditarTurno = () => {
  datosTurnoAEditar.value = { ...datosTurnoResumen.value }
  dialogDatosTurno.value = !dialogDatosTurno.value
}
const cerrarDialogEditarTurno = () => {
  dialogDatosTurno.value = !dialogDatosTurno.value
}

const agregarModificacionTurno = () => {
  //CHEQUEAR SI ESTA OK ESTO
  formModTurno.value.validate().then(response => {
    if (response.valid) {
      datosTurnoResumen.value = datosTurnoAEditar.value
      dialogDatosTurno.value = !dialogDatosTurno.value
    }
  })
}

// Dialog Empleados
const abrirDialogEditarEmpleados = () => {
  datosEmpleadosAEditar.value = datosEmpleadosResumen.value.map(empleado => ({ ...empleado }))
  dialogDatosEmpleados.value = !dialogDatosEmpleados.value
}
const agregarModificacionEmpleados = () => {
  formModEmpleados.value.validate().then(response => {
    if (response.valid) {
      datosEmpleadosResumen.value = datosEmpleadosAEditar.value
      dialogDatosEmpleados.value = !dialogDatosEmpleados.value
      datosEmpleadosResumen.value.forEach(empleado => {
        if (!empleado.esRespDeApertCaja) {
          empleado.esRespDeApertCaja = false
        }
        if (datosCajaResumen.value.encargadoCaja.idEmpleado == empleado.idEmpleado) {
          empleado.esRespDeApertCaja = true
        }
      })
      //verificar si se eliminó el responsable de caja
      const encCajaEliminado = datosEmpleadosResumen.value.every(empleado => empleado.esRespDeApertCaja === false)
      if (encCajaEliminado) {
        datosCajaResumen.value.encargadoCaja = undefined
      }
    }
  })
}
const cerrarDialogEditarEmpleados = () => {
  dialogDatosEmpleados.value = !dialogDatosEmpleados.value
}

// Dialog caja

const abrirDialogEditarCaja = () => {
  datosCajaAEditar.value = { ...datosCajaResumen.value }
  valorTotal.value = datosCajaAEditar.value.importeInicial
  dialogDatosCaja.value = !dialogDatosCaja.value
}
const agregarModificacionCaja = () => {
  formModCaja.value.validate().then(response => {
    if (response.valid) {
      datosCajaResumen.value = datosCajaAEditar.value
      datosCajaResumen.value.importeInicial = valorTotal.value
      datosEmpleadosResumen.value.forEach(empleado => {
        if (empleado.idEmpleado == datosCajaResumen.value.encargadoCaja.idEmpleado) {
          empleado.esRespDeApertCaja = true
        } else {
          empleado.esRespDeApertCaja = false
        }
      })
      dialogDatosCaja.value = !dialogDatosCaja.value
    }
  })
}
const cerrarDialogEditarCaja = () => {
  dialogDatosCaja.value = !dialogDatosCaja.value
}

///////////////
const deseleccionarEmpleado = empleado => {
  const index = datosEmpleadosAEditar.value.findIndex(e => e.idEmpleado === empleado.idEmpleado)
  if (index !== -1) {
    datosEmpleadosAEditar.value.splice(index, 1)
  }
}

//Cosas de caja
const billetes = ref([2000, 1000, 500, 200, 100, 50, 20, 10])
const cantidadBilletes = ref([])
const sumaBilletes = ref([])
const valorTotal = ref(0)
const aperturaCorrecta = ref(null)

const sumarValores = index => {
  sumaBilletes.value[index] = cantidadBilletes.value[index] * billetes.value[index]
  valorTotal.value = sumaBilletes.value.reduce((suma, numero) => suma + numero, 0)
  verificarAperturaCorrecta()
}

const verificarAperturaCorrecta = () => {
  if (valorTotal.value === datosCajaAEditar.value.activoInicial) {
    aperturaCorrecta.value = true
  } else {
    aperturaCorrecta.value = false
  }
}

//////////////////////

onMounted(() => {
  getEmpleados()
  fetchDatosTurno()
})

const confirmarEdicion = () => {
  if (datosCajaResumen.value.encargadoCaja) {
    try {
      let params = {
        idTurno: datosTurnoResumen.value.idTurno,
        tipoTurno: datosTurnoResumen.value.tipoTurno,
        fechaTurno: datosTurnoResumen.value.fechaTurno,
        horaDelInicio: datosTurnoResumen.value.horaDelInicio, // creo que no debería mandarse
        notasInicio: datosTurnoResumen.value.notasInicio,
        esFeriado: datosTurnoResumen.value.esFeriado,
        idEncargadoTurno: datosTurnoResumen.value.encargadoTurno.idEmpleado,
        empleados: datosEmpleadosResumen.value.map(empleado => ({
          idEmpleado: empleado.idEmpleado,
          horaIngresoEmpleado: (() => {
            if (empleado.horaIngresoEmpleado.length == 11) {
              return empleado.horaIngresoEmpleado.slice(0, 8)
            } else if (empleado.horaIngresoEmpleado.length == 5) {
              return empleado.horaIngresoEmpleado + ':00'
            }
            return empleado.horaIngresoEmpleado
          })(),
          descripcionIngreso: empleado.descripcionIngreso,
          esRespDeApertCaja: empleado.esRespDeApertCaja,
        })),
        caja: {
          activoInicial: datosCajaResumen.value.activoInicial,
          importeInicial: datosCajaResumen.value.importeInicial,
        },
      }
      axios
        .put('/Turno/UpdateTurnoEnCurso', params)
        .then(response => {})
        .catch(err => {
          algoSalioMalError(currentTheme.value)
        })
    } catch (err) {
      algoSalioMalError(currentTheme.value)
    }
  } else {
    //El encargado de caja no esta cargado
    warningMessage('Debe registrar un nuevo encargado de caja', currentTheme.value);
  }
}
</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="confirmarEdicion"
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
                  <th class="text-uppercase">Fecha</th>
                  <th class="text-uppercase text-center">Turno</th>
                  <th class="text-uppercase text-center">Encargado de turno</th>
                  <th class="text-uppercase text-center">Notas</th>
                  <th class="text-uppercase text-center">Feriado</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>
                    {{ datosTurnoResumen.fechaTurno.slice(2, 10) }}
                  </td>
                  <td class="text-center">
                    {{ datosTurnoResumen.tipoTurno }}
                  </td>
                  <td class="text-center">
                    {{
                      datosTurnoResumen.encargadoTurno.nombreEmpleado +
                      ' ' +
                      datosTurnoResumen.encargadoTurno.apellidoEmpleado
                    }}
                  </td>
                  <td class="text-center">
                    {{ datosTurnoResumen.notasInicio ? datosTurnoResumen.notasInicio : '-' }}
                  </td>
                  <td class="text-center">
                    {{ datosTurnoResumen.esFeriado ? 'Si' : 'No' }}
                  </td>
                </tr>
              </tbody>
            </VTable>
          </VCol>
          <VCol
            cols="12"
            md="12"
          >
            <VBtn
              color="primary"
              prepend-icon="ri-edit-2-line"
              @click="abrirDialogEditarTurno"
            >
              Editar datos del turno
            </VBtn>
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
                  <th class="text-uppercase">Empleado</th>
                  <th class="text-uppercase text-center">Hora de ingreso</th>
                  <th class="text-uppercase text-center">Notas</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="item in datosEmpleadosResumen"
                  :key="item.idEmpleado"
                >
                  <td>
                    {{ item.nombreEmpleado + ' ' + item.apellidoEmpleado }}
                  </td>
                  <td class="text-center">
                    {{ item.horaIngresoEmpleado.slice(0, 5) }}
                  </td>
                  <td class="text-center">
                    {{ item.descripcionIngreso ? item.descripcionIngreso : '-' }}
                  </td>
                </tr>
              </tbody>
            </VTable>
          </VCol>
          <VCol
            cols="12"
            md="12"
          >
            <VBtn
              color="primary"
              prepend-icon="ri-edit-2-line"
              @click="abrirDialogEditarEmpleados"
            >
              Editar empleados presentes
            </VBtn>
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
                  <th class="text-uppercase">Activo inicial</th>
                  <th class="text-uppercase text-center">Efectivo en caja</th>
                  <th class="text-uppercase text-center">Encargado de apertura de caja</th>
                  <th class="text-uppercase text-center">Estado</th>
                  <th class="text-uppercase text-center">Diferencia de efectivo</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>${{ datosCajaResumen.activoInicial }}</td>
                  <td class="text-center">${{ datosCajaResumen.importeInicial }}</td>
                  <td class="text-center">
                    {{
                      datosCajaResumen.encargadoCaja
                        ? datosCajaResumen.encargadoCaja.nombreEmpleado +
                          ' ' +
                          datosCajaResumen.encargadoCaja.apellidoEmpleado
                        : ''
                    }}
                  </td>
                  <td class="text-center">
                    {{
                      datosCajaResumen.activoInicial == datosCajaResumen.importeInicial
                        ? 'Apertura correcta'
                        : 'Apertura incorrecta'
                    }}
                  </td>
                  <td class="text-center">${{ datosCajaResumen.importeInicial - datosCajaResumen.activoInicial }}</td>
                </tr>
              </tbody>
            </VTable>
          </VCol>
          <VCol
            cols="12"
            md="12"
          >
            <VBtn
              color="primary"
              prepend-icon="ri-edit-2-line"
              @click="abrirDialogEditarCaja"
            >
              Editar apertura de caja</VBtn
            >
          </VCol>
          <VCol
            cols="12"
            md="12"
            class="d-flex gap-4 justify-end"
          >
            <VBtn
              type="submit"
              prepend-icon="ri-check-line"
            >
              Confirmar cambios
            </VBtn>
          </VCol>
        </VRow>
      </VForm>

      <!-- Modales -->

      <!-- Modal editar datos de turno -->
      <VDialog
        v-model="dialogDatosTurno"
        width="50%"
      >
        <VCard
          prepend-icon="ri-edit-2-line"
          title="Modificar datos del turno"
          class="px-5"
        >
          <VForm
            @submit.prevent="agregarModificacionTurno"
            ref="formModTurno"
          >
            <VCol
              cols="12"
              class="d-flex gap-4 justify-end"
            >
              <VRow>
                <VCol
                  cols="12"
                  md="2"
                >
                  <VSelect
                    v-model="datosTurnoAEditar.tipoTurno"
                    :items="[1, 2]"
                    :rules="[reglaObligatoria()]"
                    label="Turno"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="4"
                >
                  <VSelect
                    v-model="datosTurnoAEditar.encargadoTurno"
                    :items="allEmpleados"
                    item-title="nombreCompletoYid"
                    :rules="[reglaObligatoria()]"
                    return-object
                    label="Encargado de turno"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="4"
                >
                  <VTextField
                    v-model="datosTurnoAEditar.notasInicio"
                    label="Notas"
                  />
                </VCol>

                <VCol
                  cols="12"
                  md="2"
                >
                  <VCheckbox
                    v-model="datosTurnoAEditar.esFeriado"
                    label="Feriado"
                  />
                </VCol>

                <VCol
                  cols="12"
                  md="12"
                  class="d-flex gap-4 justify-end"
                >
                  <VBtn
                    color="secondary"
                    variant="outlined"
                    @click="cerrarDialogEditarTurno"
                  >
                    CANCELAR
                  </VBtn>
                  <VBtn type="submit"> ACEPTAR </VBtn>
                </VCol>
              </VRow>
            </VCol>
          </VForm>
        </VCard>
      </VDialog>

      <!-- Modal editar empleados presentes -->

      <VDialog
        v-model="dialogDatosEmpleados"
        width="50%"
      >
        <VCard
          prepend-icon="ri-edit-2-line"
          title="Editar empleados presentes"
          class="px-5"
        >
          <VForm
            @submit.prevent="agregarModificacionEmpleados"
            ref="formModEmpleados"
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
                  <VAutocomplete
                    v-model="datosEmpleadosAEditar"
                    :items="allEmpleados"
                    item-title="nombreCompletoYid"
                    multiple
                    return-object
                    :rules="[reglaObligatoria()]"
                    label="Empleados"
                    placeholder="Seleccione los empleados presentes"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="12"
                  v-for="empleado in datosEmpleadosAEditar"
                  :key="empleado.idEmpleado"
                >
                  <VRow>
                    <VCol cols="3">
                      <VTextField readonly>
                        {{ `${empleado.nombreEmpleado} ${empleado.apellidoEmpleado}` }}
                      </VTextField>
                    </VCol>
                    <VCol cols="2">
                      <VTextField
                        v-model="empleado.horaIngresoEmpleado"
                        :label="`Hora de llegada`"
                        :rules="[reglaObligatoria()]"
                        type="time"
                      ></VTextField>
                    </VCol>
                    <VCol cols="4">
                      <VTextField
                        v-model="empleado.descripcionIngreso"
                        :label="`Notas`"
                      ></VTextField>
                    </VCol>
                    <VCol cols="3">
                      <VBtn
                        size="small"
                        color="error-darken-1"
                        class="mt-1"
                        @click="deseleccionarEmpleado(empleado)"
                      >
                        X
                      </VBtn>
                    </VCol>
                  </VRow>
                </VCol>

                <VCol
                  cols="12"
                  md="12"
                  class="d-flex gap-4 justify-end"
                >
                  <VBtn
                    color="secondary"
                    variant="outlined"
                    @click="cerrarDialogEditarEmpleados"
                  >
                    CANCELAR
                  </VBtn>
                  <VBtn type="submit"> ACEPTAR </VBtn>
                </VCol>
              </VRow>
            </VCol>
          </VForm>
        </VCard>
      </VDialog>

      <!-- Dialog Caja -->

      <VDialog
        v-model="dialogDatosCaja"
        width="50%"
      >
        <VCard
          prepend-icon="ri-edit-2-line"
          title="Editar Apertura de Caja"
          class="px-5"
        >
          <VForm
            @submit.prevent="agregarModificacionCaja"
            ref="formModCaja"
          >
            <VCol
              cols="12"
              class="d-flex gap-4 justify-end"
            >
              <VRow>
                <VCol
                  cols="12"
                  md="6"
                >
                  <VTable>
                    <thead>
                      <tr>
                        <th class="text-uppercase">Billete</th>
                        <th class="text-uppercase text-center">Cantidad</th>
                      </tr>
                    </thead>

                    <tbody>
                      <tr
                        v-for="(billete, index) in billetes"
                        :key="billete"
                      >
                        <td>
                          <b> ${{ billete }} </b>
                        </td>
                        <td>
                          <VTextField
                            v-model="cantidadBilletes[index]"
                            type="number"
                            @input="sumarValores(index)"
                          />
                        </td>
                      </tr>
                      <tr>
                        <td><b> Total </b></td>
                        <td>
                          <b> ${{ valorTotal }} </b>
                        </td>
                      </tr>
                    </tbody>
                  </VTable>
                </VCol>
                <VCol
                  cols="12"
                  md="6"
                >
                  <VRow>
                    <VCol
                      cols="12"
                      md="12"
                    >
                      <VSelect
                        v-model="datosCajaAEditar.encargadoCaja"
                        :items="datosEmpleadosResumen"
                        item-title="nombreCompletoYid"
                        return-object
                        :rules="[reglaObligatoria()]"
                        label="Encargado de apertura de caja"
                        placeholder="Encargado de apertura de caja"
                      />
                    </VCol>
                    <VCol
                      cols="12"
                      md="12"
                    >
                      <h3>Activo inicial: ${{ datosCajaAEditar.activoInicial }}</h3>
                    </VCol>
                    <VCol
                      cols="12"
                      md="12"
                    >
                      <VChip
                        v-if="aperturaCorrecta"
                        color="success"
                      >
                        Apertura correcta
                      </VChip>
                      <VChip
                        v-else-if="aperturaCorrecta == false"
                        color="error"
                      >
                        Apertura incorrecta
                      </VChip>
                    </VCol>
                  </VRow>
                </VCol>
                <VCol
                  cols="12"
                  md="12"
                  class="d-flex gap-4 justify-end"
                >
                  <VBtn
                    color="secondary"
                    variant="outlined"
                    @click="cerrarDialogEditarCaja"
                  >
                    CANCELAR
                  </VBtn>
                  <VBtn type="submit"> ACEPTAR </VBtn>
                </VCol>
              </VRow>
            </VCol>
          </VForm>
        </VCard>
      </VDialog>
    </VCardItem>
  </VCard>
</template>