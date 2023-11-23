<script setup lang="ts">
import { ElConfigProvider } from 'element-plus'






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
.common-layout{
  height: 100%;
  width: 100%;
  overflow-y: hidden;
}
</style>
