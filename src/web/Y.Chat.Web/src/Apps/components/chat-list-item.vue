<template>
  <div class="item" @click="change(props.conversationId)">
    <el-badge
        @click.self="info()"
      >
        <img class="face" :src="avatar" />
      </el-badge>
    <div class="des">
        <div class="nickName"><el-text truncated type="info">{{ props.name }} {{time}}</el-text></div>
        <div class="signature"><el-text truncated type="info">{{sendname}}:{{props.content}}</el-text></div>
    </div>
</div>
</template>

<script lang="ts" setup>
import {ref,onMounted,computed} from 'vue'
import { defaultavatar } from "/src/utils/static.ts";
import {chatChangeState} from '/src/hooks/chatchange.ts'
import config from "/src/config.ts";
import localCache from '/src/services/localStorage.ts'
import * as dayjs from 'dayjs'

const store = chatChangeState()
const {change}=store
const userId = localCache.getCache('user').userId
const props = defineProps({
    id:{
        type:String,
        required:true,
    },
    name:{
        type:String,
        required:true
    },
    avatar:{
        default:defaultavatar
    },
    lastSendUserName:{
      type:String,
      required:false
    },
    content:{
      type:String,
      required:false
    },
    messageType:{
      type:Number,
      required:false
    },
    conversationId:{
      type:String,
      required:false
    },
    lastMessageTime:{
      type:String,
      required:false
    },
    lastSendUserId:{
      type:String,
      required:false
    }
})

const avatar = computed(()=>{
  let regex = new RegExp("^(http|https)://([\\w.]+/?)\\S*$")
  return props.avatar.match(regex) ? props.avatar :config.getFile(props.avatar)
})

const sendname = computed(()=>{
  return props.lastSendUserId===userId?"我":props.lastSendUserName
})

const time = computed(()=>{
  return dayjs(props.lastMessageTime).format('hh:mm')
})

onMounted(()=>{
})

</script>

<style lang="less" scoped>

.tip {
  height: 20px;
  line-height: 20px;
  font-size: 14px;
  color: gray;
  padding-left: 10px;
}
.none {
  display: none;
}
.item {
  width: 100%;
  height: 60px;
  box-sizing: border-box;
  border-bottom: 1px solid #eee;
  background-color: #efefef;
  display: flex;
}
.face {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  position: absolute;
  left: 10px;
  margin-top: 8px;
  object-fit: cover;
}
.des {
  height: 60px;
  display: inline-block;
  margin-left: 65px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  flex: 1;
}
.nickName {
  /*height: 30px;*/
  margin-top: 10px;
}
.nickName span {
  height: 25px;
  line-height: 25px;
  font-size: 16px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
.signature {
  /*height: 30px;*/
  margin-top: -5px;
}
.signature span {
  height: 20px;
  line-height: 20px;
  font-size: 12px;
  color: gray;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
}
.sel {
  width: 30px;
  height: 60px;
  display: inline-block;
  float: right;
  margin-right: 5px;
}
.sel img {
  width: 30px;
  height: 30px;
  margin-top: 15px;
}
</style>