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
import { type FormRules, type ElForm, ElMessage, ElNotification } from 'element-plus';
// import useLoginStore from '@/store/login/login';
import router from '@/router';
import cache from '@/utils/cache';

// 账号密码
const account = reactive({
    name: cache.getCache('name') ?? '',
    password: cache.getCache('password') ?? ''
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
//const loginStore = useLoginStore()
function loginAction(isRemPassword: boolean) {
    formRef.value?.validate((valid) => {
        if (valid) {
            //1.获取账号密码
            // const name = account.name
            // const password = account.password
            if (account.name === 'admin' && account.password === '123456') {
                const token = 'admin-token'
                localStorage.setItem('token', token)
                //console.log('token:', token)
                //console.log('登录成功')
                ElNotification({
                    title: '登录成功',
                    message: '欢迎回来',
                    type: 'success',
                    duration: 2000
                })
                router.push('/main')
            } else {
                console.log('登录失败')
            }
            //2.发送网络请求
            //loginStore.loginAccountAction({ name, password })
            //3.记住密码
            if (isRemPassword) {
                cache.setCache('name', account.name)
                cache.setCache('password', account.password)

            }
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