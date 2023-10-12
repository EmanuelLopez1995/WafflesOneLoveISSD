<template>
  <div id="mainApp">
    <NavBar />
    <router-view/>
  </div>
</template>

<script>

import NavBar from '@/components/NavBar/NavBar.vue'
export default {
  components: {
    NavBar
  }
}
</script>

