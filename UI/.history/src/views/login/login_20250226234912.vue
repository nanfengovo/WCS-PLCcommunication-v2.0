<template>
    <div class="login">
        <loginPanel />
    </div>
</template>
<script setup lang="ts">
import loginPanel from './c-cpns/login-panel.vue';
</script>

<style lang="less" scoped>
.login {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100%;
    width: 100%;
    background: url('@/assets/img/image.png') rgba(255, 255, 255, 0.5) no-repeat center center;
    /* 这里的透明度可以根据需要调整 */
}
</style>