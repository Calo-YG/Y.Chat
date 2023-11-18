<template>
  <div>
    <list :data="chatList">
      <template #default="{ item }">
        <chat-list-item
          :id="item.id"
          :name="item.name"
          :avatar="item.avatar"
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

const store = chatChangeState();
const { loadList }=store
const { chatList } = storeToRefs(store);

onMounted(() => {
  loadList()
});


</script>

<style lang="less" scoped></style>
