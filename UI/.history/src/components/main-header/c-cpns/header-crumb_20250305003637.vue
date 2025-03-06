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
import { ref, watchEffect } from 'vue';
import { useRoute } from 'vue-router';

interface IBreadcrumbs {
    name: string;
    path?: string;
}

// 示例菜单结构，实际应用中需要根据实际情况来定义
interface IMenuItem {
    name: string;
    url: string;
    children?: IMenuItem[];
}

function mapPathToBreadcrumbs(path: string, menus: IMenuItem[]): IBreadcrumbs[] {
    const breadcrumbs: IBreadcrumbs[] = [];

    function findBreadcrumb(menu: IMenuItem): boolean {
        if (menu.url === path) {
            breadcrumbs.push({ name: menu.name });
            return true;
        }
        if (menu.children) {
            for (const child of menu.children) {
                if (findBreadcrumb(child)) {
                    breadcrumbs.push({ name: menu.name });
                    return true;
                }
            }
        }
        return false;
    }

    menus.forEach(findBreadcrumb);
    return breadcrumbs.reverse(); // 反转数组以匹配正确的顺序
}

const route = useRoute();
const breadcrumbs = ref<IBreadcrumbs[]>([]);

watchEffect(() => {
    const menu = localStorage.getItem('menu');
    if (menu) {
        const menus = JSON.parse(menu) as IMenuItem[];
        breadcrumbs.value = mapPathToBreadcrumbs(route.path, menus);
    }
});
</script>
