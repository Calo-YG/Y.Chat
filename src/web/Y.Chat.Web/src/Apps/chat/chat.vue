<template>
    <div class="common-layout">
    <el-container>
      <el-aside width="280px">
        <chat-tab></chat-tab>
      </el-aside>
      <el-container>
        <el-main>
         <chat-main></chat-main>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import chatTab from '../components/chat-tab.vue'
import { onMounted,onBeforeUnmount} from "vue"
import chathub from '../hubs/chathub.ts'
import ChatMain from '../components/chat-main.vue'



onMounted(()=>{
    chathub.start();
})
onBeforeUnmount(()=>{
    chathub.close()
})

</script>

<style lang="less" scoped>
.common-layout{
    background-color: #efefef;
    width:100%;
    border-radius:5px;
    margin:0px;
    padding:-6px;
    overflow-x: none;
    overflow-y: none;
}
</style>