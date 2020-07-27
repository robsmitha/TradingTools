<template>
    <v-container fluid>
        <h1 class="font-weight-light mb-0">{{stock}}</h1>
        <small class="d-block pb-2 mb-3">These quotes are loaded from IEX Cloud.</small>
        
        <v-skeleton-loader
            type="list-item-two-line"
            class="mx-auto"
            v-if="quote.loading"
            transition="scale-transition"
        >
        </v-skeleton-loader>
        <ErrorMessage 
        v-else-if="!quote.success" 
        message="Could not load quote" />
        <div v-else>
          <v-row>
            <v-col md="3" xs="12" cols="12">
              <v-list>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.change}}</v-list-item-title>
                    <v-list-item-subtitle>Change</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.changePercent}}</v-list-item-title>
                    <v-list-item-subtitle>Change %</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.open}}</v-list-item-title>
                    <v-list-item-subtitle>Open</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.close}}</v-list-item-title>
                    <v-list-item-subtitle>Close</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.high}}</v-list-item-title>
                    <v-list-item-subtitle>High</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.low}}</v-list-item-title>
                    <v-list-item-subtitle>Low</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item two-line>
                  <v-list-item-content>
                    <v-list-item-title>{{quote.data.volume}}</v-list-item-title>
                    <v-list-item-subtitle>Volume</v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-col>
            <v-col md="9" xs="12" cols="12">
              <PatternsTable 
              :patterns="stockTradingPatterns" 
              />
            </v-col>
          </v-row>
        </div>
    </v-container>
</template>

<script>
import { mapState } from 'vuex'
import ErrorMessage from "./../_helpers/ErrorMessage"
import PatternsTable from "./../_helpers/PatternsTable"

export default {
  components:{
    ErrorMessage,
    PatternsTable
  },
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
    },
    stockTradingPatterns(){
      return this.patterns.success && this.symbol ? this.patterns.data.filter(p => p.symbol.indexOf(this.symbol) !== -1) : []
    },
    ...mapState({
      quote: state => state.tradingPatterns.quote,
      patterns: state => state.tradingPatterns.patterns
    })
  },
  watch:{
      $route (to, from){
          if(to.fullPath !== from.fullPath){
              this.loadSymbol(to.params.symbol)
          }
      }
  },
  created(){
    this.loadSymbol(this.$route.params.symbol)
    this.$store.dispatch('tradingPatterns/getTradingPatterns')
  },
  methods:{
    loadSymbol(symbol){
      this.stock = symbol
      this.$store.dispatch('tradingPatterns/getQuote', { symbol })
    }
  }
}
</script>


