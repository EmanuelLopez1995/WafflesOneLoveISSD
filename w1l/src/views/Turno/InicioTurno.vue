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
                        <v-row v-for="(empleado, index) in empleadosPresentes" :key="index" class="mb-0 mt-0">
                            <v-col cols="3" class="pt-0 pb-0">
                                <v-select
                                    no-data-text="No hay mas empleados"
                                    density="compact"
                                    v-model="empleado.empleado"
                                    :items="empleadosDisponibles"
                                    item-title="nombreCompleto"
                                    return-object
                                    :rules="reglas.notNull"
                                    label="Empleado"
                                    class="pt-0"
                                    @update:modelValue="eliminarAgregarItemDeArray(empleado.empleado)"
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
                            <v-col cols="7">
                                <v-select
                                    no-data-text="No hay empleados"
                                    density="compact"
                                    v-model="encargadoDeTurno"
                                    :items="allEmpleados"
                                    item-title="nombreCompleto"
                                    return-object
                                    :rules="reglas.notNull"
                                    label="Encargado de turno"
                                ></v-select>
                            </v-col>
                        </v-row>
                        <v-row class="justify-center">
                            <v-col cols="7">
                                <v-select
                                    no-data-text="No hay empleados"
                                    density="compact"
                                    v-model="encargadoDeAperturaCaja"
                                    :items="empleadosPresentes"
                                    item-title="empleado.nombreCompleto"
                                    return-object
                                    :rules="reglas.notNull"
                                    label="Encargado de apertura"
                                ></v-select>
                            </v-col>
                        </v-row>
                        <v-row class="justify-center">
                            <v-col cols="7">
                                <p class="text-h6 text-left">Activo inicial: ${{activoInicial}}</p>
                            </v-col>
                        </v-row>
                        <v-row class="justify-center">
                            <v-col cols="7" class="apertura">
                                <v-chip v-if="aperturaCorrecta === true" color="green">Apertura correcta</v-chip>
                                <v-chip v-if="aperturaCorrecta === false" color="red">Apertura incorrecta</v-chip>
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
import {algoSalioMalError} from '@/components/Swal/SwalCustom.js';

export default {
    data: ()=> ({
        empleadosDisponibles: [],
        allEmpleados: [],
        empleadosPresentes: [
            { empleado: null, horaIngreso: null, notas: null }
        ],
        itemsTurno: ['1', '2'],
        turno: null,
        esFeriado: false,
        notas: null,
        fecha: '',
        hora: '',
        siguienteClickeado: false,
        encargadoDeAperturaCaja: null,
        encargadoDeTurno: null,
        billetes: [10, 20, 50, 100, 500, 1000],
        valoresBilletes: [],
        sumaBilletes: [],
        valorTotal: 0,
        notasGenerales: '',
        activoInicial: 15000,
        aperturaCorrecta: null
        
    }),
    created() {
        this.definirFechaYhora();
        this.establecerTurnoSegunHora();
        this.getEmpleados();
    },
    computed: {
        reglas() {
            return this.$store.getters['validaciones/reglas'];
        }
    },
    methods: {
        getEmpleados() {
            this.$http.get('/employees/get-all').then((response) =>{
                this.allEmpleados = response.data.map((empleado) => {
                    return {
                        ...empleado,
                        nombreCompleto: `${empleado.nombre} ${empleado.apellido}`,
                    };
                });
                this.empleadosDisponibles = [...this.allEmpleados]; 
            })
            .catch(error => {
                algoSalioMalError();
            });
        },
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
            this.empleadosPresentes.push({ empleado: null, horaIngreso: null, notas: null });
        },
        eliminarFila(index, empleado) {
            this.empleadosPresentes.splice(index, 1);
            if(empleado != null) {
                this.empleadosDisponibles.push(empleado);
            }
        },
        eliminarAgregarItemDeArray(empleado) {

            //se elimina si o si 
            let indiceAEliminar = this.empleadosDisponibles.indexOf(empleado);
            if (indiceAEliminar !== -1) {
                this.empleadosDisponibles.splice(indiceAEliminar, 1);
            } 

            //verificar que este en uno y no en otro - NO TOCAR
            this.allEmpleados.forEach(empleado => {
                let estaEnPresentes = this.empleadosPresentes.some(e => e.empleado.id === empleado.id);
                let estaEnDisponibles = this.empleadosDisponibles.some(e => e.id === empleado.id);

                if(!estaEnPresentes && !estaEnDisponibles) {
                    this.empleadosDisponibles.push(empleado);
                }
            });
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
            this.verificarAperturaCorrecta();
        },
        verificarAperturaCorrecta() {
            if(this.valorTotal === this.activoInicial) {
                this.aperturaCorrecta = true;
            } else {
                this.aperturaCorrecta = false;
            }
        },
        volverAInicioTurno(){
            this.siguienteClickeado = false;
        },
    }
}
</script>
