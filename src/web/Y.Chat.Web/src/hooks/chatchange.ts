import { ref } from "vue"
import { defineStore } from 'pinia'
import localStorage from "../services/localStorage"
import chatlistServices from "../services/chatlistServices"
import groupServices from "../services/groupServices"

// 定义一个名为 ChatIdState 的 store
export const chatChangeState = defineStore('ChatIdState', () => {
   // 创建一个 ref 数组，用于存储聊天记录
   const chatList = ref<Array<any>>([])
   // 创建一个 ref 字符串，用于存储聊天 id
   const chatId = ref('')
   // 创建一个 ref 对象，用于存储聊天信息
   const chatItem = ref<any>({})

   const chatType = ref(0)
   // 创建一个 ref 对象，用于存储当前用户 id
   const currentUserId = localStorage.getCache('user')['userId']

   const groupUsers = ref<Array<any>>([])

   const friendNotice= ref(0)

   const groupNotice= ref(0)

   const noticeCount = ref(0)

   /**
    * @description: 根据传入的聊天 id，更新 chatId 和 chatItem
    * @param {string} chatid 聊天 id
    */
   const change = (chatid: string) => {
      const index = chatList.value.findIndex(p => p.conversationId === chatId)
      if(index!=-1 && !!chatId){
         chatId.value = chatid
         chatItem.value = chatList.value[index]
         chatType.value = chatItem.value?.chatType ?? 0
      }
   }

   /**
    * @description: 根据传入的聊天记录，更新 chatList
    * @param {Array<any>} list 聊天记录
    */
   const loadList = (list: Array<any>) => {
      if (!!list&&list.length>0) {
         chatList.value = list
         chatId.value = list[0].conversationId
         chatItem.value = list[0]
         chatType.value = chatItem.value?.chatType ?? 0
      }
   }

   const loadgGroupUser=()=>{
      if (!chatId.value) {
         return
       }
       const type = chatType.value ===0 ? "Default":"Group"
       groupServices.groupUser(chatId.value,type).then(res => {
         if (!!res) {
           groupUsers.value = res
         }
      })
   }

   /**
    * @description: 根据传入的聊天记录，更新 chatList
    * @param {any} item 聊天记录
    */
   const addToList = (item: any) => {
      if (!!chatList) {
         var has = chatList.value.some(p => p.id === item.id)
         if (!has) {
            chatList.value.unshift(item)
         }
      }
   }
   /**
    * @description: 更新最后一条消息
    * @param {string} groupId 聊天记录
    * @param {string} messages 消息内容
    * @param {string} type 消息类型
    * @param {string} sendUserId 发送者id
    */
   const updateLastMessae=(groupId:string,messages:string,type:string,sendUserId:string)=>{
      const index = chatList.value.findIndex(p => p.conversationId === groupId) 
      if(index===-1){
         return;
      }
      chatList.value[index].content=messages
      chatList.value[index].type=type
      chatList.value[index].lastMessageTime=Date.now()
      chatList.value[index].lastMessageSendUserId=sendUserId
   }
   /**
    * 组装消息item
    * @param groupUsers 
    * @param sendUserId 
    * @param message 
    * @param groupId 
    * @param type 
    * @param messageId
    * @returns 
    */
   const composeMessage= (sendUserId:string,message:string,groupId:string,type:string,messageId:string)=>{
       const userIndex=groupUsers.value.findIndex(p=>p.id===sendUserId)
       if(userIndex===-1){
          return;
       }
       const user = groupUsers.value[userIndex]
       const index = chatList.value.findIndex(p=>p.conversationId===groupId)
       if(index ===-1)
       {
         chatlistServices.find(groupId,sendUserId).then(res=>{
            chatList.value.unshift(res)
         })
       }
       const messages={
            id:messageId,
            content:message,
            messageType:type,
            userId:sendUserId,
            name:name,
            created:Date.now(),
            chatId:groupId,
            avatar:user.avatar,
          }
      return messages
   }
   
   const updateChatListWithDraw=(groupId:string,messageId:string)=>{
      const index = chatList.value.findIndex(p => p.conversationId === groupId&&p.lastMessageId===messageId)
      if(index!=-1){
         chatList.value[index].withDraw=true
      }
   }

   const updateNociteCount=(type:String)=>{
      type==="FriendRequest"? friendNotice.value++:groupNotice.value++
      noticeCount.value = friendNotice.value+groupNotice.value
      console.info(noticeCount.value)
   }
   
   const isCurrentUser=(id:String)=>id===currentUserId

   return {
      chatId,
      change,
      loadList,
      chatList,
      addToList,
      chatType,
      chatItem,
      updateLastMessae,
      composeMessage,
      loadgGroupUser,
      updateChatListWithDraw,
      friendNotice,
      groupNotice,
      noticeCount,
      updateNociteCount,
      isCurrentUser
   }
})



