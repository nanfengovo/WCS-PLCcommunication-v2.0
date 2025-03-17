<template>
    <div class="header-info">
        <!--1.操作小图标-->
        <div class="operation">
            <span>
                <el-icon @click="handleFullScreenClick">
                    <FullScreen />
                </el-icon>
            </span>
            <span>
                <span class="dot"></span>
                <el-icon>
                    <ChatDotRound />
                </el-icon>
            </span>
            <span>
                <el-icon>
                    <Search />
                </el-icon>
            </span>
        </div>

        <!--2.头像&&个人信息-->
        <div class="info">
            <el-dropdown>
                <span class="user-info">
                    <el-avatar :size="30" src="src\assets\img\OIP-C.jpg" />
                    <span class="name">{{ username }}</span>
                </span>
                <template #dropdown>
                    <el-dropdown-menu>
                        <el-dropdown-item>
                            <el-icon>
                                <CircleClose />
                            </el-icon>
                            <span @click="handleExitClick">退出系统</span>
                        </el-dropdown-item>
                        <el-dropdown-item>
                            <el-icon>
                                <InfoFilled />
                            </el-icon>
                            <span>
                                个人信息
                            </span>
                        </el-dropdown-item>
                        <el-dropdown-item>
                            <el-icon>
                                <EditPen />
                            </el-icon>
                            <span>
                                修改密码
                            </span>
                        </el-dropdown-item>
                    </el-dropdown-menu>
                </template>
            </el-dropdown>
        </div>
    </div>
</template>
<script setup lang="ts">
import router from '@/router';

const name = localStorage.getItem('name');
const username = name?.replace(/"/g, '');

//退出登录
function handleExitClick() {
    //1.清除token
    localStorage.removeItem('token');
    // 手动退出时清除
    //sessionStorage.removeItem('token');
    //2.跳转到登录页
    router.push('/login');
}

//全屏
const handleFullScreenClick = () => {
    if (!document.fullscreenElement) {
        document.documentElement.requestFullscreen();
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    }
}

</script>

<style lang="less" scoped>
.header-info {
    display: flex;
    align-items: center;
}

.operation {
    display: inline-flex;
    margin-right: 20px;

    span {
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 40px;
        height: 35px;
        cursor: pointer;

        &:hover {
            background-color: #f2f2f2;
        }

        i {
            font-size: 20px;
        }

        .dot {
            position: absolute;
            top: 3px;
            right: 3px;
            z-index: 10;
            width: 6px;
            height: 6px;
            border-radius: 100%;
            background-color: red;
        }
    }
}

.info {
    .user-info {
        display: flex;
        align-items: center;
        cursor: pointer;

        .name {
            margin-left: 8px;
        }
    }
}
</style>