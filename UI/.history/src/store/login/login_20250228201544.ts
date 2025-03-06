import { accountLoginRequest } from '@/service/Login/login'
import { defineStore } from 'pinia'

const useLoginStore = defineStore('login', {
    state: () => ({
        id: '',
        name: '',
        token: ''
    }),
    actions: {
        async loginAccountAction(account: any) {
            const loginResult = await accountLoginRequest(account)
            this.id = loginResult.data.id
            this.name = loginResult.data.name
            this.token = loginResult.data.token
        }
    }
})

export default useLoginStore
