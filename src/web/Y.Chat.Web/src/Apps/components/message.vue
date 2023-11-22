<template>
  <div class="chat-container">
    <div class="top-bar">
      {{ chatItem }}
    </div>
    <!-- 序列滚动 -->
    <DynamicScroller :items="messages" :min-item-size="40" class="chat-content">
      <template v-slot="{ item, index, active }">
        <DynamicScrollerItem
          :item="item"
          :active="active"
          :size-dependencies="[item.message]"
          :data-index="index"
        >
          <div class="message-item-right" v-if="isCurrentuser(item.userId)">
            <div class="message-item">
              <p class="time">{{item.name}}  {{formatTime(item.created)}}</p>
              <div class="message-content"> 
                <span>{{ item.content }}</span>
              </div>
            </div>
            <img :src="checkurl(item.avatar)"/>
          </div>
          <div class="message-item-left" v-else>
            <img :src="checkurl(item.avatar)"/>
            <div class="message-item">
              <p class="time">{{item.name}}  {{formatTime(item.created)}}</p>
              <div class="message-content"> 
                <span>{{ item.content }}</span>
              </div>
            </div>
          </div>
        </DynamicScrollerItem>
      </template>
    </DynamicScroller>
  </div>
</template>

<script lang="ts" setup>
import { chatChangeState } from "/src/hooks/chatchange.ts";
import { storeToRefs } from "pinia";
import { computed, onMounted, ref, watch } from "vue";
import messageService from "/src/services/messageServices.ts";
import { ElScrollbar } from "element-plus";
import { chatHook } from "/src/hooks/chathooks.ts";
import localCache from "/src/services/localStorage.ts";
import * as dayjs from "dayjs";

const innerRef = ref<HTMLDivElement>();
const scrollbarRef = ref<InstanceType<typeof ElScrollbar>>();

const store = chatChangeState();

const { chatItem, chatId, chatType } = storeToRefs(store);
const messages = ref<Array<any>>([]);
const page = ref(0);
const pageSize = ref(15);
const scrollValue = ref(0);
const total = ref(0);

const { checkurl } = chatHook();

onMounted(() => {
  loadMessages();
});

watch(chatId, (newValue) => {
  if (!!newValue) {
    messages.value = [];
    loadMessages();
  }
});

const isCurrentuser = (id: String) => {
  return id === localCache.getCache("user").userId;
};

const formatTime = (time: string) => {
  return dayjs(time).format("hh:mm");
};

// 监听滚动条变化
watch(page, (newValue, oldValue) => {
  // 监听page的变化，当newValue存在或者newValue大于oldValue时执行loadMessages(func)
  if (!!newValue || newValue > oldValue) {
    loadMessages(func);
  }
});
//加载数据
const loadMessages = (callback: Function | undefined = undefined) => {
  console.info(chatId.value);
  // 加载消息
  messageService.query(chatId.value, page.value, pageSize.value).then((res) => {
    if (!!res) {
      total.value = res.total;
      if (!!callback) {
        // 如果存在回调函数，则调用回调函数并传入返回结果
        callback(res.result);
      } else {
        // 否则将返回结果插入到 messages 数组的开头
        func(res.result);
      }
    }
  });
};

const scroll = ({ scrollTop }) => {
  scrollValue.value = scrollTop;
};

const func = (res: Array<any>) => {
  res.map((p) => {
    messages.value.push(p);
  });
};
</script>

<style lang="less" scoped>
* {
  margin: 0;
  padding: 0;
}
.chat-container {
  width: 100%;
  height: 80%;
  overflow-y: none;
  overflow-x: hidden;
}
.top-bar {
  height: 40px;
  line-height: 80px;
}

.chat-content {
  width: 100%;
  padding: 20px;
  height:620px;
  overflow-y: none;
  overflow-x: hidden;
  .message-item-left {
    display: flex;
    margin-bottom: 20px;
    img {
      width: 40px;
      height: 40px;
      border-radius: 50%;
    }
    .message-item {
      margin-left: 10px;
      .time {
        font-size: 12px;
        color: rgba(51, 51, 51, 0.8);
        margin: 0;
        height: 20px;
        line-height: 20px;
        margin-top: -5px;
      }
      .message-content {
        padding: 10px;
        font-size: 14px;
        background:linear-gradient(135deg,#17ead9,#6078ea);
        position: relative;
        margin-top: 8px;
        border-radius:5px;
      }
      //小三角形
      // .message-content::before {
      //   position: absolute;
      //   left: -8px;
      //   top: 8px;
      //   content: "";
      //   border-right: 10px solid #fff;
      //   border-top: 8px solid transparent;
      //   border-bottom: 8px solid transparent;
      // }
    }
  }

  .message-item-right {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 20px;
    img {
      width: 40px;
      height: 40px;
      border-radius: 50%;
    }
    .message-item {
      width: 90%;
      margin-left: 10px;
      text-align: right;
      .time {
        font-size: 12px;
        color: rgba(51, 51, 51, 0.8);
        margin: 0;
        height: 20px;
        line-height: 20px;
        margin-top: -5px;
        margin-right: 10px;
      }
      .message-content {
        max-width: 70%;
        padding: 10px;
        font-size: 14px;
        float: right;
        margin-right: 10px;
        position: relative;
        margin-top: 8px;
        background:linear-gradient(135deg,#17ead9,#6078ea);
        text-align: left;
        border-radius:5px;
      }
      //小三角形
      // .message-content::after {
      //   position: absolute;
      //   right: -8px;
      //   top: 8px;
      //   content: "";
      //   border-left: 10px solid #a3c3f6;
      //   border-top: 8px solid transparent;
      //   border-bottom: 8px solid transparent;
      // }
    }
  }
}
</style>
