<template>
    <div class="autoTask">
        <S7AutoTask />

    </div>
    <ModbusTcpAuto />

</template>
<script setup lang="ts">
import S7AutoTask from '@/views/main/system/autoTask/c-cpns/S7Auto.vue';
import ModbusTcpAuto from '@/views/main/system/autoTask/c-cpns/ModbusTcpAuto.vue';

</script>

<style lang="less" scoped>
.autoTask {
    padding: 20px;
    border-radius: 5px;
    background-color: #fff;
}
</style>