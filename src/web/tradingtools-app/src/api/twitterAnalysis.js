import { get } from './api'

const endpoint = process.env.VUE_APP_ENDPOINT_TWITTERANALYSIS
export const twitterAnalysis = {
    getUserTimeline,
    getTweetPrediction
};

async function getUserTimeline(name) {
    return get(`${endpoint}/GetUserTimeline/${name}`)
}

async function getTweetPrediction(tweet) {
    return get(`${endpoint}/GetTweetPrediction/${encodeURIComponent(tweet)}`)
}