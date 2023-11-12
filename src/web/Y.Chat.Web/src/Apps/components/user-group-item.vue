<template>
  <div class="item">
      <div>
        <el-avatar :src="props.avatar"></el-avatar>
      </div>
      <div>
        <el-text class="mx-1" truncated>{{props.name}}</el-text>
        <el-text class="mx-1" truncated>{{props.description}}</el-text>
      </div> 
  </div>
</template>

<script lang="ts" setup>
import { defaultavatar } from "/src/utils/static.ts";


const props= defineProps({
    id:{
        type:String,
        required:true
    },
    name:{
        type:String,
        required:true 
    },
    description:{
        type:String,
        default:"这个人很懒,暂无任何介绍"
    },
    avatar:{
        type:String,
        default:defaultavatar
    }
});

</script>

<style scoped lang="less">

.item{
    width:100%;
    height:40px;
    line-height:40px;
    font-size:18px
}

</style>