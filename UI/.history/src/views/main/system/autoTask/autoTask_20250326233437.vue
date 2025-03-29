<template>
    <div class="S7autoTask">
        <S7AutoTask />

    </div>
    <div class="ModbusTCPautoTask">
        <ModbusTcpAuto />

    </div>

</template>
<script setup lang="ts">
import S7AutoTask from '@/views/main/system/autoTask/c-cpns/S7Auto.vue';
import ModbusTcpAuto from '@/views/main/system/autoTask/c-cpns/ModbusTcpAuto.vue';

</script>

<style lang="less" scoped></style>