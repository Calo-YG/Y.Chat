<template>
  <div>
    <list :data="data">
      <template #default="{ item }">
        <user-group-item
          :id="item.id"
          :name="item.name"
          :description="item.description"
          :avatar="item.avatar"
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
