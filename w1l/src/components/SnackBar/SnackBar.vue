<template>
  <div class="text-center">

    <v-snackbar
        :color="exitoso ? '#1B5E20' : '#8A1212'"   
        v-model="showSnackbar"
        :timeout="timeout"
    >
    <v-icon v-if="exitoso"
        start
        icon="mdi-checkbox-marked-circle"
    ></v-icon>
    <v-icon v-else
        start
        icon="mdi-cancel"
    ></v-icon>
    <span class="mensajeDeSnackBar">
        {{ exitoso ? 'OPERACIÓN EXITOSA' : 'OPERACIÓN FALLIDA' }}
    </span>

        <template v-slot:actions>
            <v-btn
            variant="text"
            @click="showSnackbar = false"
            >
            Cerrar
            </v-btn>
        </template>
    </v-snackbar>
</div>
</template>

<script>

import './SnackBar.scss'

export default {

    props: {
        exitoso: {
            type: Boolean,
            required: true
        }
    },
    data: () => ({
        showSnackbar: null,
        timeout: 1800,
    }),
    watch: {
        showSnackbar(newValue) {
            if (!newValue) {
                this.$emit('timeout-completado');
            }
        }
    },
    created() {
        this.showSnackbar = true;
    },
  }
</script>