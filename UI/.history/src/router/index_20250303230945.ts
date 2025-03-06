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
            component: () => import('@/views/main/main.vue'), //二级路由
            children: [
                {
                    path: '/system/screen',
                    component: () =>
                        import('@/views/main/system/screen/screen.vue')
                },
                {
                    path: '/system/dashboard',
                    component: () =>
                        import('@/views/main/system/dashboaed/dashboaed.vue')
                },
                {
                    path: '/system/autoTask',
                    component: () =>
                        import('@/views/main/system/autoTask/autoTask.vue')
                },
                {
                    path: '/system/location',
                    component: () =>
                        import('@/views/main/system/location/location.vue')
                },
                {
                    path: '/system/3dLocation',
                    component: () =>
                        import('@/views/main/system/3dLocation/3dLocation.vue')
                },
                {
                    path: '/system/lift',
                    component: () => import('@/views/main/system/lift/lift.vue')
                },
                {
                    path: '/system/rgv',
                    component: () => import('@/views/main/system/rgv/rgv.vue')
                },
                {
                    path: '/system/modbus',
                    component: () =>
                        import('@/views/main/system/modbus/modbus.vue')
                },
                {
                    path: '/system/S7',
                    component: () => import('@/views/main/system/S7/S7.vue')
                },
                {
                    path: '/system/actionLog',
                    component: () =>
                        import('@/views/main/system/actionLog/actionLog.vue')
                },
                {
                    path: '/system/autoTaskLog',
                    component: () =>
                        import(
                            '@/views/main/system/autoTaskLog/autoTaskLog.vue'
                        )
                }
            ]
        },
        {
            path: '/:pathMatch(.*)',
            component: () => import('@/views/not-found/NotFound.vue')
        }
    ]
})
//导航守卫
//参数 1.to:Route:即将要进入的目标路由对象 2.from:Route:当前导航正要离开的路由

router.beforeEach((to, from, next) => {
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
    if (from.path === to.path) {
        return from.path
    }
    next()
})

export default router
