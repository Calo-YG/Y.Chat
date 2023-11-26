<template>
    <div class="common-layout">
    <el-container>
      <el-aside width="280px">
        <chat-tab></chat-tab>
      </el-aside>
      <el-container>
        <el-main style="overflow-y: hidden;padding: 0px;">
          <suspense @resolve="resolve" >
            <chat-main></chat-main>
          </suspense>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import chatTab from '../components/chat-tab.vue'
import {ref, onMounted,onBeforeUnmount,defineAsyncComponent,Suspense} from "vue"
import chathub from '../hubs/chathub.ts'

const loading = ref(false)

const ChatMain = defineAsyncComponent({
  // 加载函数
  loader: () => import('../components/chat-main.vue'),
  // 展示加载组件前的延迟时间，默认为 200ms
  delay: 200,
  // 如果提供了一个 timeout 时间限制，并超时了
  // 也会显示这里配置的报错组件，默认值是：Infinity
  timeout: 3000
})

const resolve =()=>{
  loading.value=true
}


onMounted(()=>{
    chathub.start();
})
onBeforeUnmount(()=>{
    chathub.close()
})

</script>

<style lang="less" scoped>
.common-layout{
    background-color: #efefef;
    width:100%;
    border-radius:5px;
    margin:0px;
    padding:-6px;
    overflow-x: hidden;
    overflow-y: hidden;
}
</style>