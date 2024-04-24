<script setup>

import { reglaObligatoria , validarEmail } from '@/components/validaciones.js'
import { algoSalioMalError , registroExitosoMensaje } from '@/components/SwalCustom.js'
import { ref, computed } from 'vue'
import { useTheme } from 'vuetify'
import axios from 'axios';

const proveedor = ref('');
const fechaEmision = ref(null);
const nroFactura = ref(null);
const tipoFactura = ref('');
const puntoDeVenta = ref('');
const totalFactura = ref(0);
const productos = ref([
  {
    id: 1,
    nombreProducto: 'Queso', 
  },
  {
    id: 2,
    nombreProducto: 'Coca Cola', 
  },
  {
    id: 3,
    nombreProducto: 'Harina 000', 
  },
])
const productosSeleccionados = ref([])


const vuetifyTheme = useTheme()

const currentTheme = computed(() => {
    return ref(vuetifyTheme.current.value.colors)
})

const registrarCompra = () => {
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

const calcularSubtotal = (cantidad, precioUnitario, id) => {
  if (cantidad !== undefined && precioUnitario !== undefined) {
    console.log(id) // establecer subtotal con el id
    calcularTotal();
    return cantidad * precioUnitario;
  } else {
    calcularTotal();
    return 0;
  }
}

const deseleccionarProducto = producto => {
  const index = productosSeleccionados.value.findIndex(e => e.id === producto.id)
  if (index !== -1) {
    productosSeleccionados.value.splice(index, 1)
  }
}

const calcularTotal = () => {
  // const total = productosSeleccionados
}
</script>

<template>
  <VCard>
    <VCardItem>
      <VForm @submit.prevent="registrarCompra" ref="form" class="pt-2">
        <VRow>
          <VCol
            cols="12"
            md="3"
          >
            <VSelect
              v-model="proveedor"
              :rules="[reglaObligatoria()]"
              :items="['Proveedor X']"
              label="Proveedor"
            />
          </VCol>
          <VCol
            cols="12"
            md="2"
          >
            <VTextField
              v-model="fechaEmision"
              type="date"
              :rules="[reglaObligatoria()]"
              label="Fecha de emisión"
            />
          </VCol>

          <VCol
            cols="12"
            md="1"
          >
            <VSelect
                v-model="tipoFactura"
                :label="`Tipo`"
                :items="['A', 'B', 'C', 'E', 'M', 'T']"
                :rules="[reglaObligatoria()]"
            ></VSelect>
          </VCol>
          <VCol
            cols="12"
            md="2"
          >
            <VTextField
                v-model="puntoDeVenta"
                :label="`Punto de venta`"
                :rules="[reglaObligatoria()]"
                type="number"
            ></VTextField>
          </VCol>
          <VCol
            cols="12"
            md="3"
          >
            <VTextField
                v-model="nroFactura"
                :label="`Número de factura`"
                :rules="[reglaObligatoria()]"
                type="number"
            ></VTextField>
          </VCol>

          <VCol
            cols="12"
            md="12"
          >
            <VCol
              cols="12"
              md="12"
            >
              <VAutocomplete
                v-model="productosSeleccionados"
                :items="productos"
                item-title="nombreProducto"
                multiple
                return-object
                :rules="[reglaObligatoria()]"
                label="Productos"
                placeholder="Seleccione los productos comprados"
              />
            </VCol>

            <VCol
              cols="12"
              md="12"
              v-for="(producto) in productosSeleccionados"
              :key="producto.id"
            >
              <VRow>
                <VCol cols="1">
                  <VTextField
                    v-model="producto.cantidad"
                    label="Cantidad"
                    active
                    :rules="[reglaObligatoria()]"
                    type="number"
                  >
                  </VTextField>
                </VCol>
                <VCol cols="5">
                  <VTextField
                    active
                    v-model="producto.nombreProducto"
                    label="Producto"
                    :rules="[reglaObligatoria()]"
                    readonly
                  >
                  </VTextField>
                </VCol>
                <VCol cols="2">
                  <VTextField
                    v-model="producto.precioUnitario"
                    active
                    label="Precio unitario"
                    :rules="[reglaObligatoria()]"
                    prefix="$"
                    type="number"
                  >
                  </VTextField>
                </VCol>
                <VCol cols="2">
                  <VTextField
                    v-model="producto.subtotal"
                    active
                    label="Subtotal"
                    :value="calcularSubtotal(producto.cantidad, producto.precioUnitario, producto.id)"
                    readonly
                    prefix="$"
                    type="number"
                  >
                  </VTextField>
                </VCol>
                <VCol cols="2">
                  <VBtn
                    size="small"
                    color="error-darken-1"
                    class="mt-1"
                    @click="deseleccionarProducto(producto)"
                  >
                    X
                  </VBtn>
                </VCol>
              </VRow>
              <VRow>
                <VCol cols="8">
                </VCol>
                <VCol cols="2">
                  <VTextField 
                      v-model="totalFactura"
                      active
                      label="TOTAL"
                      :value="calcularTotal()"
                      readonly
                      prefix="$"
                      type="number"
                  />
                </VCol>
              </VRow>
            </VCol>
          </VCol>

          <VCol
            cols="12"
            class="d-flex gap-4"
          >
            <VBtn type="submit"> Registrar </VBtn>

            <VBtn
              type="reset"
              color="secondary"
              variant="outlined"
            >
              Limpiar
            </VBtn>
          </VCol>
        </VRow>
      </VForm>
    </VCardItem>
  </VCard>
</template>
