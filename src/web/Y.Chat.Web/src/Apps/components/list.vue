<template>
  <el-descriptions :column="1">
    <el-descriptions-item>
      <template #default> </template>
    </el-descriptions-item>
  </el-descriptions>
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted, onBeforeUnmount } from "vue";
import Request from "/src/services/htttpRequest.ts";
import localCache from "/src/services/localStorage.ts";
import cofig from "/src/config.ts";

const props = defineProps<{
  url: String;
}>();
const data = ref<Array>([]);

onMounted(() => {
  var user = localCache.getCache("user");
  const url = props.url + "?userId=" + user.userId;
  Request.get(url)
    .then((res) => {
      if (!!res) {
        res.map((p) => {
          // const value: FriendGroupListDto = {
          //   id: p.id,
          //   name: p.name,
          //   avatar: cofig.getFile(p.avatar),
          //   description: "",
          //   comment: "",
          // };
          // data.push(value);
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
