<template>
  <div class="item" @click="change(props.conversationId as string)">
    <el-badge>
        <img class="face" :src="avatar" />
      </el-badge>
    <div class="des">
        <div class="signature"><span>{{ props.name }} {{time}}</span></div>
        <div class="signature"><span>{{sendname}}:{{ content}}</span></div>
    </div>
</div>
</template>

<script lang="ts" setup>
import {onMounted,computed} from 'vue'
import { defaultavatar } from "../../utils/static.ts";
import {chatChangeState} from '../../hooks/chatchange.ts'
import config from "../../config.ts";
import localCache from '../../services/localStorage.ts' 
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
    },
    chatType:{
      type:Number,
      required:false
    },
    withDraw:{
      type:Boolean,
      required:false
    }
})

onMounted(()=>{
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

const content = computed(()=>{
  return props.withDraw?"撤回了一条消息":props.content
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
  height: 40px;
  box-sizing: border-box;
  border-bottom: 1px solid #eee;
  background-color: #efefef;
  display: flex;
  font-size: 18px;
}
.face {
  width: 30px;
  height: 30px;
  border-radius: 50%;
  position: absolute;
  left: 1px;
  margin-top: 5px;
  object-fit: cover;
}
.des {
  height: 60px;
  display: inline-block;
  margin-left: 35px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  flex: 1;
}
.nickName {
  /*height: 30px;*/
  font-size: 18px;
  margin-top: 10px;
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