<template>
    <v-card
    color="blue"
    dark
    :to="includeNavigation ? `/tweet/${tweet.user.screen_name}/${tweet.id}` : ''"
    >
    <v-card-title>
        <span>{{tweet.created_at}}</span>
    </v-card-title>
    <v-card-text class="headline font-weight-bold">
        {{tweet.text}}
    </v-card-text>

    <v-card-actions>
        <v-list-item class="grow">
        <v-list-item-avatar color="grey darken-3">
            <v-img
            class="elevation-6"
            :src="tweet.user.profile_image_url"
            ></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
            <v-list-item-title>{{tweet.user.name}}</v-list-item-title>
        </v-list-item-content>

        <v-row
            align="center"
            justify="end"
        >
            <v-icon class="mr-1">mdi-heart</v-icon>
            <span class="subheading mr-2">{{tweet.retweet_count}}</span>
            <span class="mr-1">·</span>
            <v-icon class="mr-1">mdi-share-variant</v-icon>
            <span class="subheading">{{tweet.favorite_count}}</span>
        </v-row>
        </v-list-item>
    </v-card-actions>
    </v-card>
</template>

<script>

export default {
    props: {
        tweet: Object,
        includeNavigation: Boolean
    },
  }
</script>



