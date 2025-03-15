<template>
    <div class="user-content">
        <div class="content-top">
            <div class="title">
                <h2>用户列表</h2>
                <el-button type="primary" icon="Refresh">刷新</el-button>
                <el-button type="danger" icon="delete">删除</el-button>
            </div>
            <el-button icon="Plus" type="primary">新增用户</el-button>
        </div>
        <div class="content">
            <el-table border style="width: 100%">
                <el-table-column align="center" type="selection" width="40px" />
                <el-table-column align="center" type="index" label="序号" width="60px" />
                <el-table-column align="center" prop="date" label="用户名" width="180" />
                <el-table-column align="center" prop="name" label="角色" width="180" />
                <el-table-column align="center" label="操作" width="180">
                    <template>
                        <el-button type="primary" size="small">编辑</el-button>
                        <el-button type="danger" size="small">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>


    </div>
</template>
<script setup lang="ts">
</script>

<style lang="less" scoped>
.user-content {
    margin-top: 20px;
    background-color: #fff;
    border-radius: 20px;
}

.content-top {
    display: flex;
    justify-content: space-between;
    padding: 20px;
    align-items: flex-end;
    margin-bottom: 15px;

    .title {
        display: flex;
        align-items: center;

        h2 {
            font-size: 18px;
            margin-right: 20px;
        }
    }
}
</style>