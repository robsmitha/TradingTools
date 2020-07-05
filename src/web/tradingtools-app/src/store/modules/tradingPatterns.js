import { tradingPatternsService } from './../../services/tradingPatterns.services'

const state = () => ({
    patterns: null
})

const getters = {

}

const actions = {
    getTradingPatterns({ commit }) {
        tradingPatternsService.getTradingPatterns()
        .then(patterns => {
            commit('setPatterns', patterns)
        })
    }
}

const mutations = {
    setPatterns(state, patterns){
        state.patterns = patterns
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}