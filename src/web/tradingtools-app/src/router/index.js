
import Vue from 'vue';
import VueRouter from 'vue-router'
import Home from './../components/home/Home'
import Dashboard from './../components/dashboard/Dashboard'
import Stock from './../components/stock/Stock'
import Patterns from './../components/patterns/Patterns'
import Tweets from './../components/tweets/Tweets'
import Tweet from './../components/tweets/Tweet'
import goTo from 'vuetify/es5/services/goto'


Vue.use(VueRouter);

const routes = [
    { path: '/', component: Home },
    { path: '/dashboard', component: Dashboard },
    { path: '/stock/:symbol', component: Stock },
    { path: '/patterns', component: Patterns },
    { path: '/tweets/:name?', component: Tweets },
    { path: '/tweet/:name/:id', component: Tweet },
  ]
  
export default new VueRouter({
    routes,
    scrollBehavior: (to, from, savedPosition) => {
      let scrollTo = 0
  
      if (to.hash) {
        scrollTo = to.hash
      } else if (savedPosition) {
        scrollTo = savedPosition.y
      }
  
      return goTo(scrollTo)
    },
})