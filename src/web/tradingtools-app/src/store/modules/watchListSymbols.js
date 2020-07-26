import { watchListSymbolsSerivce } from './../../api/watchListSymbols'

const state = () => ({
    symbols: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {

}

const actions = {
    async getWatchListSymbols({ commit }) {
        const symbols = await watchListSymbolsSerivce.getWatchListSymbols()
        commit('setSymbols', symbols)
    }
}

const mutations = {
    setSymbols(state, symbols){
        state.symbols = {
            loading: false,
            success: symbols !== null,
            data: symbols
        }
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}