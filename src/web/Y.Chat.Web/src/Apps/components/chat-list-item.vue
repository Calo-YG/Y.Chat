<template>
<div>
    <el-avatar :src="props.avatar" :size="50"></el-avatar>{{props.name}}
    
</div>
</template>

<script lang='ts' setup>
import {ref} from 'vue'
import { defaultavatar } from "/src/utils/static.ts";

const props = defineProps({
    chatId:{
        type:String,
        required:true,
    },
    name:{
        type:String,
        required:true
    },
    avatar:{
        default:defaultavatar
    }
})

</script>

<style lang='less' scoped>

</style>