<template>
<a-list item-layout="horizontal" :data-source="data">
 <template #renderItem="{ item }">
  <chat-list-item :chat-id="item.id" :name="item.name" :avatar="item.avatar"></chat-list-item>
 </template>
</a-list>
</template>

<script lang='ts' setup>
import { ref,onMounted } from 'vue'
import List from 'ant-design-vue'
import mitt from '/src/utils/mitt.ts'
import ChatListItem from '/src/Apps/components/chat-list-item.vue'
import localCache from '/src/services/localStorage.ts'

const data = ref([])

onMounted(()=>{
    init()
})

const init=()=>{
    const value = localCache.getCache('chat-list')
    if(!!value){
       data.value=value
    }
    mitt.on('addchat',(item)=>{
        const has = data.value.some(p=>p.id ===item.chatId)
        if(!has){
            data.value.unshift(item)
            cachelist()
        }
    })
}

const cachelist = ()=>{
    localCache.setCache('chat-list',data.value)
}

</script>

<style lang='less' scoped>
</style>