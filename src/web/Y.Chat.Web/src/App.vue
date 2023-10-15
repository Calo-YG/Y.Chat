<script setup lang="ts">
import { ElConfigProvider } from 'element-plus'
import login from './Apps/login/login.vue';
import { provide, inject } from 'vue'
import {ChatHub} from './Apps/hubs/chathub.ts'

provide("chatHub",()=>new ChatHub())


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
