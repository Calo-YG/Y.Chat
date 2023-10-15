<template>
  <div class="outbox">
    <div class="box">
      <el-avatar shape="circle" :size="80" fit="fill" :src="url" />
    </div>
    <div class="box">
      <div class="textbox">
        <el-text class="mx-1" type="info" :truncated="true">{{ username }}</el-text>
      </div>
      <div class="textbox">
        <el-text class="mx-1" type="info" :truncated="true">{{ email }}</el-text>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import authorization from "../../utils/authorization.ts";
import config from "../../config.ts";
import { ref, computed, watch, onMounted } from "vue";

let _url = "";
const url = computed({
  get() {
    _url = config.getFile(authorization.getAvatar());
    return _url;
  },
  set(value) {
    if (!!value) {
      _url = value;
    }
  },
});
const username = computed(() => {
  return authorization.getUserName();
});
const email = computed(() => {
  return authorization.getEmail();
});

const error = () => {
  _url = "";
};
</script>

<style lang="less" scoped>
.box {
  float: left;
  width: 120px;
}
.outbox {
  width: 240px;
  height: 120px;
}
.textbox {
  width: 100%;
  height: 40px;
  line-heignt: 40px;
}
</style>
