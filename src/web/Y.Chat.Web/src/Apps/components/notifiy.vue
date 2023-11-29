<template>
  <el-popover
    placement="right"
    class="notice"
    trigger="click"
    @hide="() => (visiable = false)"
  >
    <template #reference>
      <span @click="visiable = true">{{ props.title }}</span>
    </template>
    <template #default v-if="visiable">
        <RecycleScroller>
            <template #default="{ item }">
              {{ item }}
        </template>
        </RecycleScroller>
    </template>
  </el-popover>
</template>

<script lang="ts" setup>
import localCache from "../../services/localStorage.ts";
import { ref, watch } from "vue";
import noticeService from "../../services/noticeService.ts";
import type {INoticeDto} from '../../services/dtos.d.ts'

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
})

const loadNotifiy = () => {
  noticeService.userNotices(userId, props.type).then((res: Array<INoticeDto>) => {
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
.notice {
}
</style>
