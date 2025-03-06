import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
    history: createWebHashHistory(),
    //映射关系:path=>component
    routes: [
        {
            path: '/login',
            component: () => import('@/views/login/login.vue')
        },
        {
            path: '/main',
            component: () => import('@/views/main/main.vue')
        }
    ]
})

export default router
