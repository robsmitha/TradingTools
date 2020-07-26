import { get } from './api'

const endpoint = process.env.VUE_APP_ENDPOINT_TRADINGPATTERNS

export const tradingPatternsService = {
    getTradingPatterns
};

async function getTradingPatterns(date) {
    return get(`${endpoint}/GetTradingPatterns/${date ? date : new Date().toString()}`)
}