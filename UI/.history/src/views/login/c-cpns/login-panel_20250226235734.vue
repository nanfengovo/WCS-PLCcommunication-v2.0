<template>
    <div class="login-panel">
        <h1 class="title">后台管理系统</h1>
        <div class="tabs">tabs</div>
        <div class="controls">
            <el-checkbox v-model="isRemPassword" />
            <span>忘记密码</span>
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

}
</style>