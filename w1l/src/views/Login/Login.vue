<template>
  <div>
    <div v-if="loginVisible">
      <img
        class="logoSSLIT mx-auto my-3 mt-10"
        max-width="228"
        src="../../assets/img/SSLIT.png"
      />

      <v-card
        id="cardLogin"
        class="mx-auto pa-12 pb-8"
        elevation="8"
        max-width="448"
        rounded="lg"
      >
      <v-form @submit.prevent="ingresar" ref="form">
        <div class="correoLabel text-subtitle-1 text-medium-emphasis">Email</div>

        <v-text-field
          density="compact"
          placeholder="Correo electrónico"
          :rules="reglas.email"
          prepend-inner-icon="mdi-email-outline"
          variant="outlined"
        ></v-text-field>

        <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
          Contraseña

          <a
            id="olvidastePassw" 
            class="linkLogin text-caption text-decoration-none"
            href="#"
            rel="noopener noreferrer"
            target="_blank"
          >
            Olvidaste tu contraseña?</a>
        </div>

        <v-text-field
          :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
          :rules="reglas.password"
          :type="visible ? 'text' : 'password'"
          density="compact"
          placeholder="Ingrese su contraseña"
          prepend-inner-icon="mdi-lock-outline"
          variant="outlined"
          @click:append-inner="visible = !visible"
        ></v-text-field>

        <v-card
          class="mb-12"
          color="surface-variant"
          variant="tonal"
        >
          <v-card-text class="text-medium-emphasis text-caption">
            Si olvidaste tu contraseña puedes darle click a "Olvidaste tu contraseña?" para resetearla 
          </v-card-text>
        </v-card>

        <v-btn
          block
          type="submit"
          class="mb-8"
          color="#42b883"
          size="large"
          variant="tonal"
        >
          Ingresar
        </v-btn>
      </v-form>
      </v-card>
    </div>
    <!-- Seccion crear cuenta -->

  </div>
</template>

<script>
  import './Login.scss'

  export default {
    components: {
      
    },
    data: () => ({
      visible: false,
      loginVisible: true,
    }),
    computed: {
      reglas() {
        return this.$store.getters['validaciones/reglas'];
      },
    },
    methods: {
      ingresar() {
        this.$refs.form.validate().then(response => {
          if (response.valid) {
            //Agregar logica al back 
            localStorage.setItem('usuarioAutenticado', true); // esto cambiaría desde el back
            this.$router.push('/')
          }
        });
      }
    }
  }
</script>

<style>

</style>