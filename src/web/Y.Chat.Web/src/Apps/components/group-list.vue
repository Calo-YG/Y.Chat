<template>
  <div>
    <list :data="data">
      <template #default="{ item }">
        <user-group-item
          :id="item.id"
          :name="item.name"
          :description="item.description"
          :avatar="item.avatar"
          :chat-id="item.id"
        ></user-group-item>
      </template>
    </list>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from "vue";
import Request from "../../services/htttpRequest.ts";
import localCache from "../../services/localStorage.ts";
import config from "../../config.ts";
import UserGroupItem from "../components/user-group-item.vue";
import list from "../components/list.vue";

const props = defineProps<{
  url: String;
}>();
const data=ref<Array<any>>([])

onMounted(() => {
  var user = localCache.getCache("user");
  const url = props.url + "?userId=" + user.userId;
  Request.get(url)
    .then((res:Array<any>) => {
      if (!!res) {
        let regex = new RegExp("^(http|https)://([\\w.]+/?)\\S*$");
        res.map((p) => {
          const avatar = p.avatar.match(regex) ? p.avatar : config.getFile(p.avatar);
          const value = {
            id: p.id,
            name: p.name,
            description: p.description,
            avatar: avatar,
            online:false
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
:deep(.my-label) {
  background: var(--el-color-success-light-9) !important;
}
:deep(.my-content) {
  background: var(--el-color-danger-light-9);
}
</style>
