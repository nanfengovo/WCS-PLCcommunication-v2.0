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
            <el-menu text-color="#b7bdc3" active-text-color="#fff" background-color="#001529" default-active="1"
                :collapse="false">

                <!-- 动态菜单 -->
                <!--遍历整个菜单数组-->
                <template v-for="item in menuList" :key="item.id">
                    <el-sub-menu :index="item.id + ''">
                        <template #title>
                            <!--图标-->
                            <el-icon>
                                <component :is="item.icon.split('el-icon')[1]" />
                            </el-icon>
                            <span>
                                {{ item.name }}
                            </span>
                        </template>
                        <template v-for="subitem in item.children" :key="subitem.id">
                            <el-menu-item>
                                <!--图标-->
                                <el-icon>
                                    <component :is="subitem.icon.split('el-icon')[1]" />
                                </el-icon>
                                {{ subitem.name }}
                            </el-menu-item>
                        </template>
                    </el-sub-menu>

                </template>






                <!-- 静态菜单 -->
                <!-- 系统管理 -->
                <!-- <el-sub-menu index="1">
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
</el-sub-menu> -->

                <!-- 库位管理 -->
                <!-- <el-sub-menu index="2">
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
                </el-sub-menu> -->

                <!-- 任务管理 -->
                <!-- <el-sub-menu index="3">
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
                </el-sub-menu> -->

                <!-- 任务运维 -->
                <!-- <el-sub-menu index="4">
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
                </el-sub-menu> -->

                <!-- 系统日志管理 -->
                <!-- <el-sub-menu index="5">
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
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Aim />
                            </el-icon>
                            <span>数据点读写日志</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu> -->

                <!-- 权限管理 -->
                <!-- <el-sub-menu index="6">
                    <template #title>
                        <el-icon>
                            <More />
                        </el-icon>
                        <span>权限管理</span>
                    </template>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <UserFilled />
                            </el-icon>
                            <span>用户管理</span>
                        </template>
                    </el-menu-item>
                    <el-menu-item>
                        <template #title>
                            <el-icon>
                                <Avatar />
                            </el-icon>
                            <span>角色管理</span>
                        </template>
                    </el-menu-item>
                </el-sub-menu> -->
            </el-menu>
        </div>
    </div>
</template>
<script setup lang="ts">




//模拟后端返回的菜单数组
const menuList = [
    {
        'id': 1,
        'name': '系统管理',
        'type': 1,
        'url': '/main/system',
        'icon': 'el-icon-monitor',
        'sort': 1,
        'children': [
            {
                'id': 2,
                'url': 'main/analysis/screen',
                'name': '大屏数据展示',
                'icon': 'el-icon-Platform',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 1
            },
            {
                'id': 3,
                'url': 'main/analysis/dashboard',
                'name': '系统监控面板',
                'icon': 'el-icon-Histogram',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 1
            },
            {
                'id': 4,
                'url': 'main/analysis/autoTask',
                'name': '自动任务管理',
                'icon': 'el-icon-Open',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 1
            }

        ]
    },
    {
        'id': 5,
        'name': '库位管理',
        'type': 1,
        'url': '/main/mapLocation',
        'icon': 'el-icon-MapLocation',
        'sort': 1,
        'children': [
            {
                'id': 6,
                'url': 'main/mapLocation/location',
                'name': '平面库位展示',
                'icon': 'el-icon-Location',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 5
            },
            {
                'id': 7,
                'url': 'main/mapLocation/addLocation',
                'name': '3D库位展示',
                'icon': 'el-icon-AddLocation',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 5
            }
        ]
    },
    {
        'id': 8,
        'name': '任务管理',
        'type': 1,
        'url': '/main/task',
        'icon': 'el-icon-Odometer',
        'sort': 1,
        'children': [
            {
                'id': 9,
                'url': 'main/task/lift',
                'name': '提升机任务管理',
                'icon': 'el-icon-Clock',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 8
            },
            {
                'id': 10,
                'url': 'main/task/rgv',
                'name': '四向车任务管理',
                'icon': 'el-icon-PieChart',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 8
            }
        ]
    },
    {
        'id': 11,
        'name': '系统运维管理',
        'type': 1,
        'url': '/main/om',
        'icon': 'el-icon-Setting',
        'sort': 1,
        'children': [
            {
                'id': 12,
                'url': 'main/om/modbus',
                'name': 'Modbus数据点运维',
                'icon': 'el-icon-Link',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 11
            },
            {
                'id': 13,
                'url': 'main/om/S7',
                'name': 'S7数据点运维',
                'icon': 'el-icon-Cpu',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 11
            }
        ]
    },
    {
        'id': 14,
        'name': '系统日志管理',
        'type': 1,
        'url': '/main/log',
        'icon': 'el-icon-TrendCharts',
        'sort': 1,
        'children': [
            {
                'id': 15,
                'url': 'main/log/actionLog',
                'name': '操作日志',
                'icon': 'el-icon-Document',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 14
            },
            {
                'id': 16,
                'url': 'main/log/autoTaskLog',
                'name': '自动任务日志',
                'icon': 'el-icon-ChromeFilled',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 14
            },
            {
                'id': 17,
                'url': 'main/log/dbPointLog',
                'name': '数据点读写日志',
                'icon': 'el-icon-Aim',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 14
            }
        ]
    },
    {
        'id': 18,
        'name': '权限管理',
        'type': 1,
        'url': '/main/Permissions',
        'icon': 'el-icon-Setting',
        'sort': 1,
        'children': [
            {
                'id': 19,
                'url': 'main/Permissions/user',
                'name': '用户',
                'icon': 'el-icon-UserFilled',
                'sort': 1,
                'type': 2,
                'children': null,
                'parentId': 18
            },
            {
                'id': 20,
                'url': 'main/Permissions/role',
                'name': '角色',
                'icon': 'el-icon-Avatar',
                'sort': 2,
                'type': 2,
                'children': null,
                'parentId': 18
            }
        ]
    }
]
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