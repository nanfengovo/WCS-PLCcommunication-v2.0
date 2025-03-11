<template>
    <div class="main">
        <el-container class="main-container">
            <el-aside class="el-aside" :width="isFold ? '60px' : '210px'">
                <mainMenu :is-fold="isFold" />
            </el-aside>
            <el-container>
                <el-header class="el-header">
                    <mainHeader @fold-change="handleFoldChange" />
                </el-header>
                <el-main class="el-main">
                    <router-view></router-view>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>
<script setup lang="ts">
// import router from '@/router';
import mainMenu from '@/components/main-menu/main-menu.vue';
import mainHeader from '@/components/main-header/main-header.vue';
import { ref } from 'vue';



//处理main-header的折叠事件
const isFold = ref(false);
function handleFoldChange(flag: boolean) {
    isFold.value = flag;
}

</script>

<style lang="less" scoped>
.home {
    color: blue;
}

.main-container {
    height: 100vh;
    width: 100vw;

    .el-aside {
        background-color: #001529;
        overflow-x: hidden;
        overflow-y: auto;
        //折叠动画
        transition: width 0.5s ease;
    }

    .el-header {
        height: 50px;
        background-color: antiquewhite;
    }

    .el-main {
        background-color: white;
    }
}
</style>