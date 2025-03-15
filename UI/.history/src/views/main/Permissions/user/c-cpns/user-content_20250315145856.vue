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
        <el-table border style="width: 100%">
            <el-table-columm type="selection" />
            <el-table-column prop="date" label="Date" width="180" />
            <el-table-column prop="name" label="Name" width="180" />
            <el-table-column prop="address" label="Address" />
        </el-table>

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