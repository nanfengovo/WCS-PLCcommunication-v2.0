<template>
    <div class="main-menu">
        <div class="logo">
            <img src="@/assets/img/logo.png" alt="logo加载失败" class="logo-img" />
        </div>
        <div class="search">

            <input type="text" placeholder="输入菜单名称进行搜索" class="search-input" />
            <el-button type="primary" class="search-button">
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
                                <Platform />
                            </el-icon>
                            <span>大屏数据展示</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Histogram />
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

                <!-- 库位管理 -->
                <el-sub-menu index="2">
                    <template #title>
                        <el-icon>
                            <MapLocation />
                        </el-icon>
                        <span>库位管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Location />
                            </el-icon>
                            <span>平面库位展示</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <AddLocation />
                            </el-icon>
                            <span>3D库位展示</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu>

                <!-- 任务管理 -->
                <el-sub-menu index="3">
                    <template #title>
                        <el-icon>
                            <Odometer />
                        </el-icon>
                        <span>任务管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Clock />
                            </el-icon>
                            <span>提升机任务管理</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <PieChart />
                            </el-icon>
                            <span>四向车任务管理</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu>

                <!-- 任务运维 -->
                <el-sub-menu index="4">
                    <template #title>
                        <el-icon>
                            <Setting />
                        </el-icon>
                        <span>系统运维管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Link />
                            </el-icon>
                            <span>Modbus数据点运维</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Cpu />
                            </el-icon>
                            <span>S7数据点运维</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu>

                <!-- 系统日志管理 -->
                <el-sub-menu index="5">
                    <template #title>
                        <el-icon>
                            <TrendCharts />
                        </el-icon>
                        <span>系统日志管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Document />
                            </el-icon>
                            <span>操作日志记录</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <ChromeFilled />
                            </el-icon>
                            <span>自动任务日志</span>
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
        width: 80%;
        height: 30px;
        border-radius: 5px;
        font-size: small;
    }

    .search-button {
        width: 20%;
        height: 30px;
    }


    height: 30px;
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