import { get } from './api'

const endpoint = process.env.VUE_APP_ENDPOINT_TRADINGPATTERNS

export const tradingPatternsService = {
    getTradingPatterns,
    getQuote
};

async function getTradingPatterns(date) {
    return get(`${endpoint}/GetTradingPatterns/${date ? date : new Date().toString()}`)
}

async function getQuote(symbol) {
    return get(`${endpoint}/GetQuote/${symbol}`)
}