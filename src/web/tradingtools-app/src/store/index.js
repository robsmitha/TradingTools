import Vue from 'vue'
import Vuex from 'vuex'
import tradingPatterns from './modules/tradingPatterns'
import watchListSymbols from './modules/watchListSymbols'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
    modules: {
        tradingPatterns,
        watchListSymbols
    },
    strict: debug
  })