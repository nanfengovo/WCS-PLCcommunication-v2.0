import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: localStorage.getItem('token') || null // 或 sessionStorage
    }),
    actions: {
        setToken(token: string) {
            this.token = token
            localStorage.setItem('token', token) // 存储到 localStorage
        },
        clearToken() {
            this.token = null
            localStorage.removeItem('token') // 清除 localStorage
        }
    }
})
