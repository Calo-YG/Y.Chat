<template>
  <div class="center">
    <h1>Sign in/ Sign up</h1>
    <div class="logon">
      <div :class="overlaylong">
        <div class="overlaylong-Signin">
          <h2 class="overlaylongH2">Sign in</h2>
          <input type="text" placeholder="user" v-model="ruleForm.username" />
          <input type="text" placeholder="password" v-model="ruleForm.password" />
          <h3>Forgot your password?</h3>
          <button class="inupbutton" @click="submitForm">Sign in</button>
        </div>
      </div>
      <div :class="overlaytitle">
        <div class="overlaytitle-Signin">
          <h2 class="overlaytitleH2">Hello,Friend!</h2>
          <p class="overlaytitleP">
            Enter your personal details and start journey with us
          </p>
          <div class="buttongohs" @click="toRegister">Sign up</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import userService from "./../../services/userServices.ts";
import { useRouter } from "vue-router";
import localCache from "./../../services/localStorage.ts";
import { ElNotification } from 'element-plus'

const router = useRouter();
const overlaylong = ref('overlaylong')
const overlaytitle = ref('overlaytitle')

const ruleForm = ref({
  username: "",
  password: "",
});

const checkInput = () => {
  return ruleForm.value.username.length > 0 && ruleForm.value.password.length > 0;
}

const submitForm = () => {
  if (!checkInput()) {
    ElNotification({
      message: "Please enter the username and password",
      type: "error",
    })
    return;
  }
  userService
    .login(ruleForm.value.username, ruleForm.value.password)
    .then((res: any) => {
      localCache.setCache("user", res);
      router.push("/chat");
    });
};

const toRegister = () => {
  router.push("/register");
};

</script>

<style scoped lang="less">
* {
  padding: 0;
  margin: 0;
  ;
}

.center {
  width: 100%;
  height: 98vh;
  background-image: url("https://gd-hbimg.huaban.com/9165dc3c0f8279ae5402d1f47212847bff68b49f3cb547-QEli8R");
  background-size: 100% 100%;
  background-repeat: no-repeat;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  overflow: none;
  border-radius: 5px;
}

input {
  border: none;
  /* 设置边框为无 */
  outline: none;
  /* 移除输入框获得焦点时的轮廓线 */
}

h1 {
  font-size: 30px;
  color: black;
}

.logon {
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
  /* position: relative;
		overflow: hidden; */
  width: 768px;
  max-width: 100%;
  min-height: 480px;
  margin-top: 20px;
  display: flex;
  background: -webkit-linear-gradient(right, #4284db, #29eac4);
}

.overlaylong {
  border-radius: 10px 0 0 10px;
  width: 50%;
  height: 100%;
  background-color: #fff;
  display: flex;
  align-items: center;
  justify-content: center;
}

.overlaylongleft {
  border-radius: 0px 10px 10px 0px;
  width: 50%;
  height: 100%;
  background-color: #fff;
  transform: translateX(100%);
  transition: transform 0.6s ease-in-out;
  display: flex;
  align-items: center;
  justify-content: center;
}

.overlaylongright {
  border-radius: 10px 0 0 10px;
  width: 50%;
  height: 100%;
  background-color: #fff;
  transform: translateX(0%);
  transition: transform 0.6s ease-in-out;
  display: flex;
  align-items: center;
  justify-content: center;
}

.overlaytitle {
  border-radius: 0px 10px 10px 0px;
  width: 50%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0);
  display: flex;
  align-items: center;
  justify-content: center;
}

.overlaytitleH2 {
  font-size: 30px;
  color: #fff;
  margin-top: 20px;
}

.overlaytitleP {
  font-size: 15px;
  color: #fff;
  margin-top: 20px;
}

.overlaytitleleft {
  border-radius: 0px 10px 10px 0px;
  width: 50%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0);
  display: flex;
  align-items: center;
  justify-content: center;
  transform: translateX(0%);
  transition: transform 0.6s ease-in-out;
}

.overlaytitleright {
  border-radius: 0px 10px 10px 0px;
  width: 50%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0);
  display: flex;
  align-items: center;
  justify-content: center;
  transform: translateX(-100%);
  transition: transform 0.6s ease-in-out;
}

.overlaytitle-Signin {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

.overlaytitle-Signup {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

.buttongohs {
  width: 180px;
  height: 40px;
  border-radius: 50px;
  border: 1px solid #fff;
  color: #fff;
  font-size: 15px;
  text-align: center;
  line-height: 40px;
  margin-top: 40px;
}

.overlaylongH2 {
  font-size: 25px;
  color: black;
  /* width: 250px; */
}

.overlaylong-Signin {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

.overlaylong-Signup {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

input {
  background-color: #eee;
  border: none;
  padding: 12px 15px;
  margin: 10px 0;
  width: 240px;
}

h3 {
  font-size: 10px;
  margin-top: 10px;
  cursor: pointer;
}

.inupbutton {
  background-color: #29eac4;
  border: none;
  width: 180px;
  height: 40px;
  border-radius: 50px;
  font-size: 15px;
  color: #fff;
  text-align: center;
  line-height: 40px;
  margin-top: 30px;
}</style>
