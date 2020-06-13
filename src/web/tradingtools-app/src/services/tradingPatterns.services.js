import { get } from './api.service'

const endpoint = process.env.VUE_APP_ENDPOINT_TRADINGPATTERNS

export const tradingPattternsService = {
    getTradingPattterns
};

function getTradingPattterns(date) {
    return get(`${endpoint}/GetTradingPattterns/${date ? date : new Date().toString()}`)
}