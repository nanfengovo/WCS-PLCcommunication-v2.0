<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px" :rules="accountRules">
            <el-form-item label="账号" prop="name">
                <el-input v-model="account.name" placeholder="请输入账号" clearable />
            </el-form-item>
            <el-form-item label="密码">
                <el-input type="password" v-model="account.password" placeholder="请输入密码" show-password clearable />
            </el-form-item>
        </el-form>
    </div>
</template>
<script setup lang="ts">
import { reactive } from 'vue';
import type { FormRules } from 'element-plus';
// 账号密码
const account = reactive({
    name: '',
    password: ''
})


// 表单验证规则
const accountRules: FormRules = {
    name: [
        { message: '必须输入账号~', required: true, trigger: 'blur' },
        { min: 3, max: 10, message: '长度在 3 到 10 个字符', trigger: 'blur' }
    ]
}
</script>

<style lang="less" scoped>
.pane-account {}
</style>