<template>
    <v-container fluid>
        <v-row>
          <v-col md="4" sm="6" xs="12" v-for="i in items" :key="i.title">
            <v-card
              dark
              :to="i.to"
              :color="i.color"
            >
              <v-list-item three-line>
                <v-list-item-avatar
                  size="80"
                  :color="`${i.color} darken-3`"
                >
                  <v-icon size="40" color="white">{{i.icon}}</v-icon>
                </v-list-item-avatar>
                <v-list-item-content>
                  <div class="overline mb-1">{{i.title}}</div>
                  <v-list-item-title class="headline mb-2">
                    
                    <v-progress-circular
                      v-if="!i.content"
                      indeterminate
                      color="white"
                    ></v-progress-circular>
                    <span v-else>
                      {{i.content}}
                    </span>
                    
                  </v-list-item-title>
                  <v-list-item-subtitle>
                    {{i.subtitle}}
                  </v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-card>
          </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { mapState } from 'vuex'

export default {
    data: () => ({
      items: [
        //{ color: 'deep-purple', icon: 'mdi-apps', title: 'Watching', subtitle: 'Stocks on the watch list', content: null, to: '/watching' },
        { color: 'green', icon: 'mdi-finance', title: 'Patterns', subtitle: 'Chart patterns we have observed', content: null, to: '/patterns' },
        { color: 'blue', icon: 'mdi-twitter', title: 'Tweets', subtitle: 'Possible trades from twitter', content: null, to: '/tweets' }
      ],
      name: 'LimitlessT1'
    }),
    computed: {
      ...mapState({
        symbols: state => state.watchListSymbols.symbols,
        patterns: state => state.tradingPatterns.patterns,
        tweets: state => state.twitterAnalysis.tweets,
      })
    },
    watch:{
      symbols(val){
        if(val && val.success){
          //this.items[0].content = val.data.length
        }
      },
      patterns(val){
        if(val && val.success){
          this.items[0].content = val.data.length
        }
      },
      tweets(val){
        if(val && val.success){
          this.items[1].content = val.data.length
        }
      }
    },
    created () {
      this.$store.dispatch('watchListSymbols/getWatchListSymbols')
      this.$store.dispatch('tradingPatterns/getTradingPatterns')
      this.$store.dispatch('twitterAnalysis/getUserTimeline', { name: this.name })
    }
  }
</script>


