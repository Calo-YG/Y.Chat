<template>
  <div class="send-container">
    <QuillEditor theme="snow" toolbar="#my-toolbar" :modules="modules" style="border: none;">
      <template #toolbar>
        <div class="menu" id="my-toolbar">
          <el-button type="info" class="menu-btn"
            ><img :src="Emoji" class="menu-icon"
          /></el-button>
          <el-upload
            :action="uploadChatFileApi"
            :before-upload="beforeUpload"
            :show-file-list="false"
            :on-success="uploadSuccess"
            :data="data"
            :headers="headers"
            :limit="3"
            ><el-button type="info" class="menu-btn">
              <Picture class="menu-icon" color="#2c2c34" /> </el-button
          ></el-upload>
        </div>
      </template>
    </QuillEditor>
    <div class="message-send">
      <el-button @click="send()" primary class="send-btn">发送</el-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import chathub from "../hubs/chathub.ts";
import { ref,computed } from "vue";
import { storeToRefs } from "pinia";
import { chatChangeState } from "../../hooks/chatchange.ts";
import { Picture } from "@element-plus/icons-vue";
import { Emoji } from "../../utils/static.ts";
import mitt from "../../utils/mitt.ts";
import { uploadState } from "../../hooks/upload-state.ts";
import '@vueup/vue-quill/dist/vue-quill.snow.css';

const message = ref("");
const type = ref("Text");

const { uploadChatFileApi, headers, beforeUpload } = uploadState();
const store = chatChangeState();
const { chatId } = storeToRefs(store);

const data = {
  chatId: chatId.value,
};

const modules=computed(()=>{
  return []
})

const send = () => {
    if (!!chatId && !!message.value) {
        chathub.send(message.value, chatId.value, type.value)
        document.querySelector('#messageinput')!.innerHTML = ''
        const selfdata = {
            message: message.value,
            type: type.value,
        }
        mitt.emit('ReciveSelfMessage', selfdata)
    }
}

const uploadSuccess=(res:any)=>{
    if(!!res){
       console.info(res)
    }
  }

</script>

<style lang="less" scoped>
* {
  margin: 0;
  padding: 0;
}
.send-container {
  border-top: #d5ebe1 1px solid;
}
.menu {
  margin-top: 5px;
  border: none;
}
.menu-btn {
  width: 20px;
  height: 20px;
  cursor: pointer;
  margin-right: 10px;
  float: left;
  border: none;
  border-radius: 5px;
  margin-left: 0px;
}
.menu-icon {
  width: 20px;
  height: 20px;
  background-color: #fff;
}
.message-send {
  width: 100%;
  margin-top: 5px;
}

.send-btn {
  width: 100px;
  height: 30px;
  background-color: #009688;
  color: #fff;
  font-size: 14px;
  float: right;
  margin-right: 10px;
  border-radius: 5px;
  cursor: pointer;

  &:hover {
    background-color: #00897b;
  }
}
</style>
