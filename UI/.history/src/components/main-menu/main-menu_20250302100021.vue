<template>
    <div class="main-menu">
        <div class="logo">
            <img src="@/assets/img/logo.png" alt="logo加载失败" class="logo-img" />
        </div>
        <div class="menu">
            <el-menu>
                <el-sub-menu>
                    <template #title>
                        <i class="el-icon-location"></i>
                        <span>导航一</span>
                    </template>
                </el-sub-menu>

            </el-menu>
        </div>
    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.main-menu {}

.logo {
    height: 60px;
    display: flex;
    justify-content: center;
    align-items: center;

    .logo-img {
        height: 40px;
    }
}
</style>