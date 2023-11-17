<template>
  <div>
    <list :data="data">
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
import mitt from "/src/utils/mitt.ts";
import ChatListItem from "/src/Apps/components/chat-list-item.vue";
import localCache from "/src/services/localStorage.ts";
import {chatChangeState} from '/src/hooks/chatchange.ts'

const store = chatChangeState()
const {change}=store

const data = ref([]);

onMounted(() => {
  const value = localCache.getCache("chat-list");
  data.value = value;
  if(!!!value && value.length>0){
    change(data.value[0])
  }
});
onBeforeMount(() => {
  mitt.on("addchat", (item) => {
    const has = data.value.some((p) => p.id === item.id);
    if (!has) {
      data.value.unshift(item);
      cachelist();
    }
  });
});

const cachelist = () => {
  localCache.setCache("chat-list", data.value);
};
</script>

<style lang="less" scoped></style>
