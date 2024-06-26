<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const dialog = ref(false)
const form = ref(null)
const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const puestosMock = ref([])
const puestoSeleccionado = ref({
  id: 0,
  nombre: '',
})

const obtenerSueldos = () => {
  try {
    axios
      .get('/SueldosBasicos/GetAllSueldos')
      .then(response => {
        puestosMock.value = response.data.map(puesto => {
          switch (puesto.idSueldosBasicos) {
            case 1:
              puesto.nombre = 'Colaborador'
              break
            case 2:
              puesto.nombre = 'Encargado'
              break
            case 3:
              puesto.nombre = 'Dueño'
              break

            default:
              break
          }
          return puesto
        })
      })
      .catch(() => {
        algoSalioMalError(currentTheme.value)
      })
  } catch (error) {
    algoSalioMalError(currentTheme.value)
  }
}

const abrirEdicion = item => {
  dialog.value = !dialog.value
  puestoSeleccionado.value = Object.assign({}, item)
}

const cancelarDialog = () => {
  dialog.value = !dialog.value
}

const confirmarEdicionSueldo = () => {
  form.value.validate().then(response => {
    if (response.valid) {
      let params = {
        valorHoraNormal: null,
        valorHoraFerDom: null,
        plusEncargado: null,
        basicoDueno: null
      }

      if (puestoSeleccionado.value.idSueldosBasicos == 1) {
        params.valorHoraNormal = puestoSeleccionado.value.valorHoraNormal
        params.valorHoraFerDom = puestoSeleccionado.value.valorHoraFerDom
      }
      if (puestoSeleccionado.value.idSueldosBasicos == 2) {
        params.valorHoraNormal = puestoSeleccionado.value.valorHoraNormal
        params.valorHoraFerDom = puestoSeleccionado.value.valorHoraFerDom
        params.plusEncargado = puestoSeleccionado.value.plusEncargado
      }
      if (puestoSeleccionado.value.idSueldosBasicos == 3) {
        params.basicoDueno = puestoSeleccionado.value.basicoDueno
      }

      try {
        axios.put(`/SueldosBasicos/UpdateSueldosBasicos/${puestoSeleccionado.value.idSueldosBasicos}`, params).then(() => {
          registroExitosoMensaje('sueldo', currentTheme.value)
          form.value.reset();
          obtenerSueldos();
          dialog.value = !dialog.value
        }).catch(() => {
          algoSalioMalError(currentTheme.value)
        })
      } catch {
        algoSalioMalError(currentTheme.value)
      }
    }
  })
}

onMounted(() => {
  obtenerSueldos()
})
</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Sueldos</h2>
      <VRow>
        <VCol
          cols="12"
          md="12"
        >
          <VTable>
            <thead>
              <tr>
                <th class="text-uppercase">PUESTO</th>
                <th class="text-uppercase">HORA NORMAL</th>
                <th class="text-uppercase">HORA DOMINGO/FERIADO</th>
                <th class="text-uppercase">PLUS DE ENCARGADO</th>
                <th class="text-uppercase">BÁSICO DEL DUEÑO</th>
                <th class="text-uppercase">OPCIONES</th>
              </tr>
            </thead>

            <tbody>
              <tr
                v-for="item in puestosMock"
                :key="item.idSueldosBasicos"
              >
                <td>{{ item.nombre }}</td>
                <td>{{ item.valorHoraNormal ? `$ ${item.valorHoraNormal}` : '------' }}</td>
                <td>{{ item.valorHoraFerDom ? `$ ${item.valorHoraFerDom}` : '------' }}</td>
                <td>{{ item.plusEncargado ? `$ ${item.plusEncargado}` : '------' }}</td>
                <td>{{ item.basicoDueno ? `$ ${item.basicoDueno}` : '------' }}</td>
                <td>
                  <IconBtn
                    icon="ri-edit-2-fill"
                    color="primary"
                    class="me-1"
                    @click="abrirEdicion(item)"
                  />
                </td>
              </tr>
            </tbody>
          </VTable>
        </VCol>

        <!-- Modal -->
        <VDialog
          persistent
          v-model="dialog"
          width="60%"
        >
          <VCard
            prepend-icon="ri-add-large-fill"
            title="Editar sueldo"
            class="px-5 py-5"
          >
            <VForm
              @submit.prevent="confirmarEdicionSueldo"
              ref="form"
            >
              <VRow>
                <VCol
                  cols="12"
                  md="6"
                  v-if="puestoSeleccionado.idSueldosBasicos == 1 || puestoSeleccionado.idSueldosBasicos == 2"
                >
                  <VTextField
                    v-model="puestoSeleccionado.valorHoraNormal"
                    prepend-inner-icon="ri-money-dollar-circle-line"
                    :rules="[reglaObligatoria()]"
                    label="Hora estándar"
                    type="number"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="6"
                  v-if="puestoSeleccionado.idSueldosBasicos == 1 || puestoSeleccionado.idSueldosBasicos == 2"
                >
                  <VTextField
                    v-model="puestoSeleccionado.valorHoraFerDom"
                    prepend-inner-icon="ri-money-dollar-circle-line"
                    :rules="[reglaObligatoria()]"
                    label="Hora domingo/feriado"
                    type="number"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="12"
                  v-if="puestoSeleccionado.idSueldosBasicos == 2"
                >
                  <VTextField
                    v-model="puestoSeleccionado.plusEncargado"
                    prepend-inner-icon="ri-money-dollar-circle-line"
                    :rules="[reglaObligatoria()]"
                    label="Plus de encargado General"
                    type="number"
                  />
                </VCol>
                <VCol
                  cols="12"
                  md="12"
                  v-if="puestoSeleccionado.idSueldosBasicos == 3"
                >
                  <VTextField
                    v-model="puestoSeleccionado.basicoDueno"
                    prepend-inner-icon="ri-money-dollar-circle-line"
                    :rules="[reglaObligatoria()]"
                    label="Básico dueño"
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
