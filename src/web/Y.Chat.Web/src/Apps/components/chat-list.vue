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
        ></chat-list-item>
      </template>
    </list>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onBeforeMount } from "vue";
import List from "/src/Apps/components/list.vue";
import ChatListItem from "/src/Apps/components/chat-list-item.vue";
import { chatChangeState } from "/src/hooks/chatchange.ts";
import { storeToRefs } from 'pinia'
import chatlistService from '/src/services/chatlistServices.ts'
import localCache from '/src/services/localStorage.ts'

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
