import config from '../config.ts'
import localStorage from "../services/localStorage.ts";
import { ElMessage } from "element-plus";
import type { UploadProps } from "element-plus";

interface IUploadState{
  uploadAvatarApi:String
  data:any
  headers:any,
  beforeUpload:UploadProps["beforeUpload"],
  uploadGroupFileApi:String,
  uploadChatFileApi:String,
}

const imageLimitation=["","",""]
const imageLimitSize=10
export function uploadState():IUploadState{
  const uploadAvatarApi = config.API + "/api/v1/Files/UploadAvatar";

  const uploadGroupFileApi = config.API + "/api/v1/Files/UploadGroupFile";

  const uploadChatFileApi = config.API + "/api/v1/Files/UploadChatFile";    

  const data = {
    userId: localStorage.getCache('user')['userId'],
  }

  const headers = {
    Authorization: localStorage.getCache('user')["token"]
  }

  const beforeUpload: UploadProps["beforeUpload"] = (rawFile) => {
    if (!imageLimitation.some(p=>rawFile.type===p)) {
      ElMessage.error("Avatar picture must be JPG format!");
      return false;
    } else if (rawFile.size / 1024 / 1024 > imageLimitSize) {
      ElMessage.error("头像文件大小限制为10M");
      return false;
    }
    return true;
  };

  return {
    uploadAvatarApi,
    data,
    headers,
    beforeUpload,
    uploadGroupFileApi,
    uploadChatFileApi,
  }
}