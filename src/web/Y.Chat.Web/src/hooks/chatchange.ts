import {  ref} from "vue"
import { defineStore } from 'pinia'


export const chatChangeState=defineStore('ChatIdState',()=>{
   const chatId=ref('')

   const change=(chatid:string)=>{
      chatId.value=chatid
   }

   return {
      chatId,
      change
   }
})



