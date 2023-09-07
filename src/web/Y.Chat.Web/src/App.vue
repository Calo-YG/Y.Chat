<script setup lang="ts">
import { ElConfigProvider } from 'element-plus'

</script>

<template>
  <ElConfigProvider>
    <div class="common-layout">
    <el-container>
      <el-aside class="left-side">Aside</el-aside>
      <el-main>Main</el-main>
    </el-container>
  </div>
  </ElConfigProvider>
</template>

<style scoped lang="less"> 
.left-side {
  width: 60%;
}
</style>
