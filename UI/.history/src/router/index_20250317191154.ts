import {
    createRouter,
    createWebHashHistory,
    type RouteRecordRaw
} from 'vue-router'

const router = createRouter({
    history: createWebHashHistory(),
    //映射关系:path=>component
    routes: [
        {
            path: '/',
            redirect: '/main/system/dashboard'
        },
        {
            path: '/login',
            component: () => import('@/views/login/Login.vue')
        },
        {
            path: '/main',
            name: 'main',
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

// const localRoutes = [
//     {
//         path: '/main/system/screen',
//         component: () => import('@/views/main/system/screen/screen.vue')
//     },
//     {
//         path: '/main/system/dashboard',
//         component: () => import('@/views/main/system/dashboaed/dashboaed.vue')
//     },
//     {
//         path: '/main/system/autoTask',
//         component: () => import('@/views/main/system/autoTask/autoTask.vue')
//     }
// ]

//动态的添加路由
const localRoutes: RouteRecordRaw[] = []
//1.1 读取router/main所有的ts文件
const files: Record<string, any> = import.meta.glob('@/router/main/**/*.ts', {
    eager: true
})
//console.log('files:', files)
for (const key in files) {
    const module = files[key]
    localRoutes.push(module.default)
}

const menu = localStorage.getItem('menu')
//2.2 将读取到的路由添加到路由表中
if (menu) {
    for (const menuitem of JSON.parse(menu)) {
        for (const submenu of menuitem.children) {
            const route = localRoutes.find((item) => item.path === submenu.url)
            if (route) {
                router.addRoute('main', route)
            }
        }
    }
}
//动态mains
// router.addRoute('main', localRoutes[0])
// router.addRoute('main', localRoutes[2])
//导航守卫
//参数 1.to:Route:即将要进入的目标路由对象 2.from:Route:当前导航正要离开的路由

router.beforeEach((to) => {
    //const token = localStorage.getItem('token')
    if (to.path.startsWith('/main')) {
        //如果token不存在,则跳转到login界面
        //如果token存在,则跳转到main界面
        // function checkTokenExpiration() {
        //     const expireTime = parseInt(
        //         localStorage.getItem('expire_time') || '0',
        //         10
        //     )
        //     if (Date.now() >= expireTime) {
        //         //Token 已过期
        //         localStorage.removeItem('token')
        //         localStorage.removeItem('expire_time')
        //         return '/login'
        //     }
        // }

        // 每 10 秒检查一次（频率按需调整）
        // setInterval(checkTokenExpiration, 10 * 1000)

        const token = localStorage.getItem('token')

        if (!token) {
            return '/login'
        }
        //return '/main'
    }
    // if (to.path === '/main') {
    //     return '/main/system/dashboard'
    // }
    // if (from.path === to.path) {
    //     return from.path
    // }
})

export default router
