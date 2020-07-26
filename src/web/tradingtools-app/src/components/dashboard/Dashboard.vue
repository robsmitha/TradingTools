<template>
    <v-container fluid>
        <v-row>
          <v-col md="4" sm="6" xs="12" v-for="i in items" :key="i.title">
            <v-card
              dark
              outlined
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
                    {{i.content}}
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
        { color: 'green', icon: 'mdi-thumb-up-outline', title: 'Buys', subtitle: 'Recommended long positions', content: 2 },
        { color: 'red', icon: 'mdi-thumb-down-outline', title: 'Sells', subtitle: 'Recommended short positions', content: 4 },
        { color: 'blue', icon: 'mdi-finance', title: 'Patterns', subtitle: 'Chart patterns we have observed', content: 6 }
      ]
    }),
    computed: {
      ...mapState({
        symbols: state => state.watchListSymbols.symbols
      })
    },
    created () {
      this.$store.dispatch('watchListSymbols/getWatchListSymbols')
    }
  }
</script>


