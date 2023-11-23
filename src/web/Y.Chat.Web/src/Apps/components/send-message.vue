<template>
<div>
    <el-button @click="send()" primary>发送</el-button>
</div>
</template>

<script lang='ts' setup>
import chathub  from '../hubs/chathub.ts'
// import { ref ,computed,onMounted} from 'vue'
// import localCache from '/src/services/localStorage.ts'
import { storeToRefs } from 'pinia'
import {chatChangeState} from '../../hooks/chatchange.ts'

const store = chatChangeState()
const {chatId}=storeToRefs(store)

const send =()=>{
    if(!!chatId){
        console.info(chatId.value)
        chathub.send('I lOVE YOU',chatId.value,'Text')
    }
}

</script>

<style lang='less' scoped>

</style>