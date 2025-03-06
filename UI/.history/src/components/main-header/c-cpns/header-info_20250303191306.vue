<template>
    <div class="header-info">
        <!--1.操作小图标-->
        <div class="operation">
            <span>
                <el-icon>
                    <Message />
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
                    <span>
                        <el-avatar :size="30" src="src\assets\img\OIP-C.jpg" />
                        <span class="name">admin</span>
                    </span>
                </span>
                <template #dropdown>
                    <el-dropdown-menu>
                        <el-dropdown-item>Action 1</el-dropdown-item>
                        <el-dropdown-item>Action 2</el-dropdown-item>
                        <el-dropdown-item>Action 3</el-dropdown-item>
                        <el-dropdown-item disabled>Action 4</el-dropdown-item>
                        <el-dropdown-item divided>Action 5</el-dropdown-item>
                    </el-dropdown-menu>
                </template>
            </el-dropdown>
        </div>
    </div>
</template>
<script setup lang="ts">
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
</style>