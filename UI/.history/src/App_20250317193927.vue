<script setup lang="ts">
import { onMounted, onBeforeUnmount } from 'vue';
import { useAuthStore } from '@/store/useAuthStore';

const authStore = useAuthStore();

// 清除 Token 的方法
const clearToken = () => {
  authStore.clearToken(); // 调用 Pinia Store 的 Action
};

// 生命周期钩子
onMounted(() => {
  window.addEventListener('beforeunload', clearToken);
});

onBeforeUnmount(() => {
  window.removeEventListener('beforeunload', clearToken);
});
</script>

<template>
  <div class="app">
    <router-view />
  </div>

</template>

<style scoped>
.app {
  height: 100vh;
  width: 100vw;
}
</style>
