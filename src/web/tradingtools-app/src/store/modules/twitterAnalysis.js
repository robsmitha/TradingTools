import { twitterAnalysis } from '../../api/twitterAnalysis'

const state = () => ({
    tweets: {
        loading: true,
        success: false,
        data: null
    },
    tweetPrediction: {
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
    },
    async getTweetPrediction({ commit }, args) {
        const data = await twitterAnalysis.getTweetPrediction(args.tweet)
        commit('setTweetPrediction', data)
    }
}

const mutations = {
    setTweets(state, data){
        state.tweets = {
            loading: false,
            success: data !== null,
            data: data
        }
    },
    setTweetPrediction(state, data){
        state.tweetPrediction = {
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