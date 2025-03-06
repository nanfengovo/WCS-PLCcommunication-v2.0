import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
    history: createWebHashHistory(),
    //映射关系:path=>component
    routes: [
        {
            path: '/',
            redirect: '/login'
        },
        {
            path: '/login',
            component: () => import('@/views/login/login.vue')
        },
        {
            path: '/main',
            component: () => import('@/views/main/main.vue')
        },
        {
            path: '/:pathMatch(.*)',
            component: () => import('@/views/not-found/NotFound.vue')
        }
    ]
})
export default router
