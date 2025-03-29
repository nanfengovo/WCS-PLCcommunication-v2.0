<template>
    <div class="autoTask">
        <S7AutoTask />
        <h1>fengg</h1>
        <ModbusTcpAuto />
    </div>


</template>
<script setup lang="ts">
import S7AutoTask from '@/views/main/system/autoTask/c-cpns/S7Auto.vue';
import ModbusTcpAuto from '@/views/main/system/autoTask/c-cpns/ModbusTcpAuto.vue';

</script>

<style lang="less" scoped>
.autoTask {
    margin-top: 20px;
    background-color: #fff;
}
</style>