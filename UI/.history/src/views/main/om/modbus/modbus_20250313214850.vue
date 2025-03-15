<template>
    <div class="modbus">
        <modbusSearch />
        <ModbusContent />
    </div>
</template>
<script setup lang="ts">
import ModbusContent from './c-cpns/modbus-content.vue';
import modbusSearch from './c-cpns/modbus-search.vue';

</script>

<style lang="less" scoped></style>