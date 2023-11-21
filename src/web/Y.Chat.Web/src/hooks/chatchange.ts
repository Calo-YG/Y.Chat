import { ref } from "vue"
import { defineStore } from 'pinia'

export const chatChangeState=defineStore('ChatIdState',()=>{
   const chatList=ref<Array<any>>([])
   const chatId=ref('')
   const chatItem=ref({})

   const change=(chatid:string)=>{
      chatId.value=chatid
      chatItem.value = chatList.value.find(p=>p.id===chatId)
   }

   const loadList=(list:Array<any>)=>{
      if(!!list){
         chatList.value=list
         chatId.value=list[0].id
         chatItem.value = list[0]
      }
   }

   const addToList=(item:any)=>{
      if(!!chatList){
         var has = chatList.value.some(p=>p.id==item.id)
         if(!has){
            chatList.value.unshift(item)
         }
      }
   }

   return {
      chatId,
      change,
      loadList,
      chatList,
      addToList,
   }
})



