<script setup lang="ts">
import { onMounted, onBeforeUnmount } from 'vue';
import { useAuthStore } from '@/store/authStore/authStore';

const authStore = useAuthStore();

const handleVisibilityChange = () => {
  if (document.visibilityState === 'hidden') {
    // 页面隐藏时（关闭或切换标签页），清除 Token
    authStore.clearToken();
  }
};

onMounted(() => {
  document.addEventListener('visibilitychange', handleVisibilityChange);
});

onBeforeUnmount(() => {
  document.removeEventListener('visibilitychange', handleVisibilityChange);
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
