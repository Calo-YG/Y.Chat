<template>
  <div>
    <el-tabs>
      <el-tab-pane type="card">
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>联系人</span>
          </span>
        </template>
        <user-list url="v1/Users/Friends"></user-list>
      </el-tab-pane>
      <el-tab-pane>
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>群聊</span>
          </span>
        </template>
        <group-list url="v1/Groups/UserGroup"></group-list>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script lang="ts" setup>
import GroupList from "../components/group-list.vue";
import UserList from "../components/user-list.vue";
</script>

<style lang="less" scoped>

.tab-box{
    width:210px;
    height:80px;
}

</style>
