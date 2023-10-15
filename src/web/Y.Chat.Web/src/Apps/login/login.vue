<template>
  <el-aside class="left-side"></el-aside>
  <el-main>
    <el-form
      ref="ruleFormRef"
      :model="ruleForm"
      status-icon
      label-width="120px"
      class="demo-ruleForm"
    >
      <el-form-item label="用户名" prop="username">
        <el-input v-model="ruleForm.username" />
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input
          v-model="ruleForm.password"
          type="password"
          autocomplete="off"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm(ruleFormRef)"
          >登录</el-button
        >
        <el-button type="primary" @click="toRegister()">注册</el-button>
      </el-form-item>
    </el-form>
  </el-main>
</template>

<script lang="ts" setup>
import { reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import userService from "../../services/userServices";
import { useCookies } from "vue3-cookies";
import { useRouter } from "vue-router";
import upload from "./../components/upload.vue";

const router = useRouter();
const { cookies } = useCookies();

const ruleFormRef = ref<FormInstance>();

const uploadApi = "http://localhost:5088/api/v1/Files/UploadAvatar";

const validatePass = (value: any, callback: any) => {
  console.info(callback);
  if (value === "") {
    callback(new Error("请输入密码"));
    return;
  }
  callback();
};
const validateUserName = (value: any, callback: any) => {
  if (value === "") {
    callback(new Error("请输入用户名"));
    return;
  }

  callback(true);
};

const ruleForm = ref({
  username: "",
  password: "",
});

const rules = reactive<FormRules<typeof ruleForm>>({
  username: [{ required: true, validator: validatePass, trigger: "blur" }],
  password: [{ required: true, validator: validateUserName, trigger: "blur" }],
});

const submitForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  formEl.validate((valid) => {
    console.info(valid);
    if (valid) {
      userService
        .login(ruleForm.value.username, ruleForm.value.password)
        .then((res) => {
          console.info(res);
          cookies.set("authentication", res);
          router.push('/chat')
        });
        
    }
  });
};

const toRegister = () => {
  router.push("/register");
};
</script>


<style scoped lang="less"> 
.left-side {
  width: 60%;
}
</style>