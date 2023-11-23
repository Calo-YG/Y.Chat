<template>
   <el-popover :visible="visiable" placement="right" class="notice"  trigger="click">
    <template #reference>
        <span @click="visiable=true">{{props.title}}</span>
    </template>
    <list :data="data" v-loading="load">
       <template #default="{ item }">
          {{ item }}
       </template>
    </list>
   </el-popover>
</template>

<script lang="ts" setup>
import localCache from '../../services/localStorage.ts'
import { ref,onMounted } from "vue"
import noticeService from '../../services/noticeService.ts'
import list from '../components/list.vue'
// import NotifiyUser from '/src/Apps/components/notifiy-user.vue'
// import NotifiyGroup from '/src/Apps/components/notifiy-group.vue'

const props = defineProps({
    title:{
        type:String,
        required:true
    },
    type:{
        type:String,
        required:true
    }
})

const data = ref<Array<any>>([])
const load = ref(false)
const visiable = ref(false)

onMounted(()=>{
    const userId = localCache.getCache('user')['userId']
    noticeService.userNotices(userId,props.type)
    .then((res:Array<any>)=>{
        res.map(p=>{
            const value:any =p;
            data.value.push(value)
        })
        load.value=true
    })
})

</script>

<style lang="less" scoped>

.notice{

}
</style>