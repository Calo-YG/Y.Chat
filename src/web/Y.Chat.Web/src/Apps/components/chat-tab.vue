<template>
  <div>
    <el-tabs>
      <el-tab-pane>
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>好友</span>
          </span>
        </template>
        <!-- <list url="v1/Users/Friends"></list> -->
        <user-list url="v1/Users/Friends"></user-list>
      </el-tab-pane>
      <el-tab-pane >
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>群聊</span>
          </span>
        </template>
        <group-list  url="v1/Groups/UserGroup"></group-list>
      </el-tab-pane>
    </el-tabs>
    <div>
    <el-button @click="send()" primary>发送</el-button>
</div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref ,onMounted,onBeforeMount} from "vue";
import GroupList from "/src/Apps/components/group-list.vue"
import UserList from "/src/Apps/components/user-list.vue"

import chathub  from '/src/Apps/hubs/chathub.ts'

const send =()=>{
    chathub.send('你好')
}
</script>

<style lang="less" scoped></style>
