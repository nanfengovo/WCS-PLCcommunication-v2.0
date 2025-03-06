<template>
    <div class="login-panel">
        <!--顶部标题-->
        <h1 class="title">后台管理系统</h1>
        <!--图标-->
        <img class="logo" src="@/assets/img/logo.png" alt="logo" />
        <!--选项卡-->
        <div class="tabs">
            <el-tabs type="border-card" stretch>
                <el-tab-pane label="账号登录" name="account">
                    <template #label>
                        <div class="label">
                            <el-icon>
                                <UserFilled />
                            </el-icon>
                            <span class="text">账号登录</span>
                        </div>
                    </template>
                </el-tab-pane>
                <el-tab-pane label="手机登录" name="phone">
                    <template #label>
                        <div class="label">
                            <el-icon>
                                <Cellphone />
                            </el-icon>
                            <span class="text">手机登录</span>
                        </div>
                    </template>
                </el-tab-pane>

            </el-tabs>
        </div>

        <!--底部区域-->
        <div class="controls">
            <el-checkbox v-model="isRemPassword" label="记住密码" size="large" />
            <el-link type="primary">忘记密码</el-link>
        </div>
        <el-button type="primary" class="login-btn" @click="handleLoginBtnClick">登录</el-button>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue'
const isRemPassword = ref(false)

function handleLoginBtnClick() {
    console.log('登录')
}

</script>

<style lang="less" scoped>
.login-panel {
    width: 380px;
    background-color: white;
    margin-bottom: 150px;
    height: 330px;
}

.logo {
    width: 80%;
    height: 15%;
    margin-left: 10%;
}

.title {
    text-align: center;
    margin-bottom: 15px;
}

.label {
    display: flex;
    align-items: center;
    justify-content: center;
}

.text {
    margin-left: 5px;
}

.controls {
    margin-top: 12px;
    display: flex;
    justify-content: space-between;
}

.controls .el-checkbox {
    margin-left: 25px;
}

.controls .el-link {
    margin-right: 25px;
}


.login-btn {
    margin-top: 10px;
    margin-left: 5%;
    width: 90%;
}
</style>