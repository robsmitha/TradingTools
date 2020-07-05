import { get } from './api.service'

const endpoint = process.env.VUE_APP_ENDPOINT_TRADINGPATTERNS

export const tradingPatternsService = {
    getTradingPatterns
};

function getTradingPatterns(date) {
    return get(`${endpoint}/GetTradingPatterns/${date ? date : new Date().toString()}`)
}