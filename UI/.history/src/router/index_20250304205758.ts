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
            component: () => import('@/views/main/main.vue') //二级路由
            // children: [
            //     {
            //         path: '/main/system/screen',
            //         component: () =>
            //             import('@/views/main/system/screen/screen.vue')
            //     },
            //     {
            //         path: '/main/system/dashboard',
            //         component: () =>
            //             import('@/views/main/system/dashboaed/dashboaed.vue')
            //     },
            //     {
            //         path: '/main/system/autoTask',
            //         component: () =>
            //             import('@/views/main/system/autoTask/autoTask.vue')
            //     },
            //     {
            //         path: '/main/mapLocation/location',
            //         component: () =>
            //             import('@/views/main/system/location/location.vue')
            //     },
            //     {
            //         path: '/main/mapLocation/3dLocation',
            //         component: () =>
            //             import('@/views/main/system/3dLocation/3dLocation.vue')
            //     },
            //     {
            //         path: '/main/task/lift',
            //         component: () => import('@/views/main/system/lift/lift.vue')
            //     },
            //     {
            //         path: '/main/task/rgv',
            //         component: () => import('@/views/main/system/rgv/rgv.vue')
            //     },
            //     {
            //         path: '/main/om/modbus',
            //         component: () =>
            //             import('@/views/main/system/modbus/modbus.vue')
            //     },
            //     {
            //         path: '/main/om/S7',
            //         component: () => import('@/views/main/system/S7/S7.vue')
            //     },
            //     {
            //         path: '/main/log/actionLog',
            //         component: () =>
            //             import('@/views/main/system/actionLog/actionLog.vue')
            //     },
            //     {
            //         path: '/main/log/autoTaskLog',
            //         component: () =>
            //             import(
            //                 '@/views/main/system/autoTaskLog/autoTaskLog.vue'
            //             )
            //     },
            //     {
            //         path: '/main/log/dbPointLog',
            //         component: () =>
            //             import('@/views/main/system/dbPointLog/dbPointLog.vue')
            //     },
            //     {
            //         path: '/main/Permissions/user',
            //         component: () => import('@/views/main/system/user/user.vue')
            //     },
            //     {
            //         path: '/main/Permissions/role',
            //         component: () => import('@/views/main/system/role/role.vue')
            //     }
            // ]
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
    if (to.path.startsWith('/main')) {
        //如果token不存在,则跳转到login界面
        //如果token存在,则跳转到main界面
        const token = localStorage.getItem('token')
        if (!token) {
            return '/login'
        }
        //return '/main'
    }
    if (to.path === '/main') {
        return '/main/system/dashboard'
    }
    // if (from.path === to.path) {
    //     return from.path
    // }
})

export default router
