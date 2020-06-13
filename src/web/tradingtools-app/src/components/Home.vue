<template>
  <div>
    <h1 class="font-weight-light pb-2 mb-3">{{ msg }}</h1>
    <v-alert  
      border="top"
      colored-border
      type="info"
      dismissible
      close-text="Close Alert"
      elevation="2">
      None of the information on this website is financial advice and should not be used for invenstment purposes.<br>
      The website is not responsible for any losses that may or may not occur in the stock market.<br>
      These trading patterns are programatically analyzed and posted on an automatic schedule.
    </v-alert>
    <TradingPatterns />
  </div>
</template>

<script>
import TradingPatterns from './TradingPatterns';

export default {
  name: 'Home',
  props: {
    msg: String
  },
  components: {
    TradingPatterns,
  }
}
</script>

