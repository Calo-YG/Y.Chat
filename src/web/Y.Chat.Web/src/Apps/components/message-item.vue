<template>
<div>
    <div class="message-content" v-if="props.messageType==='Text'">
      <span>{{ checkContent() }}</span>
    </div>
</div>
</template>

<script lang="ts" setup>
import { chatHook } from '../../hooks/chathooks.ts'

const needCheckUrls=['video','image','emoji']
const hook = chatHook()

const props = defineProps({
    messageType:{
        type:String,
        default:'Text'
    },
    content:{
        type:String,
        required:true
    },
    isSelf:{
        type:Boolean,
        required:true,
        default:true
    }
})

const checkContent = ()=>{
    if(needCheckUrls.includes(props.messageType)){
        return hook.checkurl(props.content)
    }
    return props.content
}



</script>