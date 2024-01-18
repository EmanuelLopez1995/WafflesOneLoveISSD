<template>
  <div class="pt-4">
        <v-form v-if="!siguienteClickeado" class="formInicioTurno" @submit.prevent="iniciarCaja()" ref="form" border>
                <v-container >
                    <h1>Iniciar turno</h1>
                    <v-row>
                        <v-col cols="2">
                            <v-text-field
                                v-model="fecha"
                                :rules="reglas.notNull"
                                :disabled="true"
                                label="Fecha"
                                type="date"
                                class="inputsFormProveedor"
                            ></v-text-field>
                        </v-col>
                        <v-col cols="2">
                            <v-select
                                v-model="turno"
                                :items="itemsTurno"
                                :rules="reglas.notNull"
                                label="Turno"
                                class="inputsFormEmpleados"
                            ></v-select>
                        </v-col>
                        <v-col cols="5">
                            <v-text-field
                                v-model="notasGenerales"
                                label="Notas generales"
                                class="inputsFormProveedor"
                            ></v-text-field>
                        </v-col>
                        <v-col class="pt-3 d-flex align-center">
                            <v-checkbox 
                                hide-details = true
                                v-model="esFeriado"
                                label="Domingo/Feriado"
                                value= "1"
                                type="checkbox"
                            ></v-checkbox>
                        </v-col>
                    </v-row>
                    <!-- <hr color="#42b983"> -->
                    <br>
                    <div class="contenedorEmpleadosIniTurno">
                        <v-row v-for="(empleado, index) in empleados" :key="index" class="mb-0 mt-0">
                            <v-col cols="3" class="pt-0 pb-0">
                                <v-select
                                    no-data-text="No hay mas empleados"
                                    density="compact"
                                    v-model="empleado.empleado"
                                    :items="itemsEmpleados"
                                    :rules="reglas.notNull"
                                    label="Empleado"
                                    class="pt-0"
                                    @update:modelValue="eliminarItemDeArray(empleado.empleado)"
                                ></v-select>
                            </v-col>
                            <v-col cols="3" class="pt-0 pb-0">
                                <v-text-field
                                    v-model="empleado.horaIngreso"
                                    label="Hora de ingreso"
                                    :rules="reglas.notNull"
                                    type="time"
                                    class="pt-0"
                                    density="comfortable"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="5" class="pt-0 pb-0">
                                <v-text-field
                                    v-model="empleado.notas"
                                    label="Notas"
                                    class="pt-0"
                                    density="comfortable"
                                ></v-text-field>
                            </v-col>
                            <span class="deleteButton" @click="eliminarFila(index, empleado.empleado)"></span>
                        </v-row>
                        <v-btn class="me-10 mt-0 d-flex" color="light-blue-darken-4" prepend-icon="mdi-plus" @click="agregarFila()">Agregar fila</v-btn>
                    </div>
                    <br>
                    <v-row justify="end" class="mt-4">
                        <v-btn class="me-10 mt-0 d-flex" color="green-darken-4" type="submit" append-icon="mdi-arrow-right">Siguiente</v-btn>
                    </v-row>
                </v-container>
        </v-form>
        <v-form v-else>
            <v-container>
                <v-row>
                    <v-col>
                        <h1>Apertura de caja</h1>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-row class="justify-center">
                            <v-col cols="6">
                                <v-select
                                    no-data-text="No hay empleados"
                                    density="compact"
                                    v-model="encargadoDeCaja"
                                    :items="itemsEmpleados"
                                    :rules="reglas.notNull"
                                    label="Encargado de caja"
                                ></v-select>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <p class="text-h6 activoInicial">Activo inicial: $2500</p>
                            </v-col>
                        </v-row>
                    </v-col>
                    <v-col>
                        <v-row>
                            <v-col>
                                  <v-table class="tablaBilletes" theme="dark">
                                    <thead>
                                        <tr>
                                            <th class="text-center">
                                            Billete
                                            </th>
                                            <th class="text-center">
                                            Cantidad
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <tr
                                        v-for="(billete, index) in billetes"
                                        :key="billete"
                                    >
                                        <td>${{ billete }}</td>
                                        <td>
                                            <v-text-field
                                                v-model="valoresBilletes[index]"
                                                variant="solo-filled"
                                                type="number"
                                                class="pt-0"
                                                density="compact"
                                                :hide-details="true"
                                                @input="sumarValores(index)"
                                            ></v-text-field>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TOTAL
                                        </td>
                                        <td>
                                            ${{valorTotal}}
                                        </td>
                                    </tr>
                                    </tbody>
                                </v-table>
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
                <v-row class="mt-10">
                    <v-col>
                        <v-row>
                            <v-btn class="me-10 mt-0 d-flex" color="light-blue-darken-4" prepend-icon="mdi-arrow-left" @click="volverAInicioTurno()">Atras</v-btn>
                        </v-row>
                    </v-col>
                    <v-col>
                        <v-row justify="end" >
                            <v-btn class="me-10 mt-0 d-flex" color="green-darken-4" type="submit" append-icon="mdi-arrow-right">Finalizar</v-btn>
                        </v-row>
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
    </div>
