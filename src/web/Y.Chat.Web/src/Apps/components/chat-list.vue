<template>
  <div>
    <list :data="chatList">
      <template #default="{ item }">
        <chat-list-item
          :id="item.id"
          :name="item.name"
          :avatar="item.avatar"
          :conversationId="item.conversationId"
          :content="item.content"
          :messageType="item.type"
          :lastSendUserName="item.lastSendUserName"
          :lastMessageTime="item.lastMessageTime"
          :lastSendUserId="item.lastSendUserId"
          :chatType="item.chatType"
        ></chat-list-item>
      </template>
    </list>
  </div>
</template>

<script lang="ts" setup>
import { onMounted } from "vue";
import List from "../components/list.vue";  
import ChatListItem from "../components/chat-list-item.vue";
import { chatChangeState } from "../../hooks/chatchange.ts";
import { storeToRefs } from 'pinia'
import chatlistService from '../../services/chatlistServices.ts'
import localCache from '../../services/localStorage.ts'

const store = chatChangeState();
const { loadList }=store
const { chatList } = storeToRefs(store);


onMounted(() => {
  const user = localCache.getCache('user')
  chatlistService.query(user.userId).then((res)=>{
    if(!!res){
      loadList(res)
    }
  })
});


</script>

<style lang="less" scoped></style>
