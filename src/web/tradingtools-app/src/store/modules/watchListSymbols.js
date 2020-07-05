import { watchListSymbolsSerivce } from './../../services/watchListSymbols.service'

const state = () => ({
    symbols: []
})

const getters = {

}

const actions = {
    getWatchListSymbols({ commit }) {
        watchListSymbolsSerivce.getWatchListSymbols()
        .then(symbols => {
            commit('setSymbols', symbols)
        })
    }
}

const mutations = {
    setSymbols(state, symbols){
        state.symbols = symbols
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}