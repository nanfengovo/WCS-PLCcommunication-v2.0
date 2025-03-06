import router from '@/router'
// import { accountLoginRequest } from '@/service/Login/login'
import { defineStore } from 'pinia'

const useLoginStore = defineStore('login', {
    // state: () => ({
    //     id: '',
    //     name: '',
    //     token: localStorage.getItem('token') ?? ''
    // }),
    actions: {
        async loginAccountAction(account: { name: string; password: string }) {
            //const loginResult = await accountLoginRequest(account)
            //1.账号登录获取token等信息
            //this.id = loginResult.data.id
            //this.name = loginResult.data.name
            //this.token = loginResult.data.token
            //2.进行本地缓存
            //localStorage.setItem('token', this.token)
            //3.页面跳转(main界面)
            if (account.name === 'admin' && account.password === '123456') {
                const token = 'admin-token'
                console.log('token:', token)
                console.log('登录成功')
                router.push('/main')
            }
        }
    }
})

export default useLoginStore
