<template>
    <div class="main-menu">
        <div class="logo">
            <img src="@/assets/img/logo.png" alt="logo加载失败" class="logo-img" />
        </div>
        <div class="search">

            <input type="text" placeholder="输入菜单名称进行搜索" class="search-input" />
            <el-button>
                <el-icon>
                    <Search />
                </el-icon>
            </el-button>
        </div>
        <div class="menu">
            <el-menu text-color="#b7bdc3" active-text-color="#fff" background-color="#001529">
                <!-- 系统管理 -->
                <el-sub-menu index="1">
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
                <el-sub-menu index="2">
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
                <el-sub-menu index="3">
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
                <el-sub-menu index="4">
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
                <el-sub-menu index="5">
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
                <el-sub-menu index="6">
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

.search {
    .search-input {
        width: 70%;
        height: 30px;
        border-radius: 5px;
    }

    .el-button {
        width: 20%;
        height: 30px;
    }


    height: 30px;
    background-color: #fff;
    display: flex;
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

    .el-menu-item.is-active {
        background-color: #0a60bd;
    }
}
</style>