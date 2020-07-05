<template>
  <div>
    <h1 class="font-weight-light mb-0">Dashboard</h1>
    <small class="d-block pb-2 mb-3">Tools to help you trade smarter.</small>
    <v-alert  
      icon="mdi-sign-caution"
      prominent
      text
      dismissible
      close-text="Close Alert"
      elevation="2">
      None of the information on this website is financial advice and should not be used for invenstment purposes.
      The website is not responsible for any losses that may or may not occur in the stock market.
    </v-alert>
    
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

