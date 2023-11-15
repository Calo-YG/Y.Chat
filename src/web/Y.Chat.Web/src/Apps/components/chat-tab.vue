<template>
  <div style="height:100vh;">
    <div class="box container">
      <div class="avatar">
        <el-badge is-dot type="success">
          <el-avatar shape="circle" :size="45" fit="fill" :src="avatar" />
        </el-badge>
      </div>
      <div>
        <ul>
          <li @click="changemenu('message')"><el-icon><ChatDotRound color="#6ca8af" size="60"/></el-icon></li>
          <li @click="changemenu('contact')"><el-icon><UserFilled color="#6ca8af" size="60"/></el-icon></li>
        </ul>
      </div>
    </div>
    <div class="box main-box">
      <chat-serach :key="key"></chat-serach>
      <div class='message-contact-box'>
        <chat-list v-if="showmessage"></chat-list>
        <user-group-tab v-if="showcontact"></user-group-tab>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, onMounted, onBeforeMount } from "vue"
import ChatList from "/src/Apps/components/chat-list.vue"
import localCache from "/src/services/localStorage.ts"
import { defaultavatar } from "/src/utils/static.ts"
import List from '/src/Apps/components/list.vue'
import config from "/src/config.ts"
import ChatSerach from '/src/Apps/components/chat-serach.vue'
import UserGroupTab from '/src/Apps/components/user-group-tab.vue'

const userid = ref("")
const avatar = ref("")
const menu = ref('contact')
const key = ref('')


onMounted(() => {
  const user = localCache.getCache("user");
  userid.value = user.userId;
  avatar.value = !!user.avatar ? config.getFile(user.avatar) : defaultavatar;
});

const showcontact=computed(()=>{
  return menu.value==='contact'
})
const showmessage =computed(()=>{
  return menu.value==='message'
})

const changemenu=(key:string)=>{
  menu.value=key;
}
</script>

<style lang="less" scoped>
.avatar {
  height: 20vh;
  margin-bottom: 40px;
  margin-top: 20px;
  padding-left:10px;
}

.container {
  height: 100vh;
  width: 60px;
  background-color: #eaeef1;
  margin:0px;
  padding:0px;
  border-radius:5px 0px 0px 5px;
}

ul {
  height:40px;
  list-style-type: none;
  margin: 0;
  padding: 0;
}

li {
  margin: 40px 0;
  padding: 10px;
  text-align: center;
}
.box{
  float:left;
}
.main-box{
  width:210px;
  height:100vh;
  padding-left:10px;
}

.message-contact-box{
  width:90%;
  height:80vh;
  margin:0 0;
}
</style>
