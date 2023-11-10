<template>
  <el-descriptions :column="1">
    <el-descriptions-item
      label="Username"
      label-align="right"
      align="center"
      label-class-name="my-label"
      class-name="my-content"
      width="100%"
      >kooriookami</el-descriptions-item
    >
  </el-descriptions>

</template>

<script lang="ts" setup>
import { reactive, ref ,onMounted,onBeforeUnmount} from "vue";
import Request from "/src/services/htttpRequest.ts"
import localCache from "/src/services/localStorage.ts"

const props = defineProps<{
  url:String
}>();
const data=ref<Array>([])

onMounted(()=>{
    var user= localCache.getCache("user");
   const _url=props.url+"?userId="+user.userId;
   Request.get(_url).then((res)=>{
    console.info(res);
   }).catch((error)=>{
    console.error(error)
   })
})

</script>

<style lang="less" scoped>
:deep(.my-label) {
  background: var(--el-color-success-light-9) !important;
}
:deep(.my-content) {
  background: var(--el-color-danger-light-9);
}
</style>