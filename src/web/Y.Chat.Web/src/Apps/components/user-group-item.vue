<template>
  <div class="item" @click="addchat()">
    <el-badge
        :is-dot="true"
        type="success"
        :hidden="!props.online"
      >
        <img class="face" :src="props.avatar" />
      </el-badge>
    <div class="des">
        <div class="nickName"><el-text truncated type="info">{{ props.name }}</el-text></div>
      <div class="signature"><el-text truncated type="info">{{ props.description }}</el-text></div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { defaultavatar } from "../../utils/static.ts"
import {chatChangeState} from '../../hooks/chatchange.ts' 

const store = chatChangeState()
const { change,addToList }=store
const props = defineProps({
  id: {
    type: String,
    required: true,
  },
  name: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    default: "这个人很懒,暂无任何介绍",
  },
  avatar: {
    type: String,
    default: defaultavatar,
  },
  online: {
    type: Boolean,
    default: false,
  },
  chatId: {
    type: String,
    required: true,
  },
})

const addchat = () => {
  const listitem ={
    id: props.chatId,
    name: props.name,
    avatar: props.avatar,
  }
  change(props.chatId)
  addToList(listitem)
}
</script>

<style scoped lang="less">
*{
  margin: 0;
  padding: 0;
}
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
  font-size: 12px;
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
  font-size: 12px;
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
.face {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  position: absolute;
  left: 10px;
  margin-top: 8px;
  object-fit: cover;
}
</style>
