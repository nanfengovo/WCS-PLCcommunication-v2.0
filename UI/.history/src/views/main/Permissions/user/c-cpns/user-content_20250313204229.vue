<template>
    <div class="user-content">
        <div class="content-top">
            <h2>用户列表</h2>
            <el-button icon="Plus" type="primary">新增用户</el-button>
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
}
</style>