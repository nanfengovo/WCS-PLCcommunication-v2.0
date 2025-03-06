<template>
    <div class="autoTaskLog">
        <h2>autoTaskLog</h2>
    </div>
</template>
<script setup lang="ts">
import { useRoute } from 'vue-router';


const route = useRoute();
console.log(route.path);
</script>

<style lang="less" scoped></style>