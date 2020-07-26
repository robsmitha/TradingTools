
<template>
    <v-container
    fluid>
        <h1 class="font-weight-light mb-0">Chart Patterns</h1>
        <small class="d-block pb-2 mb-3">These trading patterns are programatically analyzed and posted on an automatic schedule.</small>
        
        <v-skeleton-loader
            ref="skeleton"
            :boilerplate="boilerplate"
            :type="type"
            :tile="tile"
            class="mx-auto"
            v-if="patterns.loading"
            :transition="transition"
        >
        </v-skeleton-loader>
        <ErrorMessage 
        v-else-if="!patterns.success" 
        message="Could not load patterns" />
        <v-data-table
            v-else
            :headers="headers"
            :items="patterns.data"
            :items-per-page="15"
            class="elevation-1"
            :custom-sort="customSort"
            :expanded.sync="expanded"
            item-key="id"
            show-expand
        >
            <template v-slot:expanded-item="{ headers, item }">
                <td :colspan="headers.length">
                    <v-row>
                        <v-col
                            cols="12"
                            sm="4">
                            Open: {{ item.open }}   
                        </v-col>
                        <v-col
                            cols="12"
                            sm="4">
                            High: {{ item.high }}  
                        </v-col>
                        <v-col
                            cols="12"
                            sm="4">
                            Volume: {{ item.volume }}  
                        </v-col>
                        <v-col
                            cols="12"
                            sm="4">
                            Close: {{ item.close }}  
                        </v-col>
                        <v-col
                            cols="12"
                            sm="4">
                            Low: {{ item.low }}  
                        </v-col>
                    </v-row>
                </td>
            </template>
        </v-data-table>
    </v-container>
</template>

<script>
import ErrorMessage from "./../_helpers/ErrorMessage";
import { mapState } from 'vuex'
export default {
    components: {
        ErrorMessage
    },
    data: () => ({
        expanded: [],
        headers: [
            { text: 'Symbol', align: 'start', value: 'symbol', },
            { text: 'Pattern', value: 'pattern' },
            { text: 'Date', value: 'date' },
            { text: 'Change', value: 'change' },   
            { text: 'Change Percent', value: 'changePercent' },
            { text: '', value: 'data-table-expand' },     
        ],
        boilerplate: false,
        tile: false,
        type: 'table',
        types: [],
        transition: 'scale-transition'
    }),
    computed: {
      ...mapState({
        patterns: state => state.tradingPatterns.patterns
      })
    },
    created () {
      this.$store.dispatch('tradingPatterns/getTradingPatterns')
    },
    methods: {
        customSort: function(items, index, isDesc) {
            items.sort((a, b) => {
                if (index[0] =='date') {
                    if (!isDesc[0]) {
                        return new Date(b[index]) - new Date(a[index]);
                    } else {
                        return new Date(a[index]) - new Date(b[index]);
                    }
                }
                else {
                    if(typeof a[index] !== 'undefined'){
                         if (isNaN(a[index[0]])){
                            if (!isDesc[0]) {
                                return a[index].toLowerCase().localeCompare(b[index].toLowerCase());
                            }
                            else {
                                return b[index].toLowerCase().localeCompare(a[index].toLowerCase());
                            }
                         }
                         else{
                             if (!isDesc[0]) {
                                return (a[index[0]] < b[index[0]]) ? -1 : 1;
                            } else {
                                return (b[index[0]] < a[index[0]]) ? -1 : 1;
                            }
                         }
                    }
                }
            });
            return items;
        }
    }
}
</script>