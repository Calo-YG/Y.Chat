<template>
  <div class="chat-container">
    <div class="top-bar">
      {{ chatItem }}
    </div>
    <!-- 序列滚动 -->
    <div>
      <InfiniteList :data="messages" :width="'100%'" :height="620" :itemSize="getItemSize" v-slot="{ item ,index}"  debug="debug" >
        {{item}}
        <div class="message-item message-item-left" v-if="isCurrentuser">
          <div>
            <el-avatar :src="checkurl(item.avatar)" />
          </div>
          <div class="message-content">
            <div class="nickName" v-show="chatType === 1">
              <span>{{ item.name }}</span>
            </div>
            <div class="signature">
              <span>{{ item.content }}</span>
            </div>
          </div>
        </div>
        <div class="message-item message-item-right" v-else>
          <div>
            <el-avatar :src="checkurl(item.avatar)" />
          </div>
          <div class="message-content">
            <div class="nickName" v-show="chatType === 1">
              <span>Me</span>
            </div>
            <div class="signature">
              <span>{{ item.content }}</span>
            </div>
          </div>
        </div>
      </InfiniteList>
    </div>
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
import InfiniteList from "vue3-infinite-list";

const innerRef = ref<HTMLDivElement>();
const scrollbarRef = ref<InstanceType<typeof ElScrollbar>>();

const store = chatChangeState();

const { chatItem, chatId, chatType } = storeToRefs(store);
const messages = ref<Array<any>>([]);
const page = ref(0);
const pageSize = ref(50);
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

const getItemSize = (i: number): number => {
      switch (i % 4) {
        case 1:
          return 80;
        case 2:
          return 50;
        case 3:
          return 100;
        default:
          return 200;
      }
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
.chat-container {
  width: 100%;
  height: 80%;
}
.top-bar {
  height: 40px;
  line-height: 80px;
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

.message-item {
  width: 100%;
  height: 60px;
  box-sizing: border-box;
  border-bottom: 1px solid #eee;
  background-color: #efefef;
  display: flex;
}
.message-item-left {
  width: 100%;
  height: 60px;
  box-sizing: border-box;
  border-bottom: 1px solid #eee;
  background-color: #efefef;
  display: flex;
  float: left;
}
.message-item-right {
  width: 100%;
  height: 60px;
  box-sizing: border-box;
  border-bottom: 1px solid #eee;
  background-color: #efefef;
  display: flex;
  float: right;
}
</style>
