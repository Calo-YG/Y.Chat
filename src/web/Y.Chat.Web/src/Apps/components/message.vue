<template>
  <div class="chat-container">
    <!-- 虚拟滚动 -->
    <div class="message-container">
      <DynamicScroller ref="scroller" :items="messages" :min-item-size="60" class="message-scroll" key-field="id" type-field="messageType"
        :emit-update="true" @scroll-end="scrollToEnd" @update="updateMessageList" @scroll-start="scrollStart">
        <template #before v-if="load">
          加载中
        </template>
        <template #default="{ item, index, active }">
          <DynamicScrollerItem :item="item" :active="active" :size-dependencies="[item.content]" :data-index="index" :data-active="active">
            <div class="message-item-right" v-if="isCurrentuser(item.userId)">
              <div class="message-item">
                <p class="time">{{ item.name }} {{ formatTime(item.created) }}</p>
                <div class="message-content">
                  <span>{{ item.content }}</span>
                </div>
              </div>
              <img :src="checkurl(item.avatar)" />
            </div>
            <div class="message-item-left" v-else>
              <img :src="checkurl(item.avatar)" />
              <div class="message-item">
                <p class="time">{{ item.name }} {{ formatTime(item.created) }}</p>
                <div class="message-content">
                  <span>{{ item.content }}</span>
                </div>
              </div>
            </div>
          </DynamicScrollerItem>
        </template>
      </DynamicScroller>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { chatChangeState } from "../../hooks/chatchange.ts";
import { storeToRefs } from "pinia";
import { onMounted, ref, watch,computed } from "vue";
import messageService from "../../services/messageServices";
import { chatHook } from "../../hooks/chathooks.ts";
import localCache from "../../services/localStorage.ts";
import * as dayjs from "dayjs";
import mitt from "./../../utils/mitt.ts";
import groupServices from '../../services/groupServices.ts'
import {DynamicScroller} from 'vue-virtual-scroller'
import chathub from '../hubs/chathub.ts'
import { ElNotification } from 'element-plus'

const store = chatChangeState();

const { chatId,chatType } = storeToRefs(store);
const { composeMessage } = store
const messages = ref<Array<any>>([]);
const page = ref(0);
const pageSize = ref(15);
const total = ref(0);
const groupUsers = ref<Array<any>>([])
//const startIndex=ref(18)

const { checkurl } = chatHook();

const currentUser = localCache.getCache("user");
const scroller = ref<InstanceType<typeof DynamicScroller>|null>(null)
const load = ref(false);

const scrollObj=ref({
  start:0,
  end:0,
  visibleStart:0,
  visibleEnd:0,
})

onMounted(() => {
  messages.value = [];
  loadMessages();
  loadGroupUsers();
  reciveMessage();
  reciveSelfMessage();
  listenScroll()
});

watch(chatId, (newValue) => {
  if (!!newValue) {
    messages.value = [];
    loadMessages();
    loadGroupUsers();
  }
});
/**
 * 计算总页数
 */
const totalPage = computed(() => {
  return Math.ceil(total.value / pageSize.value);
});
/**
 * 判断是否是当前用户
 * @param id
 */
const isCurrentuser = (id: String) => {
  return id === localCache.getCache("user").userId;
};
/**
 * 格式化时间
 * @param time
 */
const formatTime = (time: string) => {
  return dayjs(time).format("hh:mm");
};

// 监听滚动条变化
// watch(page, (newValue, oldValue) => {
//   // 监听page的变化，当newValue存在或者newValue大于oldValue时执行loadMessages(func)
//   console.info("监听page的变化")
//   if (!!newValue && newValue > oldValue) {
//     loadMessages(func);
//   }
// });
//加载数据
const loadMessages = (callback: Function | undefined = undefined) => {
  if (!chatId.value) {
    return
  }
  // 加载消息
  messageService.query(chatId.value, page.value, pageSize.value).then((res) => {
    if (!!res) {
      total.value = res.totalCount;
      if (!!callback) {
        // 如果存在回调函数，则调用回调函数并传入返回结果
        callback(res.result);
        load.value = false;
        //scroller.value?.scrollToItem(startIndex.value)
      } else {
        // 否则将返回结果插入到 messages 数组的开头 
        res.result.map((p:any) => {
          messages.value.push(p);
        });     
        toEnd();
      }
    }
  });
};
/**
 * 加载群组成员
 */
