
import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from './../components/Home';
import WatchList from './../components/WatchList';
import TradingPatterns from './../components/TradingPatterns';


Vue.use(VueRouter);

const routes = [
    { path: '/', component: Home },
    { path: '/watch-list', component: WatchList },
    { path: '/trading-patterns', component: TradingPatterns }
  ]
  
export default new VueRouter({
    routes
})