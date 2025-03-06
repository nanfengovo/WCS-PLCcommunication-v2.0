<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px">
            <el-form-item label="账号">
                <el-input v-model="account.name" placeholder="请输入账号" />
            </el-form-item>
            <el-form-item label="密码">
                <el-input type="password" v-model="account.password" placeholder="请输入密码" />
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