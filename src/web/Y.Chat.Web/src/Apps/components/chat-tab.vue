<template>
  <div class="container">
    <div class="avatar">
      <el-avatar shape="circle" :size="60" fit="fill" :src="avatar"/>
    </div>
    <el-tabs tab-position="left" class="tabs">
      <el-tab-pane>
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>联系人</span>
          </span>
        </template>
        <!-- <list url="v1/Users/Friends"></list> -->
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
      <el-tab-pane>
        <template #label>
          <span class="custom-tabs-label">
            <!-- <el-icon><calendar /></el-icon> -->
            <span>消息</span>
          </span>
        </template>
      </el-tab-pane>
    </el-tabs>
    <div>
</div>
</div>
</template>

<script lang="ts" setup>
import { computed, ref ,onMounted,onBeforeMount} from "vue";
import GroupList from "/src/Apps/components/group-list.vue"
import UserList from "/src/Apps/components/user-list.vue"
import ChatList from '/src/Apps/components/chat-list.vue'
import localCache from '/src/services/localStorage.ts'
import { defaultavatar } from '/src/utils/static.ts'
import config from '/src/config.ts'

const userid = ref('')
const avatar = ref('')

onMounted(()=>{
  const user= localCache.getCache('user')
  userid.value = user.userId;
  avatar.value = !!user.avatar ? config.getFile(user.avatar): defaultavatar
})

</script>

<style lang="less" scoped>

.avatar{
   height:20vh;
   margin-bottom:40px;
   margin-top:20px;
}

.container{
  height:100vh;
  width:75px;
}
.tabs > .el-tabs__content {
  padding: 32px;
  color: #6b778c;
  font-size: 32px;
  font-weight: 600;
}
</style>
