<template>
  <div class="item">
      <div class="item-float avatar-item">
        <el-badge :is-dot="true" type="success" :hidden="!props.online">
            <el-avatar :src="props.avatar"></el-avatar>
        </el-badge>
      </div>
      <div class="item-float description-item">
        <el-text  truncated>{{props.name}}</el-text>
        <el-text  truncated>{{props.description}}</el-text>
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
    },online:{
        type:Boolean,
        default:false
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

.item-float{
    float:left;
}
.avatar-item{
    width:40%;
}
.description-item{
    width:60%;
    height:40px;
}
</style>