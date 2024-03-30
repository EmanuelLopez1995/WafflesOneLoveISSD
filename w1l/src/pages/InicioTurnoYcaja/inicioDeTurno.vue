<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, defineEmits } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'
import CardBasic from '@/views/pages/cards/card-basic/CardBasic.vue'

const allEmpleados = ref([])
const empleadosSeleccionados = ref([])

const emit = defineEmits(['inicioTurno'])

const fecha = ref(null)
const turno = ref('')
const notas = ref('')
const esFeriado = ref(false)

const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const iniciarTurno = () => {
  form.value.validate().then(response => {
    if (response.valid) {
        emit('inicioTurno', empleadosSeleccionados)
    } 
  })
}

const getEmpleados = async () => {
  try {
    await axios.get('/employees/get-all').then(response => {
      allEmpleados.value = response.data.map(empleado => {
        return {
          ...empleado,
          nombreCompletoYid: `${empleado.nombre} ${empleado.apellido} (${empleado.id})`,
        }
      })
    })
  } catch {
    algoSalioMalError(currentTheme.value)
  }
}

const obtenerFechaActual = () => {
  const fechaActual = new Date()

  const dia = fechaActual.getDate()
  const mes = fechaActual.getMonth() + 1
  const anio = fechaActual.getFullYear()

  const diaFormateado = dia < 10 ? '0' + dia : dia
  const mesFormateado = mes < 10 ? '0' + mes : mes

  fecha.value = `${anio}-${mesFormateado}-${diaFormateado}`
}

const deseleccionarEmpleado = empleado => {
  const index = empleadosSeleccionados.value.findIndex(e => e.id === empleado.id)
  if (index !== -1) {
    empleadosSeleccionados.value.splice(index, 1)
  }
}

onMounted(() => {
  getEmpleados()
  obtenerFechaActual()
})
</script>

<template>
  <VCard>
    <VCardItem>
      <VForm
        @submit.prevent="iniciarTurno"
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
            />
          </VCol>

          <VCol
            cols="12"
            md="4"
          >
            <VTextField
              v-model="notas"
              label="Notas"
            />
          </VCol>

          <VCol
            cols="12"
            md="4"
          >
            <VCheckbox
              v-model="esFeriado"
              label="Feriado"
            />
          </VCol>
          <!-- Body -->

          <VCol
            cols="12"
            md="10"
          >
                <VCol
                  cols="12"
                  md="10"
                >
                  <VSelect
                    v-model="empleadosSeleccionados"
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
                  v-for="empleado in empleadosSeleccionados"
                  :key="empleado.id"
                >
                  <VRow>
                    <VCol cols="3">
                      <VTextField readonly>
                        {{ `${empleado.nombre} ${empleado.apellido}` }}
                      </VTextField>
                    </VCol>
                    <VCol cols="2">
                      <VTextField
                        v-model="empleado.horaLlegada"
                        :label="`Hora de llegada`"
                        :rules="[reglaObligatoria()]"
                        type="time"
                      ></VTextField>
                    </VCol>
                    <VCol cols="4">
                      <VTextField
                        v-model="empleado.notas"
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
