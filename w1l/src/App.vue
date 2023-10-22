<template>
  <div id="mainApp">
    <NavBar v-if="usuarioAutenticado"/>
    <router-view/>
  </div>
</template>

<script>

import NavBar from '@/components/NavBar/NavBar.vue'
import Login from '@/views/Login/Login.vue'

export default {
  components: {
    NavBar,
    Login
  },
  data() {
    return {
    }
  },
  computed: {
    usuarioAutenticado(){
      return this.$store.state.usuarioAutenticado;
    }
  },
  methods: {

  }
}
</script>