const loadGroupUsers = () => {
  if (!chatId.value) {
    return
  }
  const type = chatType.value ===0 ? "Default":"Group"
  groupServices.groupUser(chatId.value,type).then(res => {
    if (!!res) {
      groupUsers.value = res
    }
  })
}
/**
 * 向数组的末尾添加数据
 * @param res 要添加的数据
 */
const func = (res: Array<any>) => {
  for(let i=res.length-1;i>=0;i--){
    messages.value.unshift(res[i])
  }
};

/**
 * 监听"ReciveMessage"事件，用于接收消息
 * @param data 接收到的消息数据
 */
const reciveMessage = () => {
  mitt.on("ReciveMessage", async (data: any) => {
    // 如果消息存在并且发送用户ID等于当前用户ID
    if (!!data.messageId && data.sendUserId === currentUser.userId) {
      // 遍历消息数组
      messages.value.forEach((item, index) => {
        // 如果消息发送者ID等于当前用户ID并且消息ID等于'selfdefaultmessageId'
        if (item.userId === currentUser.userId && item.id === 'selfdefaultmessageId') {
          // 更新消息ID为接收到的消息ID
          messages.value[index].id = data.messageId
        }
      })
      return
    }
    // 组装消息
    const message = composeMessage(groupUsers.value,
      data.sendUserId,
      data.msg,
      data.groupId,
      data.type,
      data.messageId)
    // 如果消息不存在，则返回
    if (!message) {
      return;
    }
    // 将消息添加到消息数组末尾
    messages.value.push(message)
  })
}

/**
 * 接收自己的消息
 */
const reciveSelfMessage = () => {
  mitt.on("ReciveSelfMessage", (data: any) => {
    // 创建一个包含当前消息的对象
    const selfLastmessages = {
      id: "selfdefaultmessageId",
      content: data.message,
      messageType: data.type,
      userId: currentUser.userId,
      name: currentUser.userName,
      created: Date.now(),
      chatId: chatId,
      avatar: currentUser.avatar,
    }
    // 将当前消息添加到消息列表中
    messages.value.push(selfLastmessages)
    toEnd()
  })
}

const scrollToEnd = () => {
  console.info("scrollToEnd")
}

const scrollStart=()=>{
  console.info("scroll-start")
}

const updateMessageList=(startIndex:number, endIndex:number, visibleStartIndex:number, visibleEndIndex:number)=>{
    scrollObj.value.start=startIndex
    scrollObj.value.end=endIndex
    scrollObj.value.visibleStart=visibleStartIndex
    scrollObj.value.visibleEnd=visibleEndIndex
}
/**
 * 滚动到底部
 */
const toEnd=()=>{
  if(!!scroller.value['scrollToBottom']){
    scroller.value['scrollToBottom']()
  }
}
/**
 * 监听滚动条
 */
const listenScroll=()=>{
  const element = document.querySelector(".message-scroll")
  if(!!element){
    element.addEventListener("scroll",()=>{
      const scrollTop = element.scrollTop
      const scrollerTop = scrollObj.value.start+scrollObj.value.end ===messages.value.length
      if(scrollTop===0 && page.value<totalPage.value &&!load.value&&scrollerTop){
        page.value++
        load.value=true
        loadMessages(func)
      }
    })
  }
}

const withDrawMessage=(chatId:string,messageId:string)=>{
  if(!chatId ||!messageId){
    return
  }
  try {
    chathub.withDrawMessage(messageId,chatId)
    const index = messages.value.findIndex(p=>p.id===messageId)
    if(index>-1){
      messages.value.splice(index,1)
    }
  } catch (e:any) {
    console.warn(e.message)
      ElNotification({
        title: "消息提示",
        message: e.message,
        type: "error",
        position: "bottom-right",
      });
  }
}

</script>

<style lang="less" scoped>
@import '../../style/messages.css'
</style>
