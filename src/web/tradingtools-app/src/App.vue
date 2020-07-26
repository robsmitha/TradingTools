<template>
  <v-app id="inspire">
    <AppBar />
    <v-main>
      <router-view></router-view>
    </v-main>
    <Footer />
  </v-app>
</template>

<script>
  import AppBar from './components/_layout/AppBar'
  import Footer from './components/_layout/Footer'
  export default {
    components: {
      AppBar,
      Footer
    }
  }
</script>