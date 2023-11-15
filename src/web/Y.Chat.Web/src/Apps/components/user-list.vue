<template>
  <div>
    <list :data="data">
      <template #default="{ item }">
        <user-group-item
          :id="item.id"
          :name="item.name"
          :description="item.description"
          :avatar="item.avatar"
          :online="item.online"
          :chat-id="item.chatId"
        ></user-group-item>
      </template>
    </list>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted, onBeforeUnmount } from "vue";
import Request from "/src/services/htttpRequest.ts";
import localCache from "/src/services/localStorage.ts";
import config from "/src/config.ts";
import UserGroupItem from "/src/Apps/components/user-group-item.vue";
import list from "/src/Apps/components/list.vue";

const props = defineProps<{
  url: String;
}>();
const data=ref([])

onMounted(() => {
  var user = localCache.getCache("user");
  const url = props.url + "?userId=" + user.userId;
  Request.get(url)
    .then((res) => {
      if (!!res) {
        res.map((p) => {
          const avatar = p.avatar==="" ? undefined : config.getFile(p.avatar);
          const name = !!p.comment ? p.name+"("+p.comment+")":p.name
          const value = {
            id: p.id,
            name: name,
            description: p.sign,
            avatar: avatar,
            online:p.online,
            chatId:p.chatId
          };
          data.value.push(value);
        });
      }
    })
    .catch((error) => {
      console.error(error);
    });
});

</script>

<style lang="less" scoped>
</style>