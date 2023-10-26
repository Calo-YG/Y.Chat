<script setup lang="ts">
import { ElConfigProvider } from 'element-plus'
import login from './Apps/login/login.vue';
import { provide, inject } from 'vue'
import {ChatHub} from './Apps/hubs/chathub.ts'
import { TokenHub } from './Apps/hubs/tokenhub.ts'
import { reactive, ref ,onMounted,onBeforeUnmount} from "vue";

var tokenhub=new TokenHub()
var chatHub=new ChatHub()
provide("tokenhub",tokenhub)
provide("chatHub",chatHub)


onMounted(()=>{
  //tokenhub.start();
})

onBeforeUnmount(()=>{
  tokenhub.close();
})

</script>

<template>
  <ElConfigProvider>
    <div class="common-layout">
    <el-container>
      <router-view></router-view>
    </el-container>
  </div>
  </ElConfigProvider>
</template>


<style scoped lang="less"> 

</style>