</template>

<script>

import './InicioTurno.scss';

export default {
    data: ()=> ({
        itemsEmpleados: ['Ema', 'Nacho', 'Yorch'],
        itemsTurno: ['1', '2'],
        turno: null,
        empleados: [
            { empleado: null, horaIngreso: null, notas: null }
        ],
        esFeriado: false,
        notas: null,
        fecha: '',
        hora: '',
        siguienteClickeado: false,
        encargadoDeCaja: null,
        billetes: [10, 20, 50, 100, 500, 1000],
        valoresBilletes: [],
        sumaBilletes: [],
        valorTotal: 0
        
    }),
    created() {
        this.definirFechaYhora();
        this.establecerTurnoSegunHora();
    },
    computed: {
        reglas() {
            return this.$store.getters['validaciones/reglas'];
        }
    },
    methods: {
        definirFechaYhora() {
            const fechaActual = new Date();

            const dia = fechaActual.getDate();
            const mes = fechaActual.getMonth() + 1; 
            const anio = fechaActual.getFullYear();

            const diaFormateado = (dia < 10) ? '0' + dia : dia;
            const mesFormateado = (mes < 10) ? '0' + mes : mes;

            const fechaFormateada = `${anio}-${mesFormateado}-${diaFormateado}`;

            const hora = fechaActual.getHours();
            const minutos = fechaActual.getMinutes();

            const horaFormateada = (hora < 10) ? '0' + hora : hora;
            const minutosFormateados = (minutos < 10) ? '0' + minutos : minutos;

            const horaCompleta = `${horaFormateada}:${minutosFormateados}`;

            this.fecha = fechaFormateada;
            this.hora = horaCompleta;
        },
        establecerTurnoSegunHora() {

        },
        agregarFila() {
            this.empleados.push({ empleado: null, horaIngreso: null, notas: null });
        },
        eliminarFila(index, empleado) {
            this.empleados.splice(index, 1);
            if(empleado != null) {
                this.itemsEmpleados.push(empleado);
            }
        },
        eliminarItemDeArray(empleado) {
            let indiceAEliminar = this.itemsEmpleados.indexOf(empleado);
            if (indiceAEliminar !== -1) {
                this.itemsEmpleados.splice(indiceAEliminar, 1);
            }
        },
        iniciarCaja() {
            this.$refs.form.validate().then(response => {
                if (response.valid) {
                    this.siguienteClickeado = true;
                }
            });
        },
        sumarValores(index) {
            this.sumaBilletes[index] = this.billetes[index] * this.valoresBilletes[index];
            this.valorTotal = this.sumaBilletes.reduce((suma, numero) => suma + numero, 0);
        },
        volverAInicioTurno(){
            this.siguienteClickeado = false;
        },
    }
}
</script>
