<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px" :rules="accountRules" status-icon ref="formRef">
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
import { reactive, ref } from 'vue';
import { type FormRules, type ElForm, ElMessage } from 'element-plus';
import type { Instance } from 'element-plus/lib/components/index.js';

// 账号密码
const account = reactive({
    name: '',
    password: ''
})


// 表单验证规则
const accountRules: FormRules = {
    name: [
        { message: '必须输入账号~', required: true, trigger: 'blur' },
        { pattern: /^[a-z0-9]{3,10}$/, message: '必须是3到10位的数字或字母', trigger: 'blur' }
    ],
    password: [
        { message: '必须输入密码~', required: true, trigger: 'blur' },
        { pattern: /^[a-z0-9]{3,}$/, message: '必须是3位以上的数字或字母', trigger: 'blur' }
    ]
}

//账号的登录逻辑
const formRef = ref<InstanceType<typeof ElForm>>()
function loginAction() {
    formRef.value?.validate((valid) => {
        if (valid) {
            console.log('验证符合', account)
        } else {
            ElMessage.error('请检查账号密码是否符合规范')
        }
    })
}

defineExpose({
    loginAction
})
</script>

<style lang="less" scoped>
.pane-account {}
</style>