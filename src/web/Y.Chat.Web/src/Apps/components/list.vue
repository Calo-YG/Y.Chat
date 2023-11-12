<template>
  <div v-if="!!props.data">
    <ul class="infinite-list" style="overflow: auto">
      <li v-for="item in props.data" :key="item" class="infinite-list-item">
        <slot name="default" :item="item"></slot>
    </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed } from "vue";

const props = defineProps({
  data: Array<any>,
});

</script>

<style lang="less" scoped>
.infinite-list {
  height: 100px;
  padding: 0;
  margin: 0;
  list-style: none;
}
.infinite-list .infinite-list-item {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  margin: 10px;
  color: var(--el-color-primary);
}
.infinite-list .infinite-list-item + .list-item {
  margin-top: 10px;
}
</style>
