import { get } from './api.service'

const endpoint = process.env.VUE_APP_ENDPOINT_WATCHLISTSYMBOLS

export const watchListSymbolsSerivce = {
    getWatchListSymbols
};

function getWatchListSymbols(userId) {
    return get(`${endpoint}/GetWatchListSymbols/${userId ? userId : 'robsmitha94@gmail.com'}`)
}