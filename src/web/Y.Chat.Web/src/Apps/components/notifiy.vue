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
          {{ item }}
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

const data = ref<Array<INoticeDto>>([]);
const load = ref(false);
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
      res.map((p) => {
        data.value.push(p);
      });
      load.value = true;
    });
};


</script>

<style lang="less" scoped>
* {
  margin: 0px;
  padding: 0px;
}

.arrow {
  margin-right: 10px;
  margin-top: auto;
  margin-bottom: auto;
  width: 10px;
  height: 10px;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;
}

.notice {
  width: 640;
  height: 300px;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.1);
}
span {
  font-size: 14px;
  text-align: left;
  width: 100%;
  height: 30px;
  line-height: 20px;
}

.notifiy-container{
    width: 400px;
    height: 450px;
}
</style>
