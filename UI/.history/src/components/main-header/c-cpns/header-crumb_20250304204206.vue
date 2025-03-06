<template>
    <el-breadcrumb class="custom-breadcrumb" separator-icon="ArrowRight">
        <el-breadcrumb-item :to="{ path: '/' }">homepage</el-breadcrumb-item>
        <el-breadcrumb-item>promotion management</el-breadcrumb-item>
        <el-breadcrumb-item>promotion list</el-breadcrumb-item>
        <el-breadcrumb-item>promotion detail</el-breadcrumb-item>
    </el-breadcrumb>
</template>

<style scoped>
.custom-breadcrumb {
    display: flex;
    align-items: center;
    font-size: 16px;
    /* 增大字体大小 */
}

.custom-breadcrumb .el-breadcrumb__item {
    position: relative;
    padding: 0 8px;
    /* 增加内边距 */
}

.custom-breadcrumb .el-breadcrumb__item::after {
    content: '';
    position: absolute;
    top: 50%;
    right: -4px;
    margin-top: -3px;
    width: 6px;
    height: 6px;
    border-right: 1px solid #d9d9d9;
    /* 分隔线颜色 */
    border-bottom: 1px solid #d9d9d9;
    transform: rotate(-45deg);
}

.custom-breadcrumb .el-breadcrumb__inner,
.custom-breadcrumb .el-breadcrumb__inner a {
    color: #409EFF;
    /* 更鲜艳的蓝色 */
    text-decoration: none;
    /* 移除下划线 */
}

.custom-breadcrumb .el-breadcrumb__inner:hover,
.custom-breadcrumb .el-breadcrumb__inner a:hover {
    color: #3A8EE6;
    /* 鼠标悬停时的颜色 */
    cursor: pointer;
}

/* 移除最后一个面包屑项的分隔线 */
.custom-breadcrumb .el-breadcrumb__item:last-child::after {
    display: none;
}
</style>