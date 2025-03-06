<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px">
            <el-form-item label="账号">
                <el-input />


            </el-form-item>
            <el-form-item label="密码">
                <el-input type="password" />


            </el-form-item>
        </el-form>
    </div>
</template>
<script setup lang="ts">
import { reactive } from 'vue';

const account = reactive({
    name: '',
    password: ''
})
</script>

<style lang="less" scoped>
.pane-account {}
</style>