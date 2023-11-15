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
import info from '/src/Apps/components/info.vue'
import chatTab from '/src/Apps/components/chat-tab.vue'
import { reactive, ref ,onMounted,onBeforeUnmount} from "vue"
import { provide, inject } from 'vue'
import chathub from '/src/Apps/hubs/chathub.ts'
import ChatList from '/src/Apps/components/chat-list.vue'
import ChatMain from '/src/Apps/components/chat-main.vue'



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
}
</style>