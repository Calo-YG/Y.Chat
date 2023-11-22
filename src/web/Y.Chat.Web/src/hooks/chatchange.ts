import { ref } from "vue"
import { defineStore } from 'pinia'

// 定义一个名为 ChatIdState 的 store
export const chatChangeState = defineStore('ChatIdState', () => {
   // 创建一个 ref 数组，用于存储聊天记录
   const chatList = ref<Array<any>>([])
   // 创建一个 ref 字符串，用于存储聊天 id
   const chatId = ref('')
   // 创建一个 ref 对象，用于存储聊天信息
   const chatItem = ref<any>({})

   const chatType = ref(0)

   /**
    * @description: 根据传入的聊天 id，更新 chatId 和 chatItem
    * @param {string} chatid 聊天 id
    */
   const change = (chatid: string) => {
      chatId.value = chatid
      chatItem.value = chatList.value.find(p => p.id === chatId)
      chatType.value = chatItem.value?.chatType ?? 0
   }

   /**
    * @description: 根据传入的聊天记录，更新 chatList
    * @param {Array<any>} list 聊天记录
    */
   const loadList = (list: Array<any>) => {
      if (!!list) {
         chatList.value = list
         chatId.value = list[0].id
         chatItem.value = list[0]
         chatType.value = chatItem.value?.chatType ?? 0
      }
   }

   /**
    * @description: 根据传入的聊天记录，更新 chatList
    * @param {any} item 聊天记录
    */
   const addToList = (item: any) => {
      if (!!chatList) {
         var has = chatList.value.some(p => p.id == item.id)
         if (!has) {
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
      chatType
   }
})



