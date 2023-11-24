<template>
  <div>
    <div class="top-bar">
      {{ chatItem?.name }}
    </div>
    <message class="message-box"></message>
    <send-message class="send-message-container"></send-message>
  </div>
</template>

<script lang='ts' setup>
import Message from '../components/message.vue'
import SendMessage from '../components/send-message.vue'
import { chatChangeState } from "../../hooks/chatchange.ts";
import { storeToRefs } from "pinia";

const store = chatChangeState();

const { chatItem } = storeToRefs(store);
</script>


<style lang='less' scoped>
*{
    margin:0px;
    padding:0px;
}
.top-bar {
  height: 30px;
  line-height: 30px;
  width: 100%;
}
.message-box{
    height: 620px;
    width: 100%;
    overflow-y: none;
    overflow-x: hidden;
}
.send-message-container{
    width:100%;
    height:140px;
}
</style>