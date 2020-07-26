import { tradingPatternsService } from './../../api/tradingPatterns'

const state = () => ({
    patterns: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {

}

const actions = {
    async getTradingPatterns({ commit }) {
        const patterns = await tradingPatternsService.getTradingPatterns()
        commit('setPatterns', patterns)
    }
}

const mutations = {
    setPatterns(state, patterns){
        state.patterns = {
            loading: false,
            success: patterns !== null,
            data: patterns
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