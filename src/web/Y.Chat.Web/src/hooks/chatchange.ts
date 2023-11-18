import {  ref} from "vue"
import { defineStore } from 'pinia'
import localCache from "./../services/localStorage.ts";

export const chatChangeState=defineStore('ChatIdState',()=>{
   const chatList=ref<Array<any>>([])
   const chatId=ref('')

   const change=(chatid:string)=>{
      chatId.value=chatid
   }

   const loadList=()=>{
      const list=localCache.getCache('chat-list')
      if(!!list){
         chatList.value=list
         chatId.value=list[0].id
      }
   }

   const addToList=(item:any)=>{
      if(!!chatList){
         var has = chatList.value.some(p=>p.id==item.id)
         if(!has){
            chatList.value.unshift(item)
            localCache.setCache('chat-list',chatList.value)
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



