<template>
  <div class="user-search">
    <userSearch />
    <useContent />
  </div>
</template>
<script setup lang="ts">
import userSearch from '@/views/main/Permissions/user/c-cpns/user-search.vue'
import userContent from '@/views/main/Permissions/user/c-cpns/user-content.vue';
</script>

<style lang="less" scoped>
.user-search {
  width: 100%;
  border-radius: 20px;
}
</style>
