import { tradingPatternsService } from './../../api/tradingPatterns'

const state = () => ({
    patterns: {
        loading: true,
        success: false,
        data: null
    },
    quote: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {

}

const actions = {
    async getTradingPatterns({ commit }) {
        const data = await tradingPatternsService.getTradingPatterns()
        commit('setPatterns', data)
    },
    async getQuote({ commit }, args) {
        const data = await tradingPatternsService.getQuote(args.symbol)
        commit('setQuote', data)
    }
}

const mutations = {
    setPatterns(state, data){
        state.patterns = {
            loading: false,
            success: data !== null,
            data: data
        }
    },
    setQuote(state, data){
        state.quote = {
            loading: false,
            success: data !== null,
            data: data
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