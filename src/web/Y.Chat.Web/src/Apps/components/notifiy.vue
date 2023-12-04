<template>
  <el-popover
    placement="right"
    class="notice"
    trigger="click"
    width="640px"
    @hide="() => (visiable = false)"
  >
    <template #reference>
      <span @click="visiable = true" class="text-gray-600">{{
        props.title
      }}</span>
    </template>
    <div v-if="visiable" class="notifiy-container">
      <list :data="data">
        <template #default="{ item }">
          <div class="notice-item">
            <img class="face" :src="avatar(item)" />
            <div class="des">
              <div class="nickName">
                <el-text truncated type="info"
                  >{{ name(item) }} {{ mark(item) }}
                  {{ formatDate(item.creationTime) }}</el-text
                >
              </div>
              <div class="signature">
                <el-text truncated type="info"
                  >留言 : {{ item.remark }}</el-text
                >
              </div>
            </div>
            <div class="btn-container">
              <el-button
                class="agree-btn"
                :disabled="item.agree || isCurrentUser(item.requestUserId)"
                >{{ item.agree ? "已同意" : "同意" }}</el-button
              >
            </div>
          </div>
        </template>
      </list>
    </div>
  </el-popover>
</template>

<script lang="ts" setup>
import localCache from "../../services/localStorage.ts";
import { ref, watch } from "vue";
import noticeService from "../../services/noticeService.ts";
import type { INoticeDto } from "../../services/dtos.d.ts";
import list from "../components/list.vue";
import { chatChangeState } from "../../hooks/chatchange.ts";
import { chatHook } from "../../hooks/chathooks";
import * as dayjs from "dayjs";

const store = chatChangeState();
const { updateNociteCount, isCurrentUser } = store;
const { checkurl } = chatHook();

const props = defineProps({
  title: {
    type: String,
    required: true,
  },
  type: {
    type: String,
    required: true,
  },
});

//const first = ref(false)
const data = ref<Array<INoticeDto>>([]);
const load = ref(true);
const visiable = ref(false);
const userId = localCache.getCache("user")["userId"];

watch(visiable, (val) => {
  if (!!val) {
    loadNotifiy();
  }
});

const loadNotifiy = () => {
  noticeService
    .userNotices(userId, props.type)
    .then((res: Array<INoticeDto>) => {
      data.value = res;
      res.map((p) => {
        if (!p.agree && load.value && isCurrentUser(p.recivedId)) {
          updateNociteCount(props.type);
        }
      });
      load.value = false;
    });
};

const mark = (item: INoticeDto) => {
  if (isCurrentUser(item.recivedId)) {
    return "请求加为好友";
  }
  return item.agree ? "已同意" : "正在验证你的好友申请";
};

const name = (item: INoticeDto) => {
  return isCurrentUser(item.recivedId)
    ? item.requestUserName
    : item.reciveUserName;
};

const avatar = (item: INoticeDto) => {
  const ava = isCurrentUser(item.recivedId)
    ? item.sendAvatar
    : item.requestAvatar;
  return checkurl(ava);
};

const formatDate = (creationTime: string) => {
  return dayjs(creationTime).format("YYYY:MM:DD");
};

</script>

<style lang="less" scoped>
* {
  margin: 0px;
  padding: 0px;
}

.notice-item {
  width: 90%;
  height: 60px;
  margin: 0px auto;
  box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 6px -1px,
    rgba(0, 0, 0, 0.06) 0px 2px 4px -1px;
  border-radius: 10px;
}

.notice {
  width: 640;
  height: 300px;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.1);
}
span {
  font-size: 12px;
  text-align: left;
  width: 100%;
  height: 30px;
  line-height: 20px;
}

.notifiy-container {
  height: 450px;
}

.des {
  height: 60px;
  display: inline-block;
  margin-left: 85px;
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
.face {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  position: absolute;
  margin-left: 25px;
  margin-top: 15px;
  object-fit: cover;
}

.btn-container {
  width: 60px;
  height: 30px;
  float: right;
  padding-top: 15px;
  margin-right: 15px;
}

.agree-btn {
  width: 60px;
  height: 30px;
  line-height: 30px;
  text-align: center;
}
</style>
