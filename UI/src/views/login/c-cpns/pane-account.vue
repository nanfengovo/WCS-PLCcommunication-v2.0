<template>
  <div class="pane-account">
    <el-form :model="account" label-width="60px" :rules="accountRules" status-icon ref="formRef">
      <el-form-item label="账号" prop="userName">
        <el-input v-model="account.userName" placeholder="请输入账号" clearable />
      </el-form-item>
      <el-form-item label="密码" prop="userPwd">
        <el-input type="password" v-model="account.userPwd" placeholder="请输入密码" show-password clearable />
      </el-form-item>
    </el-form>
  </div>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import { type FormRules, type ElForm, ElMessage, ElNotification } from 'element-plus';
import router from '@/router';
import cache from '@/utils/cache';
import axios from 'axios';
//import { accountLoginRequest } from '@/service/Login/login';
// import useLoginStore from '@/store/login/login';

//模拟后端返回的菜单数组
const menuList = [
  {
    'id': 1,
    'name': '系统管理',
    'type': 1,
    'url': '/main/system',
    'icon': 'el-icon-monitor',
    'sort': 1,
    'children': [
      {
        'id': 2,
        'url': '/main/system/screen',
        'name': '大屏数据展示',
        'icon': 'el-icon-Platform',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 1
      },
      {
        'id': 3,
        'url': '/main/system/dashboard',
        'name': '系统监控面板',
        'icon': 'el-icon-Histogram',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 1
      },
      {
        'id': 4,
        'url': '/main/system/autoTask',
        'name': '自动任务管理',
        'icon': 'el-icon-Open',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 1
      }

    ]
  },
  {
    'id': 5,
    'name': '库位管理',
    'type': 1,
    'url': '/main/mapLocation',
    'icon': 'el-icon-MapLocation',
    'sort': 1,
    'children': [
      {
        'id': 6,
        'url': '/main/mapLocation/location',
        'name': '平面库位展示',
        'icon': 'el-icon-Location',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 5
      },
      {
        'id': 7,
        'url': '/main/mapLocation/3dLocation',
        'name': '3D库位展示',
        'icon': 'el-icon-AddLocation',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 5
      }
    ]
  },
  {
    'id': 8,
    'name': '任务管理',
    'type': 1,
    'url': '/main/task',
    'icon': 'el-icon-Odometer',
    'sort': 1,
    'children': [
      {
        'id': 9,
        'url': '/main/task/lift',
        'name': '提升机任务管理',
        'icon': 'el-icon-Clock',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 8
      },
      {
        'id': 10,
        'url': '/main/task/rgv',
        'name': '四向车任务管理',
        'icon': 'el-icon-PieChart',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 8
      }
    ]
  },
  {
    'id': 11,
    'name': '系统运维管理',
    'type': 1,
    'url': '/main/om',
    'icon': 'el-icon-Setting',
    'sort': 1,
    'children': [
      {
        'id': 12,
        'url': '/main/om/modbus',
        'name': 'Modbus数据点运维',
        'icon': 'el-icon-Link',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 11
      },
      {
        'id': 13,
        'url': '/main/om/S7',
        'name': 'S7数据点运维',
        'icon': 'el-icon-Cpu',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 11
      }
    ]
  },
  {
    'id': 14,
    'name': '系统日志管理',
    'type': 1,
    'url': '/main/log',
    'icon': 'el-icon-TrendCharts',
    'sort': 1,
    'children': [
      {
        'id': 15,
        'url': '/main/log/actionLog',
        'name': '操作日志',
        'icon': 'el-icon-Document',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 14
      },
      {
        'id': 16,
        'url': '/main/log/autoTaskLog',
        'name': '自动任务日志',
        'icon': 'el-icon-ChromeFilled',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 14
      },
      {
        'id': 17,
        'url': '/main/log/dbPointLog',
        'name': '数据点读写日志',
        'icon': 'el-icon-Aim',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 14
      }
    ]
  },
  {
    'id': 18,
    'name': '权限管理',
    'type': 1,
    'url': '/main/Permissions',
    'icon': 'el-icon-Setting',
    'sort': 1,
    'children': [
      {
        'id': 19,
        'url': '/main/Permissions/user',
        'name': '用户',
        'icon': 'el-icon-UserFilled',
        'sort': 1,
        'type': 2,
        'children': null,
        'parentId': 18
      },
      {
        'id': 20,
        'url': '/main/Permissions/role',
        'name': '角色',
        'icon': 'el-icon-Avatar',
        'sort': 2,
        'type': 2,
        'children': null,
        'parentId': 18
      }
    ]
  }
]

