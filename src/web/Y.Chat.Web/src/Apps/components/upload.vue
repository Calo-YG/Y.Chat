<template>
  <el-upload
    class="avatar-uploader"
    :action=uploadApi
    :show-file-list="true"
    :on-success="handleAvatarSuccess"
    :before-upload="beforeAvatarUpload"
    :data="data"
    v-model:file-list="fileList"
  >
  <img v-if="imageUrl" :src="imageUrl" class="avatar" />
  <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
  </el-upload>
</template>

<script lang="ts" setup>
import { ref,computed  } from "vue";
import { ElMessage } from "element-plus";
import { Plus } from "@element-plus/icons-vue";
import { useCookies } from "vue3-cookies";
import type { UploadFile } from "element-plus";
const { cookies } = useCookies();
const fileList: UploadUserFile[] = ref([]);

const data = {
  userId: cookies.get("authentication")["userId"],
};
const headers = {
  Authorization: cookies.get("authentication")["token"],
};

const uploadApi =ref('http://localhost:5088/api/v1/Files/UploadAvatar')

const handleAvatarSuccess: UploadProps["onSuccess"] = (
  response,
  uploadFile
) => {
  //imageUrl.value = URL.createObjectURL(uploadFile.raw!);
};

const imageUrl = computed(() => {
  if(fileList.length>0){
   return fileList[1].url
  }
  return ''
})

const beforeAvatarUpload: UploadProps["beforeUpload"] = (rawFile) => {
  if (rawFile.type !== "image/jpeg") {
    ElMessage.error("Avatar picture must be JPG format!");
    return false;
  } else if (rawFile.size / 1024 / 1024 > 2) {
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
