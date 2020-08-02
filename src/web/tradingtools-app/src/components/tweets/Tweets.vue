<template>

      <div>
        <v-toolbar dense>
      <v-menu>
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            icon
            v-bind="attrs"
            v-on="on"
          >
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>

        <v-list>
          <v-list-item
            v-for="(item, i) in defaultUsers"
            :key="i"
            :to="`/tweets/${item}`"
          >
            <v-list-item-title>{{ item }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>

      <v-toolbar-title>{{name}}</v-toolbar-title>

      <v-spacer></v-spacer>
      <v-row
      v-if="searchOpen"
      >
        <v-text-field
        class="ml-5"
          @keydown.enter="searchTwitterUser"
          placeholder="Username"
          single-line
          hide-details
          v-model="searchUser"
        ></v-text-field>
      </v-row>
      <v-btn icon @click="searchOpen = !searchOpen"><v-icon>mdi-magnify</v-icon></v-btn>
    </v-toolbar>
        
    <v-container fluid>
        <v-skeleton-loader v-if="userTweets.loading"
          type="list-item-avatar-three-line"
        ></v-skeleton-loader>
        <ErrorMessage v-else-if="!userTweets.success" messsage="Could not load tweets." />
        <v-timeline dense v-else>
          <v-timeline-item
            v-for="t in userTweets.data" 
            :key="t.id"
            :fill-dot="true"
            :icon="'mdi-twitter'"
            color="blue"
            icon-color="white"
          >
            <TwitterCard :tweet="t" :includeNavigation="true" />
          </v-timeline-item>
        </v-timeline>
    </v-container>
      </div>
</template>

<script>
import { mapState } from 'vuex'
import ErrorMessage from "./../_helpers/ErrorMessage";
import TwitterCard from "./../_helpers/TwitterCard";

export default {
    components:{
      ErrorMessage,
      TwitterCard
    },
    data: () => ({
      name: null,
      searchUser: null,
      defaultUsers: ['LimitlessT1', 'BeastModeTrades', 'Ultra_Calls', 'PurePowerPicks', 'Duckingmoney'],
      searchOpen: false,
      userTweets: {
        loading: true,
        success: false,
        data: null
      }
    }),
    computed: {
      ...mapState({
        tweets: state => state.twitterAnalysis.tweets
      })
    },
    watch:{
      $route (to, from){
          if(to.fullPath !== from.fullPath){
              this.userTweets =  {
                loading: true,
                success: false,
                data: null
              }
              this.loadTweet(to.params.name)
          }
      },
      tweets(val){
        this.userTweets = val
      }
    },
    created () {
      this.loadTweet(this.$route.params.name)
    },
    methods:{
      searchTwitterUser(){
        this.$router.push({ path: `/tweets/${this.searchUser}` })
      },
      loadTweet(name){
        this.name = name ? name : this.defaultUsers[0]
        this.$store.dispatch('twitterAnalysis/getUserTimeline', { name: this.name })
      }
    }
  }
</script>



