import { accountLoginRequest } from '@/service/Login/login'
import { defineStore } from 'pinia'

const useLoginStore = defineStore('login', {
    state: () => ({
        token: '',
        userInfo: {},
        menuList: [],
        role: '',
        permissions: []
    }),
    actions: {
        async loginAccount(account: any) {
            const loginResult = await accountLoginRequest(account)
            console.log(loginResult)
        }
    }
})

export default useLoginStore
