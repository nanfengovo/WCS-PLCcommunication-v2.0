<template>
    <div class="crumb">
        <el-breadcrumb separator-icon="ArrowRight">
            <template v-for="item in breadcrumbs" :key="item.name">
                <el-breadcrumb-item :to="item.path">{{ item.name }}</el-breadcrumb-item>
            </template>
        </el-breadcrumb>
    </div>
</template>
<script setup lang="ts">
// import { mapPathToBreadcrumbs } from '@/utils/map-menus';
// import { useRoute } from 'vue-router';

// const route = useRoute();
// const menu = localStorage.getItem('menu');
// const breadcrumbs = mapPathToBreadcrumbs(route.path, menu ? JSON.parse(menu) : []);
// console.log('##', menu);

interface IBreadcrumbs {
    name: string
    path: string
}
function mapPathToBreadcrumbs(path: string, menus: any[]) {
    const breadcrumbs: IBreadcrumbs[] = []
    for (const menuitem of menus) {
        for (const submenu of menuitem.children) {
            if (submenu.path === path) {
                breadcrumbs.push({ name: menuitem.name, path: menuitem.url })
                breadcrumbs.push({ name: submenu.name, path: submenu.url })
                console.log('##', submenu.name)
                return breadcrumbs
            }
        }
        console.log('##', menuitem.name)

    }

}


</script>

<style lang="less" scoped></style>