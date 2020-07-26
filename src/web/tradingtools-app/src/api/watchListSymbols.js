import { get } from './api'

const endpoint = process.env.VUE_APP_ENDPOINT_WATCHLISTSYMBOLS
const defaultUser = 'robsmitha94@gmail.com'
export const watchListSymbolsSerivce = {
    getWatchListSymbols
};

async function getWatchListSymbols(userId) {
    return get(`${endpoint}/GetWatchListSymbols/${userId ? userId : defaultUser}`)
}