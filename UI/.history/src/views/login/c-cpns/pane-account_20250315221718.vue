<template>
    <div class="pane-account">
        <el-form :model="account" label-width="60px" :rules="accountRules" status-icon ref="formRef">
            <el-form-item label="账号" prop="userName">
                <el-input v-model="account.userName" placeholder="请输入账号" clearable />
            </el-form-item>
            <el-form-item label="密码" prop="userPwd">
                <el-input type="password" v-model="account.userPwd" placeholder="请输入密码" show-password clearable />
            </el-form-item>
        </el-form>
    </div>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import { type FormRules, type ElForm, ElMessage, ElNotification } from 'element-plus';
import router from '@/router';
import cache from '@/utils/cache';
import axios from 'axios';
// import useLoginStore from '@/store/login/login';


// 账号密码
const account = reactive({
    userName: cache.getCache('name') ?? '',
    userPwd: cache.getCache('password') ?? ''
})


// 表单验证规则
const accountRules: FormRules = {
    userName: [
        { message: '必须输入账号~', required: true, trigger: 'blur' },
        { pattern: /^[a-z0-9]{3,10}$/, message: '必须是3到10位的数字或字母', trigger: 'blur' }
    ],
    userPwd: [
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
            const name = account.userName
            const password = account.userPwd
            // 2. 发送网络请求
            axios.post('http://localhost:8888/api/Authorize/Login', {
                userName: name,
                userPwd: password
            })
                .then(response => {
                    // 处理登录成功的响应
                    console.log(response.data);
                    //   })
                    if (response.data.code === 200)
                    //if (name === 'admin' && password === '123456')
                    {
                        const token = 'admin-token'
                        localStorage.setItem('token', token)
                        // localStorage.setItem('name', name)
                        cache.setCache('username', name)
                        // console.log('token:', token)
                        // console.log('登录成功')
                        const menu = localStorage.getItem('menu')
                        console.log('menu:' + menu)
                        //动态的添加路由
                        //const localRoutes: RouteRecordRaw[] = []
                        //1.1 读取router/main所有的ts文件
                        //const files: Record<string, any> = import.meta.glob('@/router/main/**/*.ts', { eager: true })
                        //console.log('files:', files)
                        // for (const key in files) {
                        //     const module = files[key]
                        //     localRoutes.push(module.default)
                        // }
                        //2.2 将读取到的路由添加到路由表中
                        // if (menu) {
                        //     for (const menuitem of JSON.parse(menu)) {
                        //         for (const submenu of menuitem.children) {
                        //             const route = localRoutes.find((item) => item.path === submenu.url)
                        //             if (route) {
                        //                 router.addRoute('main', route)
                        //             }
                        //         }
                        //     }

                        // }
                        // router.addRoute('main', localRoutes[0])
                        // console.log('@@@main', localRoutes)
                        //3.获取本地路由
                        console.log('main', router.getRoutes)
                        //console.log(localRoutes)
                        ElNotification({
                            title: '登录成功',
                            message: '欢迎回来',
                            type: 'success',
                            duration: 2000
                        })
                        router.push('/main/system/dashboard')
                    } else {
                        account.userName = ''
                        account.userPwd = ''
                        ElNotification({
                            title: '登录失败',
                            message: '用户名或密码不正确!',
                            type: 'error',
                            duration: 2000
                        })
                    }
                })
            //2.发送网络请求
            //useLoginStore.loginAccountAction({ userName: account.userName, userPwd: account.userPwd })
            //3.记住密码
            if (isRemPassword) {
                cache.setCache('name', name)
                cache.setCache('password', password)
            }
            else {
                cache.removeCache('name')
                cache.removeCache('password')
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