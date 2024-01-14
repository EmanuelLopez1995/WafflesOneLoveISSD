<template>
    <div>
        <h2 class="pt-4">Registrar proveedor</h2>
        <v-form @submit.prevent="registrarProveedor" ref="form">
            <v-container id="formRegistroProveedor" >
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="nombreFantasiaProveedor"
                            :rules="reglas.nombre"
                            label="Nombre de fantasía"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="razonSocialProveedor"
                            label="Razón social"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>

                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="domicilioProveedor"
                            label="Domicilio"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="telefonoProveedor"
                            label="Teléfono"
                            type="tel"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field
                            v-model="cuitProveedor"
                            label="CUIT"
                            type = "number"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                    <v-col>
                        <v-text-field
                            v-model="emailProveedor"
                            type="email"
                            label="Email"
                            class="inputsFormProveedor"
                        ></v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-textarea
                            v-model="detalleProveedor"
                            label="Detalle"
                            class="textareaProveedor"
                        ></v-textarea>
                    </v-col>
                </v-row>
            </v-container>
            <br>
            <v-btn v-if="!loading" class="me-10" type="submit" color="green-darken-4"> Registrar </v-btn>

            <v-btn v-if="!loading" @click="resetForm" class="botonLimpiar " color="#8A1212"> Limpiar </v-btn>
        </v-form>
        <v-progress-circular v-if="loading"
            :width="5"
            color="green"
            indeterminate
        ></v-progress-circular>
        <snack-bar v-if="submitFinalizado" :exitoso="esExitoso" @timeout-completado="submitFinalizado = false" />
    </div>
</template>

<script>
import './RegistrarProveedor.scss'
import SnackBar from '@/components/SnackBar/SnackBar.vue'


export default {

    components: {
        SnackBar
    },
    data: () => ({
        nombreFantasiaProveedor: '',
        razonSocialProveedor: '',
        domicilioProveedor: '',
        telefonoProveedor: '',
        cuitProveedor: '',
        emailProveedor: '',
        detalleProveedor: '',
        submitFinalizado: false,
        esExitoso: false,
        loading: false
    }),
    computed: {
        reglas() {
        return this.$store.getters['validaciones/reglas'];
        }
    },
    methods: {
        resetForm () {
            this.$refs.form.reset()
        },
        registrarProveedor() {
            this.$refs.form.validate().then(response => {
                if (response.valid) {
                    
                    let params = {
                        Nombre: this.nombreFantasiaProveedor,
                        RazonSocial: this.razonSocialProveedor,
                        Direccion: this.domicilioProveedor,
                        Numero: this.telefonoProveedor,
                        Cuit: this.cuitProveedor,
                        Email: this.emailProveedor
                    }
                    this.loading = true;
                    this.$http.post('/suppliers', params).then((response) =>{
                        this.loading = false;
                        if(response.status == 200){
                            this.esExitoso = true;
                        }else{
                            this.esExitoso = false;
                        }
                        this.resetForm();
                        this.submitFinalizado = true

                    }).catch((error)=>{
                        this.esExitoso = false;
                    })
                }
            });
        }
    }
}
</script>