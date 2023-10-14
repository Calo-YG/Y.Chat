<template>
  <el-upload
    class="avatar-uploader"
    :action="uploadApi"
    :show-file-list="false"
    :on-success="uploadSuccess"
    :before-upload="beforeAvatarUpload"
    :data="data"
    v-model:file-list="fileList"
  >
    <img v-if="imageUrl" :src="imageUrl" class="avatar" />
    <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
  </el-upload>
</template>

<script lang="ts" setup>
import { ref, computed, watch,onMounted  } from "vue";
import { ElMessage } from "element-plus";
import { Plus } from "@element-plus/icons-vue";
import { useCookies } from "vue3-cookies";
import type { UploadFile } from "element-plus";
import config from '/src/config.ts'

const { cookies } = useCookies();
const fileList: UploadUserFile[] = ref([]);

let uploadApi = ref("");

let imageUrl = ref("");



onMounted(() => {
    uploadApi=config.API+"/api/v1/Files/UploadAvatar"
});

const data =computed(()=>{
  return {
    userId: getAutication("userId"),
  }
})
const headers =computed(()=>{
  return {
    Authorization: getAutication("token")
  }
})

const uploadSuccess=(response: any, uploadFile: UploadFile, uploadFiles: UploadFiles)=>{
    console.info(uploadFiles)
    if(!!response){
      imageUrl=config.API+"/api/v1/Files/File?filename="+response
      console.info(imageUrl)
    }
}

const getAutication = (param: string) => {
  const obj = cookies.get("authentication");
  if (!!obj && !!obj[param]) {
    return obj[param];
  }
  return null;
};

const beforeAvatarUpload: UploadProps["beforeUpload"] = (rawFile) => {
  if (rawFile.type !== "image/jpeg") {
    ElMessage.error("Avatar picture must be JPG format!");
    return false;
  } else if (rawFile.size / 1024 / 1024 > 10) {
    ElMessage.error("Avatar picture size can not exceed 2MB!");
    return false;
  }
  return true;
};
</script>

<style scoped lang="less">
.avatar-uploader .avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>

<style>
.avatar-uploader .el-upload {
  border: 1px dashed var(--el-border-color);
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
  border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
</style>
