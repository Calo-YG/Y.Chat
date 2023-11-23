<template>
  <div>
    <message class="message-box"></message>
    <send-message class="send-message-container"></send-message>
  </div>
</template>

<script lang='ts' setup>
import Message from '../components/message.vue'
import SendMessage from '../components/send-message.vue'

</script>


<style lang='less' scoped>
*{
    margin:0px;
    padding:0px;
}
.message-box{
    height: 700px;
    width: 100%;
    overflow-y: none;
    overflow-x: hidden;
}
.send-message-container{
    width:100%;
    height:120px;
}
</style>