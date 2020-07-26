<template>
    <div>
        <v-app-bar
        app
        dark
        clipped-right
        clipped-left
        >
            <v-app-bar-nav-icon @click.stop="drawerLeft = !drawerLeft"></v-app-bar-nav-icon>
            
        <v-toolbar-title
            class="ml-0 pl-md-4 pl-2"
        >
            <v-btn
                large
                text
                @click="onBrandClick"
            >
                Trading Tools
            </v-btn>
        </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click.stop="drawerRight = !drawerRight">
                <v-icon>mdi-apps</v-icon>
            </v-btn>
        </v-app-bar>

        <v-navigation-drawer
        v-model="drawerLeft"
        clipped
        app
        left
        >
        <v-list dense>
            <template v-for="item in items">
            <v-row
                v-if="item.heading"
                :key="item.heading"
                align="center"
            >
                <v-col cols="6">
                <v-subheader v-if="item.heading">
                    {{ item.heading }}
                </v-subheader>
                </v-col>
                <v-col
                cols="6"
                class="text-center"
                >
                <a
                    href="#!"
                    class="body-2 black--text"
                >EDIT</a>
                </v-col>
            </v-row>
            <v-list-group
                v-else-if="item.children"
                :key="item.text"
                v-model="item.model"
                :prepend-icon="item.model ? item.icon : item['icon-alt']"
                append-icon=""
            >
                <template v-slot:activator>
                <v-list-item-content>
                    <v-list-item-title>
                    {{ item.text }}
                    </v-list-item-title>
                </v-list-item-content>
                </template>
                <v-list-item
                v-for="(child, i) in item.children"
                :key="i"
                link
                :to="child.to"
                :href="child.href"
                :target="child.href ? '_blank' : ''" 
                :rel="child.href ? 'noopener noreferrer' : ''">
                
                <v-list-item-action v-if="child.icon">
                    <v-icon>{{ child.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>
                    {{ child.text }}
                    </v-list-item-title>
                </v-list-item-content>
                </v-list-item>
            </v-list-group>
            <v-list-item
                v-else
                :key="item.text"
                link
                :to="item.to"
            >
                <v-list-item-action>
                <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                <v-list-item-title>
                    {{ item.text }}
                </v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            </template>
        </v-list>
        </v-navigation-drawer>
        
        <v-navigation-drawer
            v-model="drawerRight"
            app
            clipped
            right
            >
            <v-list dense>
                <v-list-item>
                <v-list-item-content>
                    <v-list-item-title>Watch List</v-list-item-title>
                </v-list-item-content>
                </v-list-item>
                <div 
                v-if="symbols.loading"
                >
                    <v-skeleton-loader
                        v-for="i in 3"
                        :key="i"
                        type="list-item"
                        class="mx-auto"
                    ></v-skeleton-loader>
                </div>
                <v-list-item 
                v-else-if="!symbols.success"
                >
                    <v-list-item-icon>
                        <v-icon>mdi-alert-circle</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        Could not load items
                    </v-list-item-content>
                </v-list-item>
                <v-list-item
                v-else
                v-for="(child, i) in symbolsList"
                :key="i"
                link
                :to="child.to">
                
                <v-list-item-action v-if="child.icon">
                    <v-icon>{{ child.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>
                    {{ child.text }}
                    </v-list-item-title>
                </v-list-item-content>
                </v-list-item>
            </v-list>
        </v-navigation-drawer>
    </div>
</template>

<script>
import { mapState } from 'vuex'
  export default {
    data: () => ({
      right: false,
      left: false,
      items: [
        { icon: 'mdi-view-dashboard', text: 'Dashboard', to: '/dashboard' },
        { icon: 'mdi-finance', text: 'Patterns', to: '/patterns' },
        { icon: 'mdi-twitter', text: 'Tweets', to: '/tweets' }
      ],
      symbolsList: []
    }),
    computed: {
        ...mapState({
            symbols: state => state.watchListSymbols.symbols
        }),
        drawerRight: {
            get: function () {
                return this.right
            },
            set: function (val) {
                this.right = val
            }
        },
        drawerLeft: {
            get: function () {
                return this.left
            },
            set: function (val) {
                this.left = val
            }
        },
        isHomePage() {
            return this.$route.path === '/'
        }
    },
    watch:{
        symbols(val){
            if(val && val.data && Array.isArray(val.data)){
                this.symbolsList = []
                val.data.map((stock) => {
                    this.symbolsList.push({ text: stock.symbol, to: `/stock/${stock.symbol}` })
                })
            }
        },
        $route(val){
            if(this.$vuetify.breakpoint.lgAndUp){
                 this.drawerLeft = this.drawerRight = val.path !== '/'
            }
        }
    },
    created () {
      this.$store.dispatch('watchListSymbols/getWatchListSymbols')
    },
    methods: {
        onBrandClick(){
            if(this.$route.fullPath === '/'){
                this.$vuetify.goTo('body', { duration: 300, easing: 'easeInCubic' })
            }
            else{
                this.$router.push({ path: '/' })
            }
        }
    }
  }
</script>