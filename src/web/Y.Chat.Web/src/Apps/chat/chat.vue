<template>
    <div class="common-layout">
    <el-container>
      <el-aside width="240px">
        <info></info>
        <chat-tab></chat-tab>
      </el-aside>
      <el-container>
        <el-main></el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import info from './../components/info.vue'
import chatTab from './../components/chat-tab.vue'
import { reactive, ref ,onMounted,onBeforeUnmount} from "vue";
import { provide, inject } from 'vue'

const chathub = inject<ChatHub>("chatHub")

onMounted(()=>{
    chathub.start();
})
onBeforeUnmount(()=>{
    chathub.close()
})

</script>

<style lang="less" scoped>
.common-layout{
    padding-top:0px;
}
</style>