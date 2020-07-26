<template>
    <v-list>
        <v-list-item>
            <UnDraw />
        </v-list-item>
        <v-list-item>
            <v-list-item-icon>
                <v-icon>mdi-alert-circle</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                {{message}}
            </v-list-item-content>
        </v-list-item>
    </v-list>
</template>
<script>
import UnDraw from "./UnDraw/UnDraw";
export default {
    props: {
        message: String
    },
    components: {
        UnDraw
    }
}
</script>