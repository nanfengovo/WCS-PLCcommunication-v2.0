<template>
    <div class="login-panel">
        <h1 class="title">后台管理系统</h1>
        <div class="tabs">tabs</div>
        <div class="controls">
            <el-checkbox v-model="isRemPassword" label="记住密码" size="large" />
            <el-link type="primary">忘记密码</el-link>
        </div>
        <div class="login-btn">登录</div>
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

}
</style>