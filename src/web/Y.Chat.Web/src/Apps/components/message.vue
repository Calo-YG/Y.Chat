<template>
<div class="chat-container">
 <div >

 </div>
</div>
</template>

<script lang='ts' setup>
import {chatChangeState} from '/src/hooks/chatchange.ts'
import { storeToRefs } from 'pinia'
import { computed,onMounted,ref,watch  } from 'vue'
import messageService from '/src/services/messageServices.ts'

const store = chatChangeState()

const {chatItem,chatId}=storeToRefs(store)
const messages = ref<Array<any>>([])
const page=ref(0)
const pageSize=ref(10)

onMounted(()=>{
  loadMessages()
})

watch(chatId,(newValue)=>{
  if(!!newValue){
    loadMessages()
  }
})

const loadMessages=()=>{
    messageService.query(chatId,page,pageSize).then(res=>{
      if(!!res){
         messages.value=res
      }
    })
}

</script>

<style lang='less' scoped>

.chat-container{
  width:100%;
  height:80%;
}
.top-bar{
  height:80px;
  line-height:80px;
}
</style>