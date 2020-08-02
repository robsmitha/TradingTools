<template>
<div>
      <v-toolbar dense>
      <v-btn icon :to="`/tweets/${name}`">
           <v-icon>mdi-arrow-left</v-icon>
      </v-btn>
      <v-toolbar-title>
         {{name}}
        </v-toolbar-title>
    </v-toolbar>
    <v-container fluid>
      
        <v-skeleton-loader v-if="tweet.loading"
          type="list-item-avatar-three-line"
        ></v-skeleton-loader>
        <ErrorMessage v-else-if="!tweet.success" messsage="Could not load tweet." />
        
        <v-row v-else>
            <v-col md="6" sm="12" v-if="tweet.success">
                <TwitterCard :tweet="tweet.data" />
            </v-col>
            <v-col md="6" sm="12">
                 <v-card outlined v-if="topIntent">
                        <v-list-item two-line>
                            <v-list-item-content class="text-left">
                                <div class="overline mb-3">Intent Score</div>
                                <v-list-item-title class="headline mb-1">
                                    {{topIntent.intent}}
                                </v-list-item-title>
                                <v-list-item-subtitle>
                                    How confident AI is of it's assumption
                                </v-list-item-subtitle>
                            </v-list-item-content>
                            <v-list-item-avatar
                                tile
                                size="80"
                            >
                                <v-progress-circular size="80" :value="topIntent.percent">
                                    <span class="text-h6 font-weight-thin">
                                        {{topIntent.percent}}%
                                    </span>
                                </v-progress-circular>
                            </v-list-item-avatar>
                        </v-list-item>
                        <v-treeview 
                        :items="items" 
                        :open-on-click="true" 
                        :open-all="true"
                        ></v-treeview>
                    </v-card>
            </v-col>
        </v-row>
    </v-container>
    </div>
    
</template>

<script>
import { mapState } from 'vuex'
import ErrorMessage from "./../_helpers/ErrorMessage"
import TwitterCard from "./../_helpers/TwitterCard"

export default {
    components:{
      ErrorMessage,
      TwitterCard
    },
    data: () => ({
        predictions: [],
        intents: [],
        topIntent: null,
        items: [],
        searchOpen: false
    }),
    computed: {
        ...mapState({
            tweetPrediction: state => state.twitterAnalysis.tweetPrediction,
            tweets: state => state.twitterAnalysis.tweets
        }),
        tweet(){
            if(this.$route.params.id && this.tweets.success){
                const tweet = this.tweets.data.find(t => t.id === Number(this.$route.params.id))
                return {
                    success: tweet !== null,
                    data: tweet
                }
            }
            return {
                    loading: true
            }
        },
        tweetId (){
            return this.$route.params.id
        },
        name (){
            return this.$route.params.name
        }
    },
    watch: {
        tweet(val){
           if(val && val.success){
            this.$store.dispatch('twitterAnalysis/getTweetPrediction', { tweet: val.data.text })
          }
        },
        tweetPrediction(val){
            if(val.success){
                const { entities , intents, topIntent } = val.data.prediction
                this.topIntent = {
                    intent: topIntent,
                    score: intents[topIntent].score,
                    percent: Math.round(intents[topIntent].score * 100)
                }
                Object.keys(entities).map((e, e_index) => {
                    let item = {
                        id: e+"_"+e_index,
                        name: e,
                        children:[]
                    }
                    const subentities = entities[e]
                    subentities.map((se, se_index) => {
                        Object.keys(se).map(k => {
                            let child = {
                                id: e+"_"+k+"_"+se_index,
                                name: k,
                                children: []
                            }
                            se[k].map((v, v_index) => child.children.push({
                                id: e+"_"+k+"_"+v+"_"+v_index,
                                name: v,
                                to: '/'
                            }))
                            item.children.push(child)
                        })
                    })
                    this.items.push(item)
                })
            }
        }
    },
    created () {
        if(this.$route.params.name) {
            this.$store.dispatch('twitterAnalysis/getUserTimeline', { name: this.$route.params.name })
        }
    }
  }
</script>



