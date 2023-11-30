<template>
  <div class="left-bar">
    <div class="container box">
      <div class="avatar">
        <el-badge is-dot type="success">
          <el-avatar shape="circle" :size="45" fit="fill" :src="avatar" />
        </el-badge>
      </div>
      <div>
        <ul>
          <li @click="changemenu('message')">
            <el-badge :value="noticeCount" v-if="noticeCount > 0"
              ><el-icon><ChatDotRound color="#6ca8af" size="80" /></el-icon
            ></el-badge>
            <el-icon v-else><ChatDotRound color="#6ca8af" size="80" /></el-icon>
          </li>
          <li @click="changemenu('contact')">
            <el-icon><UserFilled color="#6ca8af" size="80" /></el-icon>
          </li>
        </ul>
      </div>
    </div>
    <div class="box main-box">
      <chat-serach :key="key"></chat-serach>
      <div class="notifiy-container" v-if="showcontact">
        <notifiy title="群聊通知" type="GroupRequest" class="notifiy"></notifiy>
        <notifiy title="好友通知" type="FriendRequest" class="notifiy"></notifiy>
      </div>
      <div class="message-contact-box">
        <chat-list v-show="showmessage"></chat-list>
        <user-group-tab v-show="showcontact"></user-group-tab>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, onMounted } from "vue";
import ChatList from "../components/chat-list.vue";
import localCache from "../../services/localStorage.ts";
import config from "../../config.ts";
import ChatSerach from "../components/chat-serach.vue";
import UserGroupTab from "../components/user-group-tab.vue";
import { chatChangeState } from "../../hooks/chatchange.ts";
import { storeToRefs } from "pinia";
import Notifiy from '../components/notifiy.vue'

const userid = ref("");
const avatar = ref("");
const menu = ref("message");
const key = ref("");
const store = chatChangeState();
const { noticeCount } = storeToRefs(store);

onMounted(() => {
  let regex = new RegExp("^(http|https)://([\\w.]+/?)\\S*$");
  const user = localCache.getCache("user");
  userid.value = user.userId;
  avatar.value = user.avatar.match(regex)
    ? user.avatar
    : config.getFile(user.avatar);
});

const showcontact = computed(() => {
  return menu.value === "contact";
});
const showmessage = computed(() => {
  return menu.value === "message";
});

const changemenu = (key: string) => {
  menu.value = key;
};
</script>

<style lang="less" scoped>
.avatar {
  height: 20vh;
  margin-bottom: 40px;
  margin-top: 20px;
  padding-left: 10px;
}

.container {
  height: 98vh;
  width: 60px;
  background-color: #eaeef1;
  margin: 0px;
  padding: 0px;
  border-radius: 5px 0px 0px 5px;
  overflow-x: none;
  overflow-y: hidden;
}

ul {
  height: 40px;
  list-style-type: none;
  margin: 0;
  padding: 0;
}

li {
  margin: 40px 0;
  padding: 10px;
  text-align: center;
}
.box {
  float: left;
}
.main-box {
  width: 210px;
  height: 100vh;
  padding-left: 0px;
  overflow: none;
}

.search-box {
  width: 100%;
  height: 18vh;
}

.message-contact-box {
  width: 100%;
  height: 80vh;
  margin: 10 0 0 0;
}
.left-bar {
  height: 100vh;
  overflow-x: hidden;
}

.item {
  margin-top: 10px;
  margin-right: 40px;
}

.notifiy {
  width: 100%;
  height: 20px;
  margin-top: 10px;
  line-height: 40px;
  text-align: center;
  background-color: rgb(243, 244, 246);
  box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
  &::hover{
        background-color: #95b5c4;
    }
}
.notifiy-container {
  width: 100%;
  height: 40px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

</style>
