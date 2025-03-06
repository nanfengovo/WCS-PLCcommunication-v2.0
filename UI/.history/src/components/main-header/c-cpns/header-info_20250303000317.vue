<template>
    <div class="header-info">
        <div class="operation">
            <span>
                <el-icon>
                    <Message />
                </el-icon>
            </span>
            <span>
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
    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
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
    }
}
</style>