<script setup>
import { reglaObligatoria, validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError, registroExitosoMensaje, eliminarRegistro } from '@/components/SwalCustom.js'
import { ref } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios'

const empleadoABuscar = ref(null)
const empleados = ref([])
const fechaDesde = ref(null) 
const fechaHasta = ref(null) 

const paginas = [
    {value: 10, title: '10'},
    {value: 25, title: '25'},
    {value: 50, title: '50'},
    {value: 100, title: '100'},
    {value: -1, title: 'Todos'}
]

const titulosTabla = [          
    {
      key: 'id',
      sortable: false,
      title: 'ID',
    },
    {
      key: 'nombre',
      sortable: false,
      title: 'NOMBRE',
    },
    {
      key: 'apellido',
      sortable: false,
      title: 'APELLIDO',
    },
    {
      key: 'dni',
      sortable: false,
      title: 'DNI',
    },      
    {
      key: 'numero',
      sortable: false,
      title: 'TELÉFONO',
    },
    {
      key: 'direccion',
      sortable: false,
      title: 'DIRECCIÓN',
    }, 
    {
      key: 'email',
      sortable: false,
      title: 'EMAIL',
    },
    {
      key: 'posicion',
      sortable: false,
      title: 'PUESTO',
    },
    {
      key: 'opciones',
      sortable: false,
      title: 'OPCIONES',
    }
]

const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
  return ref(vuetifyTheme.current.value.colors)
})

const registrarAdelantos = () => {
  // form.value.validate().then(response => {
  //     if (response.valid) {
  //         let params = {
  //           nombre: nombre.value,
  //           apellido: apellido.value,
  //           dni: dni.value,
  //           numero: telefono.value,
  //           direccion: direccion.value,
  //           email: email.value,
  //           posicion: puesto.value
  //         }
  //         try {
  //             axios.post('/employees', params).then(() => {
  //                 registroExitosoMensaje('empleado', currentTheme.value)
  //                 form.value.reset();
  //             })
  //         } catch {
  //             algoSalioMalError(currentTheme.value)
  //         }
  //     }
  // })
}

const obtenerEmpleados = async () => {
  try {
    axios.get('/employees/get-all').then(response => {
      empleados.value = response.data.map(empleado => {
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

onMounted(obtenerEmpleados)
</script>

<template>
  <VCard>
    <VCardItem>
      <h2 class="pb-3 mt-3">Registrar adelantos de sueldo</h2>
      <VRow>
        <VCol
          cols="12"
          md="12"
        >
          <VBtn
            color="primary"
            class="mt-1"
          >
            Registrar adelanto
          </VBtn>
        </VCol>
        <VCol
          cols="12"
          md="12"
        >
          <h2>Registros</h2>
        </VCol>
        <VCol
          cols="12"
          md="4"
        >
          <VSelect
            v-model="empleadoABuscar"
            :rules="[reglaObligatoria()]"
            :items="empleados"
            item-title="nombreCompletoYid"
            return-object
            label="Empleado"
          />
        </VCol>
        <VCol
          cols="12"
          md="1"
        >
          <VBtn
            color="primary"
            variant="outlined"
            class="mt-1"
          >
            Buscar
          </VBtn>
        </VCol>
        <VCol cols="12" md="3"/>
        <VCol
          cols="12"
          md="2"
        >
          <VTextField 
              v-model="fechaDesde"
              label="Desde"
              type="date"
          />
        </VCol>
        <VCol
          cols="12"
          md="2"
        >
          <VTextField 
              v-model="fechaHasta"
              label="Hasta"
              type="date"
          />
        </VCol>

        <VCol
          cols="12"
          md="12"
        >
          <VDataTable
            :items="empleados" 
            :headers="titulosTabla"
            :items-per-page-options="paginas"
            items-per-page-text="Items por página:"
            no-data-text="No hay registros"
          >
            <template v-slot:[`item.opciones`]="{ item }">
                <IconBtn
                  icon="ri-edit-2-fill"
                  color="primary"
                  class="me-1"
                />
                <IconBtn
                  icon="ri-delete-bin-5-fill"
                  color="error-darken-1"
                  class="me-1"
                  @click="eliminarRegistro(eliminar, item.id, item.nombre, currentTheme.value)"
                />
            </template>
          </VDataTable>
        </VCol>
      </VRow>
    </VCardItem>
  </VCard>
</template>