localStorage.setItem('menu', JSON.stringify(menuList))


// 账号密码
const account = reactive({
  userName: cache.getCache('name') ?? '',
  userPwd: cache.getCache('password') ?? ''
})


// 表单验证规则
const accountRules: FormRules = {
  userName: [
    { message: '必须输入账号~', required: true, trigger: 'blur' },
    { pattern: /^[a-z0-9]{3,10}$/, message: '必须是3到10位的数字或字母', trigger: 'blur' }
  ],
  userPwd: [
    { message: '必须输入密码~', required: true, trigger: 'blur' },
    { pattern: /^[a-z0-9]{3,}$/, message: '必须是3位以上的数字或字母', trigger: 'blur' }
  ]
}

//账号的登录逻辑
const formRef = ref<InstanceType<typeof ElForm>>()
//const loginStore = useLoginStore()
function loginAction(isRemPassword: boolean) {
  formRef.value?.validate((valid) => {
    if (valid) {
      //1.获取账号密码
      const name = account.userName
      const password = account.userPwd
      // 2. 发送网络请求
      axios.post('http://localhost:8888/api/Authorize/Login', {
        userName: name,
        userPwd: password
      })
        // accountLoginRequest({
        //     userName: name,
        //     userPwd: password
        // })
        .then(response => {
          // 处理登录成功的响应
          console.log(response.data);
          //   })
          if (response.data.code === 200)
          //if (name === 'admin' && password === '123456')
          {
            const token = response.data.data
            // 登录成功后，记录过期时间（假设 ExpireSeconds 是后端返回的 3600）
            const expiresIn = 3600; // 单位：秒
            const expireTime = Date.now() + expiresIn * 1000; // 转换为毫秒时间戳
            localStorage.setItem('token', token);
            localStorage.setItem('expire_time', expireTime.toString());
            localStorage.setItem('token', token)
            // 登录成功后存储Token
            //sessionStorage.setItem('token', token);
            //localStorage.setItem('name', name)
            cache.setCache('username', name)
            // console.log('token:', token)
            // console.log('登录成功')
            const menu = localStorage.getItem('menu')
            console.log('menu:' + menu)
            //动态的添加路由
            //const localRoutes: RouteRecordRaw[] = []
            //1.1 读取router/main所有的ts文件
            //const files: Record<string, any> = import.meta.glob('@/router/main/**/*.ts', { eager: true })
            //console.log('files:', files)
            // for (const key in files) {
            //     const module = files[key]
            //     localRoutes.push(module.default)
            // }
            //2.2 将读取到的路由添加到路由表中
            // if (menu) {
            //     for (const menuitem of JSON.parse(menu)) {
            //         for (const submenu of menuitem.children) {
            //             const route = localRoutes.find((item) => item.path === submenu.url)
            //             if (route) {
            //                 router.addRoute('main', route)
            //             }
            //         }
            //     }

            // }
            // router.addRoute('main', localRoutes[0])
            // console.log('@@@main', localRoutes)
            //3.获取本地路由
            console.log('main', router.getRoutes)
            //console.log(localRoutes)
            ElNotification({
              title: '登录成功',
              message: '欢迎回来',
              type: 'success',
              duration: 2000
            })
            router.push('/main/system/dashboard')
          } else {
            account.userName = ''
            account.userPwd = ''
            ElNotification({
              title: '登录失败',
              message: '用户名或密码不正确!',
              type: 'error',
              duration: 2000
            })
          }
        })
      //2.发送网络请求
      //useLoginStore.loginAccountAction({ userName: account.userName, userPwd: account.userPwd })
      //3.记住密码
      if (isRemPassword) {
        cache.setCache('name', name)
        cache.setCache('password', password)
      }
      else {
        cache.removeCache('name')
        cache.removeCache('password')
      }
    } else {
      ElMessage.error('请检查账号密码是否符合规范')
    }
  })
}

defineExpose({
  loginAction
})
</script>

<style lang="less" scoped></style>
