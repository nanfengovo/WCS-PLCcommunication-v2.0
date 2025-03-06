<template>
    <div class="login-panel">

        <h1 class="title">后台管理系统</h1>

        <el-tabs type="border-card">
            <el-tab-pane label="User">User</el-tab-pane>
            <el-tab-pane label="Config">Config</el-tab-pane>
            <el-tab-pane label="Role">Role</el-tab-pane>
            <el-tab-pane label="Task">Task</el-tab-pane>
        </el-tabs>

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
    margin-left: 5px;
}



.login-btn {
    margin-top: 10px;
    width: 100%;
}
</style>