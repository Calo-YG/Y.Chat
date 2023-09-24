<script setup lang="ts">
import { ElConfigProvider } from 'element-plus'

</script>

<template>
  <ElConfigProvider>
    <div class="common-layout">
    <el-container>
      <el-aside class="left-side"></el-aside>
      <el-main></el-main>
    </el-container>
  </div>
  </ElConfigProvider>
</template>


<style scoped lang="less"> 
.left-side {
  width: 60%;
}
</style>
