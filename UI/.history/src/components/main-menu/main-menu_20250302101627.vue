<template>
    <div class="main-menu">
        <div class="logo">
            <img src="@/assets/img/logo.png" alt="logo加载失败" class="logo-img" />
        </div>
        <div class="menu">
            <el-menu>
                <!-- 系统管理 -->
                <el-sub-menu>
                    <template #title>
                        <el-icon>
                            <Monitor />
                        </el-icon>
                        <span>系统管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Grid />
                            </el-icon>
                            <span>系统监控面板</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Open />
                            </el-icon>
                            <span>自动任务管理</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu>

                <!-- 任务管理 -->
                <el-sub-menu>
                    <template #title>
                        <el-icon>
                            <Monitor />
                        </el-icon>
                        <span>系统管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Grid />
                            </el-icon>
                            <span>系统监控面板</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Open />
                            </el-icon>
                            <span>自动任务管理</span>
                        </template>
                    </el-menu-item>
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

.el-menu {
    border-right: none;
    user-select: none;
}

.el-sub-menu {
    .el-menu-item {
        padding-left: 50px !important;
        background-color: #0c2135;
    }

    .el-menu-item:hover {
        color: #fff;
    }
}
</style>