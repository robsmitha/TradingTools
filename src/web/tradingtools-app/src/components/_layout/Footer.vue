<template>
  <v-footer
    v-if="isHomePage"
    dark
    padless
  >
    <v-card
      flat
      tile
      class="dark flex white--text text-center"
    >
      <v-card-text>
        <v-btn
          v-for="item in items" 
          :key="item.href"
          :href="item.href" 
          icon
          target="_blank" 
          class="mx-4 white--text"
          rel="noopener noreferrer">
          <v-icon size="24px">{{ item.icon }}</v-icon>
        </v-btn>
      </v-card-text>

      <v-card-text class=" pt-0">
         None of the information on this website is financial advice and should not be used for investment purposes. The website is not responsible for any losses that may or may not occur in the stock market.
      </v-card-text>

      <v-divider></v-divider>

      <v-card-text class="white--text">
        {{ new Date().getFullYear() }} —  Made with <v-icon color="red">mdi-heart</v-icon> by Rob Smitha
      </v-card-text>
    </v-card>
  </v-footer>
</template>

<script>
export default {
    data: () => ({
        items: []
    }),
    computed: {
      isHomePage() {
          return this.$route.path === '/'
      },
    }
}
</script>