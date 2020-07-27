<template>
    <v-container fluid>
        <h1 class="font-weight-light mb-0">{{this.name}}</h1>
        <small class="d-block pb-2 mb-3">Tweets are programatically analyzed to find trade ideas, trends and more.</small>
        <v-skeleton-loader v-if="tweets.loading"
          type="list-item-avatar-three-line"
        ></v-skeleton-loader>
        <ErrorMessage v-else-if="!tweets.success" messsage="Could not load tweets." />
        <v-timeline dense v-else>
          <v-timeline-item
            v-for="t in tradeIdeaTweets" 
            :key="t.id"
            :fill-dot="true"
            :icon="'mdi-twitter'"
            color="blue"
            icon-color="white"
          >
            <v-card
                color="blue"
                dark
              >
                <v-card-title>
                  <span>{{t.created_at}}</span>
                </v-card-title>
                <v-card-text class="headline font-weight-bold">
                  "{{t.text}}"
                </v-card-text>

                <v-card-actions>
                  <v-list-item class="grow">
                    <v-list-item-avatar color="grey darken-3">
                      <v-img
                        class="elevation-6"
                        :src="t.user.profile_image_url"
                      ></v-img>
                    </v-list-item-avatar>

                    <v-list-item-content>
                      <v-list-item-title>{{t.user.name}}</v-list-item-title>
                    </v-list-item-content>

                    <v-row
                      align="center"
                      justify="end"
                    >
                      <v-icon class="mr-1">mdi-heart</v-icon>
                      <span class="subheading mr-2">{{t.retweet_count}}</span>
                      <span class="mr-1">·</span>
                      <v-icon class="mr-1">mdi-share-variant</v-icon>
                      <span class="subheading">{{t.favorite_count}}</span>
                    </v-row>
                  </v-list-item>
                </v-card-actions>
              </v-card>
          </v-timeline-item>
        </v-timeline>
    </v-container>
</template>

<script>
import { mapState } from 'vuex'
import ErrorMessage from "./../_helpers/ErrorMessage";

export default {
    components:{
      ErrorMessage
    },
    data: () => ({
      name: 'LimitlessT1'
    }),
    computed: {
      ...mapState({
        tweets: state => state.twitterAnalysis.tweets
      }),
      tradeIdeaTweets(){
        return this.tweets.success ? this.tweets.data.filter(t => t.text.toLowerCase().indexOf('trade idea') !== -1) : []
      }
    },
    created () {
      this.$store.dispatch('twitterAnalysis/getUserTimeline', { name: this.name })
    }
  }
</script>



