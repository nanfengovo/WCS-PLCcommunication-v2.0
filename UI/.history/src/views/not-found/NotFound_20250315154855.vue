<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
    <div class="min-h-screen bg-gradient-to-br from-indigo-50 to-blue-100 flex items-center justify-center p-8">
        <div class="max-w-3xl w-full text-center">
            <!-- 404 主标题 -->
            <div class="relative mb-12">
                <h1 class="text-9xl font-bold text-indigo-900/10">404</h1>
                <div class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2">
                    <img :src="'https://public.readdy.ai/ai/img_res/bff84bf92bbcc6c7bd0e42999a0f3cf9.jpg'"
                        alt="404 Robot" class="w-48 h-48 object-cover rounded-full shadow-xl" />
                </div>
            </div>

            <!-- 错误信息 -->
            <div class="space-y-6 mb-12">
                <h2 class="text-3xl font-bold text-indigo-900">页面未找到</h2>
                <p class="text-lg text-indigo-700">抱歉，您访问的页面可能已被移动或不存在</p>
            </div>

            <!-- 建议操作 -->
            <div class="space-y-8">
                <div class="flex flex-col items-center space-y-4">
                    <div class="text-indigo-600">您可以尝试以下操作：</div>
                    <div class="flex space-x-4">
                        <button @click="goBack"
                            class="!rounded-button whitespace-nowrap px-6 py-3 bg-indigo-600 text-white hover:bg-indigo-700 transition-colors duration-300 cursor-pointer flex items-center space-x-2">
                            <el-icon>
                                <ArrowLeft />
                            </el-icon>
                            <span>返回上一页</span>
                        </button>
                        <button @click="goHome"
                            class="!rounded-button whitespace-nowrap px-6 py-3 bg-white text-indigo-600 border-2 border-indigo-600 hover:bg-indigo-50 transition-colors duration-300 cursor-pointer flex items-center space-x-2">
                            <el-icon>
                                <House />
                            </el-icon>
                            <span>返回首页</span>
                        </button>
                    </div>
                </div>

                <!-- 搜索框 -->
                <div class="max-w-md mx-auto">
                    <div class="relative">
                        <input v-model="searchQuery" type="text" placeholder="搜索内容..."
                            class="w-full px-4 py-3 pl-12 border-2 border-indigo-200 rounded-lg focus:outline-none focus:border-indigo-600 text-indigo-900 placeholder-indigo-300"
                            @keyup.enter="handleSearch" />
                        <el-icon class="absolute left-4 top-1/2 transform -translate-y-1/2 text-indigo-400">
                            <Search />
                        </el-icon>
                    </div>
                </div>
            </div>

            <!-- 随机推荐 -->
            <div class="mt-16">
                <h3 class="text-xl font-semibold text-indigo-900 mb-6">您可能感兴趣的内容</h3>
                <div class="grid grid-cols-3 gap-6">
                    <div v-for="(item, index) in recommendedContent" :key="index" @click="handleRecommendClick(item)"
                        class="bg-white p-6 rounded-xl shadow-md hover:shadow-lg transition-shadow duration-300 cursor-pointer">
                        <img :src="item.image" :alt="item.title" class="w-full h-32 object-cover rounded-lg mb-4" />
                        <h4 class="text-lg font-medium text-indigo-900 mb-2">{{ item.title }}</h4>
                        <p class="text-sm text-indigo-600">{{ item.description }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { ArrowLeft, House, Search } from '@element-plus/icons-vue';

const searchQuery = ref('');

const recommendedContent = [
    {
        title: '产品介绍',
        description: '了解我们的最新产品特性和创新技术',
        image: 'https://public.readdy.ai/ai/img_res/e3b963190f39a4a79e3e614cae18c3b4.jpg'
    },
    {
        title: '技术支持',
        description: '获取专业的技术支持和解决方案',
        image: 'https://public.readdy.ai/ai/img_res/778993aee098ea8cc7b5dbbdc26261f4.jpg'
    },
    {
        title: '关于我们',
        description: '探索我们的企业文化和发展历程',
        image: 'https://public.readdy.ai/ai/img_res/1e5b109625e8dac3aedd4c8e468ef46b.jpg'
    }
];

const goBack = () => {
    window.history.back();
};

const goHome = () => {
    window.location.href = '/';
};

const handleSearch = () => {
    if (searchQuery.value.trim()) {
        // 实际项目中替换为真实的搜索逻辑
        console.log('Searching for:', searchQuery.value);
    }
};

const handleRecommendClick = (item: typeof recommendedContent[0]) => {
    // 实际项目中替换为真实的跳转逻辑
    console.log('Clicked recommendation:', item.title);
};
</script>

<style scoped>
.text-9xl {
    font-size: 15rem;
    line-height: 1;
}
</style>