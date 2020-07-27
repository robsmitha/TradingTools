
<template>
    <v-container
    fluid>
        <h1 class="font-weight-light mb-0">Chart Patterns</h1>
        <small class="d-block pb-2 mb-3">These trading patterns are programatically analyzed and posted on an automatic schedule.</small>
        
        <v-skeleton-loader
            ref="skeleton"
            type="table"
            class="mx-auto"
            v-if="patterns.loading"
            transition="scale-transition"
        >
        </v-skeleton-loader>
        <ErrorMessage 
        v-else-if="!patterns.success" 
        message="Could not load patterns" />
        <PatternsTable 
        v-else 
        :patterns="patterns.data" 
        />
    </v-container>
</template>

<script>
import ErrorMessage from "./../_helpers/ErrorMessage";
import PatternsTable from "./../_helpers/PatternsTable";
import { mapState } from 'vuex'
export default {
    components: {
        ErrorMessage,
        PatternsTable
    },
    computed: {
      ...mapState({
        patterns: state => state.tradingPatterns.patterns,
      })
    },
    created () {
      this.$store.dispatch('tradingPatterns/getTradingPatterns')
    }
}
</script>