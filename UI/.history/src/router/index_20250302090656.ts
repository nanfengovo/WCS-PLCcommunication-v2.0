import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
    history: createWebHashHistory(),
    //映射关系:path=>component
    routes: [
        {
            path: '/',
            redirect: '/main'
        },
        {
            path: '/login',
            component: () => import('@/views/login/Login.vue')
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
//导航守卫
//参数 1.to:Route:即将要进入的目标路由对象 2.from:Route:当前导航正要离开的路由
// eslint-disable-next-line @typescript-eslint/no-unused-vars
router.beforeEach((to, from) => {
    //const token = localStorage.getItem('token')
    if (to.path === '/main') {
        //如果token不存在,则跳转到login界面
        //如果token存在,则跳转到main界面
        const token = localStorage.getItem('token')
        if (!token) {
            return '/login'
        }
        //return '/main'
    }
})

export default router
