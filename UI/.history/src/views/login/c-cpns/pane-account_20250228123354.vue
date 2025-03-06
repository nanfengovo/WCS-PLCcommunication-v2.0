<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px" :rules="accountRules" status-icon>
            <el-form-item label="账号" prop="name">
                <el-input v-model="account.name" placeholder="请输入账号" clearable />
            </el-form-item>
            <el-form-item label="密码" prop="password">
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
        { pattern: /^[a-z0-9]{3,10}$/, message: '必须是3到10位的数字或字母', trigger: 'change' }
    ],
    password: [
        { message: '必须输入密码~', required: true, trigger: 'blur' },
        { pattern: /^[a-z0-9]{3,}$/, message: '必须是3位以上的数字或字母', trigger: 'change' }
    ]
}

//账号的登录逻辑
function loginAction() {
    console.log('账号登录', account)
}

defineExpose({
    loginAction
})
</script>

<style lang="less" scoped>
.pane-account {}
</style>