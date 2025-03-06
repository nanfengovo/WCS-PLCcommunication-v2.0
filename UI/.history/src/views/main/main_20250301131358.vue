<template>
    <div class="main">
        <h2>home</h2>
        <el-button type='success' @click="handleExitClick">退出登录</el-button>
    </div>
</template>
<script setup lang="ts">
import router from '@/router';


//退出登录
function handleExitClick() {
    //1.清除token
    localStorage.removeItem('token');
    //2.跳转到登录页
    router.push('/login');
}

</script>

<style lang="less" scoped>
.home {
    color: blue;
}
</style>