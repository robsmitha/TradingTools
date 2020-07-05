<template>
    <div>
        <h1 class="font-weight-light mb-0">Watch List</h1>
        <small class="d-block pb-2 mb-3">These stocks are analyzed daily for possible trading patterns.</small>
        <v-list-item v-for="symbol in symbols" :key="symbol.symbol">
            <v-list-item-content>
                <v-list-item-title>
                    {{symbol.symbol}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>
    </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
    name: 'WatchList',
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


