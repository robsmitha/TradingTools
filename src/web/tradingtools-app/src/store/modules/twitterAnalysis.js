import { twitterAnalysis } from '../../api/twitterAnalysis'

const state = () => ({
    tweets: {
        loading: true,
        success: false,
        data: null
    }
})

const getters = {

}

const actions = {
    async getUserTimeline({ commit }, args) {
        const data = await twitterAnalysis.getUserTimeline(args.name)
        commit('setTweets', data)
    }
}

const mutations = {
    setTweets(state, data){
        state.tweets = {
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