<template>
    <div class="main">
        <!-- <el-button type='success' @click="handleExitClick">退出登录</el-button> -->
        <el-container class="main-container">
            <el-aside class="el-aside" width="200px">Aside</el-aside>
            <el-container>
                <el-header class="el-header">Header</el-header>
                <el-main class="el-main">Main</el-main>
            </el-container>
        </el-container>
    </div>
</template>
<script setup lang="ts">
// import router from '@/router';


//退出登录
// function handleExitClick() {
//     //1.清除token
//     localStorage.removeItem('token');
//     //2.跳转到登录页
//     router.push('/login');
// }

</script>

<style lang="less" scoped>
.home {
    color: blue;
}

.main-container {
    height: 100vh;
    width: 100vw;

    .el-aside {
        background-color: antiquewhite;
    }

    .el-header {
        background-color: aqua;
    }

    .el-main {
        background-color: bisque;
    }
}
</style>