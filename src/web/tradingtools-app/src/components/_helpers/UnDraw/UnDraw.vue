<template>
    <img :width="width" :src="src" />
</template>
<script>
import { errors } from './UnDraws'
export default {
    props: {
        icon: String
    },
    computed: {
        width(){
            return window.innerWidth < 768 ? "200px" : "300px"
        },
        src(){
            const baseUrl = 'https://smitha-cdn.s3.us-east-2.amazonaws.com/Content/vendor/unDraw/'
            switch(this.icon){
                case undefined:
                case null: 
                case '':
                return baseUrl + errors[Math.floor(Math.random() * errors.length)] + ".svg"
            }
            return baseUrl + this.icon + ".svg"
        }
    }
}
</script>