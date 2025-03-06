<template>
    <div class="login-panel">
        <!--顶部标题-->
        <h1 class="title">后台管理系统</h1>
        <!--图标-->
        <img class="logo" src="@/assets/img/logo.png" alt="logo" />
        <!--选项卡-->
        <div class="tabs">
            <el-tabs type="border-card">
                <el-tab-pane label="登录">User</el-tab-pane>
                <el-tab-pane label="注册">Config</el-tab-pane>
                <el-tab-pane label="扫码登录">Role</el-tab-pane>
            </el-tabs>
        </div>

        <!--底部区域-->
        <div class="controls">
            <el-checkbox v-model="isRemPassword" label="记住密码" size="large" />
            <el-link type="primary">忘记密码</el-link>
        </div>
        <el-button type="primary" class="login-btn">Primary</el-button>
    </div>
</template>
<script setup lang="ts">
import { ref } from 'vue'
const isRemPassword = ref(false)

</script>

<style lang="less" scoped>
.login-panel {
    width: 330px;
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

.icon {
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