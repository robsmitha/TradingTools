<template>
  <div>
    <v-parallax src="https://smitha-cdn.s3.us-east-2.amazonaws.com/Content/images/md/bg-031.jpg">
        <v-container
          fluid
          >
          <v-row>
            <v-col class="text-center">
              <h1 class="subtitle-1 text-uppercase">
                Hello there
              </h1>
              <span class="text-lg-h1 text-h2">
                Welcome
              </span>
              <p class="subtitle-2">
                Glad you're here
              </p>
              <v-btn class="mx-2" dark large to="/dashboard">
                Dashboard
              </v-btn>
            </v-col>
          </v-row>
        </v-container>
      </v-parallax>
      <v-sheet color="grey lighten-3 py-7 text-center">
        <span class="text-overline d-block">
          Tools to help you trade smarter
        </span>
      </v-sheet>
      <v-container class="py-7 text-center">
        <h3 class="subtitle-1 text-uppercase">
          Features
        </h3>
        <span class="text-h4">
          The Tools
        </span>
        <p class="subtitle-2">
          Automated trading pattern analysis
        </p>
        <v-row>
          <v-col md="4" sm="6" xs="12"
              v-for="i in items"
              :key="i.title"
            >
            <v-card
              height="100%"
              outlined
              class="py-4"
              hover
              :to="i.to"
            >
            <div>
              <v-avatar
                size="80"
                :color="`${i.color}`"
              >
                <v-icon color="white">{{i.icon}}</v-icon>
              </v-avatar>
            </div>
             
              <v-list-item two-line>
                <v-list-item-content>
                  <v-list-item-title class="headline my-2">{{i.title}}</v-list-item-title>
                  <p class="text-body-1">{{i.text}}</p>
                </v-list-item-content>
              </v-list-item>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
  </div>
</template>

<script>
export default {
  data: () => ({
    items: [
      { color: 'deep-purple', icon: 'mdi-view-dashboard', title: 'Daily Dashboard', text: 'A daily dashboard for at a glance analysis of what is going on.', to: '/dashboard' },
      { color: 'green', icon: 'mdi-finance', title: 'Chart Patterns', text: 'These trading patterns are programatically analyzed and posted on an automatic schedule.', to: '/patterns' },
      { color: 'blue', icon: 'mdi-twitter', title: 'Stock Tweets', text: 'We follow day traders on twitter to help curate the market movers in real time.', to: '/tweets' }
    ]
  }),
}
</script>

