<template>
  <div class="outbox">
    <div class="box">
        <el-badge :is-dot="true" type="success" >
            <el-avatar shape="circle" :size="60" fit="fill" :src="url" @error="error"/>
        </el-badge>
    </div>
    <div class="box">
      <div class="textbox">
        <el-text class="mx-1" type="info" :truncated="true">{{ username }}</el-text>
      </div>
      <div class="textbox">
        <el-text class="mx-1" type="info" :truncated="true">{{ email }}</el-text>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import config from "../../config.ts";
import { ref, onMounted } from "vue";
import localCache from "../../services/localStorage.ts";

const url =ref("")
const username = ref("")
const email = ref("")

onMounted(()=>{
  const user= localCache.getCache('user')
  url.value=config.getFile(user.avatar)
  username.value=user.userName
  email.value=user.email
})

const error = () => {
  url.value = config.defaultAvatar;
};

</script>

<style lang="less" scoped>
.box {
  float: left;
  width: 80px;
}
.outbox {
  width: 240px;
  height: 120px;
  margin-top:5px;
}
.textbox {
  width: 100%;
  height: 40px;
  line-heignt: 40px;
}
</style>
