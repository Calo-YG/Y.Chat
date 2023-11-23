<template>
  <el-upload class="avatar-uploader" :action="uploadApi" :show-file-list="false" :on-success="uploadSuccess"
    :before-upload="beforeAvatarUpload" :data="data" v-model:file-list="fileList" :headers="headers">
    <img v-if="imageUrl" :src="imageUrl" class="avatar" />
    <el-icon v-else class="avatar-uploader-icon">
      <Plus />
    </el-icon>
  </el-upload>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import { ElMessage } from "element-plus";
import { Plus } from "@element-plus/icons-vue";

import type { UploadProps, UploadUserFile } from "element-plus";
import config from '../../config.ts'
import localStorage from "../../services/localStorage";

const fileList = ref<Array<UploadUserFile>>([]);

let uploadApi = config.API + "/api/v1/Files/UploadAvatar";

const imageUrl = ref("");


const data = {
  userId: localStorage.getCache('user')['userId'],
}
const headers = {
  Authorization: localStorage.getCache('user')["token"]
}

const uploadSuccess = (response: any) => {
  if (!!response) {
    imageUrl.value = config.API + "/api/v1/Files/File?filename=" + response
    console.info(imageUrl)
  }
}

const beforeAvatarUpload: UploadProps["beforeUpload"] = (rawFile) => {
  if (rawFile.type !== "image/jpeg") {
    ElMessage.error("Avatar picture must be JPG format!");
    return false;
  } else if (rawFile.size / 1024 / 1024 > 10) {
    ElMessage.error("头像文件大小限制为10M");
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
