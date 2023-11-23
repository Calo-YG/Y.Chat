<template>
    <div class="input-box">
      <el-input
        v-model:value="props.key"
        class="input-item"
        :suffix-icon="Search"
      />
      <div class="plus"><el-icon color="#6ca8af"><Plus /></el-icon></div>
    </div>
</template>

<script lang="ts" setup>
import { Search } from "@element-plus/icons-vue";

const props = defineProps({
  key: String,
});
</script>

<style lang="less" scoped>
.input-item {
  width: 150px;
}
.input-box {
  padding: 10px 5px;
  width: 210px;
  height: 40px;
  background-color:#f4f4f4;
}
.plus {
  width: 40px;
  height: 30px;
  background-color: #f2f2f2;
  border: 1px solid #ddd;
  text-align: center;
  line-height: 30px;
  float:right;
  margin-right:5px;
}
</style>
