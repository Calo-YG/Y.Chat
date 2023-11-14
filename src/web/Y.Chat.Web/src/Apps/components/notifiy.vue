<template>
   <el-popover :visible="visible" placement="right" class="notice"  trigger="click">
    <template #reference>
        <span @click="visiable=true">{{props.title}}</span>
    </template>
    <list :data="data" v-loading="load">
       <template #default="{ item }">
          
       </template>
    </list>
   </el-popover>
</template>

<script lang="ts" setup>
import localCache from '/src/services/localStorage.ts'
import { ref,computed,onMounted } from "vue"
import noticeService from '/src/services/noticeService.ts'
import list from '/src/Apps/components/list.vue'
import NotifiyUser from '/src/Apps/components/notifiy-user.vue'
import NotifiyGroup from '/src/Apps/components/notifiy-group.vue'

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
const userid =computed(()=>{
    const user= localCache.getCache('user');
    return user.userId
})
const data = ref([])
const load = ref(false)
const visiable = ref(false)

onMounted(()=>{
    noticeService.userNotices(userid,props.type)
    .then((res)=>{
        res.map(p=>{
            const value ={

            }
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