<template>
    <div class="crumb">
        <el-breadcrumb separator-icon="ArrowRight">
            <el-breadcrumb-item :to="{ path: '/' }">homepage</el-breadcrumb-item>
            <el-breadcrumb-item>promotion management</el-breadcrumb-item>
            <el-breadcrumb-item>promotion list</el-breadcrumb-item>
            <el-breadcrumb-item>promotion detail</el-breadcrumb-item>
        </el-breadcrumb>
    </div>
</template>
<script setup lang="ts">
import { mapPathToBreadcrumbs } from '@/utils/map-menus';
import { useRoute } from 'vue-router';

const route = useRoute();
const menu = localStorage.getItem('menu');
const breadcrumbs = mapPathToBreadcrumbs(route.path, menu ? JSON.parse(menu) : []);
</script>

<style lang="less" scoped></style>