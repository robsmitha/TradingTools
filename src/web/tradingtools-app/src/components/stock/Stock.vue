<template>
    <v-container fluid>
        <h1 class="font-weight-light mb-0">{{stock}}</h1>
    </v-container>
</template>

<script>
export default {
  data: () => ({
    symbol: null
  }),
  computed: {
    stock: {
      get: function(){
        return this.symbol
      },
      set: function(val) {
        this.symbol = val
      }
    }
  },
  watch:{
      $route (to, from){
          if(to.fullPath !== from.fullPath){
              this.stock = to.params.symbol
          }
      }
  },
  created(){
    this.symbol = this.$router.currentRoute.params.symbol
  }
}
</script>


