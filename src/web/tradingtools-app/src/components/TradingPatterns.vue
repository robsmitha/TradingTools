
<template>
    <div>
    <v-skeleton-loader
        ref="skeleton"
        :boilerplate="boilerplate"
        :type="type"
        :tile="tile"
        class="mx-auto"
        :loading="loading"
        :transition="transition"
        >
            <v-data-table
                :headers="headers"
                :items="patterns"
                :items-per-page="15"
                class="elevation-1"
                :custom-sort="customSort"
                :single-expand="singleExpand"
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
        </v-skeleton-loader>
    </div>
</template>

<script>
import { tradingPattternsService } from "../services/tradingPatterns.services";
  export default {
    name: 'TradingPatterns',
    data: () => ({
        expanded: [],
        singleExpand: false,
        loading: true,
        success: false,
        headers: [
            { text: 'Symbol', align: 'start', value: 'symbol', },
            { text: 'Pattern', value: 'pattern' },
            { text: 'Date', value: 'date' },
            { text: 'Change', value: 'change' },   
            { text: 'Change Percent', value: 'changePercent' },
            { text: '', value: 'data-table-expand' },     
        ],
        patterns: [],
        boilerplate: false,
        tile: false,
        type: 'table',
        types: [],
        transition: 'scale-transition'
    }),
    async mounted() { 
        const data = await tradingPattternsService.getTradingPattterns()
        this.loading = false
        this.patterns = data ? data : []
        this.success = data !== null
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