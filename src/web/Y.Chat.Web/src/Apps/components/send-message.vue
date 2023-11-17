<template>
<div>
    <el-button @click="send()" primary>发送</el-button>
</div>
</template>

<script lang='ts' setup>
import chathub  from '/src/Apps/hubs/chathub.ts'
import { ref ,computed} from 'vue'
import localCache from '/src/services/localStorage.ts'

const send =()=>{
    chathub.send('你好')
}

</script>

<style lang='less' scoped>
</style>