<template>
  <div>
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
      <div class="correoLabel text-subtitle-1 text-medium-emphasis">Email</div>

      <v-text-field
        density="compact"
        placeholder="Correo electrónico"
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
        class="mb-8"
        color="#42b883"
        size="large"
        variant="tonal"
      >
        Ingresar
      </v-btn>

      <v-card-text class="text-center">
        <a
          class="linkLogin text-decoration-none"
          href="#"
          rel="noopener noreferrer"
          target="_blank"
        >
          Crea tu cuenta <v-icon icon="mdi-chevron-right"></v-icon>
        </a>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
  import './Login.scss'

  export default {
    data: () => ({
      visible: false,
    }),
  }
</script>

<style>

</style>