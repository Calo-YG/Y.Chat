<template>
  <div v-if="!!props.data">
    <ul class="infinite-list" v-if="!!props.data && props.data.length > 0">
      <li v-for="item in props.data" 
        :key="item" class="infinite-list-item">
        <slot name="default" :item="item"></slot>
    </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>

const props = defineProps({
  data: Array<any>,
});

</script>

<style lang="less" scoped>
.infinite-list {
  height: 60px;
  padding: 0px;
  margin: 0px;
  list-style: none;
}
.infinite-list .infinite-list-item {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  margin: 0px;
  color: var(--el-color-primary);
}
.infinite-list .infinite-list-item + .list-item {
  margin-top: 10px;
}
</style>
