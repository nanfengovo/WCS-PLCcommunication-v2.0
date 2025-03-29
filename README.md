# WCS-PLCcommunication--v2.0
架构图：
![image](https://github.com/user-attachments/assets/5d4bc35a-1937-4c35-bd13-35f2cff12a13)
# * 技术栈：
       * 前端：
            1、vue3
            2、TypeScript
            3、Pinia
            4、Axios
            5、element-plus（考虑后续加element-plus x）
            6、vue-router
            7、xlsx
            8、dayjs
            9、normalize.css
            10、three.js
            11、echarts
       * 后端：
            1、ASP.NET Core 8.0 Webapi
            2、EntityFramekCore（8.0.13）
            3、NLog (5.4.0)
            4、S7.NET PLUS (0.20.0)
            5、NModbus(3.0.81）
            6、MiniEXcel（1.39.0）
            7、Polly(8.5.2)
            8、quartz.net
            9、Redis(待加入)
            10、SingIR(待加入)
            11、Identity(待加入)
# 文件夹说明：
![image](https://github.com/user-attachments/assets/826feb49-4fe5-475d-a3ff-d8fd352da37a)

 * UI里面是一个单独的前端的模板，支持动态的菜单，找到文件里面模拟后端返回的菜单数据后端传类似格式的就可以；支持Token（也是模拟的在F12后在应用中删掉token就需要重新登录）下面是效果图：
     ![image](https://github.com/user-attachments/assets/8adfe0a6-2d2b-4276-be30-b798a34c7984)
     ![image](https://github.com/user-attachments/assets/caa1df73-a1fc-4afd-9f60-b1f0e4cb554f)
     ![image](https://github.com/user-attachments/assets/60fc32f3-beea-418e-8467-74b5c1bbad4e)
     ![image](https://github.com/user-attachments/assets/efaaa1ab-edfa-4839-8eb9-74e348ee4286)
     ![image](https://github.com/user-attachments/assets/0fd17c7d-c560-4d4a-8cf1-cc4d00acd31a)
     ![image](https://github.com/user-attachments/assets/efc3ef01-540f-4466-870b-1cf1c7d3205a)




   * 前端文件夹说明：
     >前端启动：
     >1、npm install
     >2、npm run dev     
     ![image](https://github.com/user-attachments/assets/21c740d8-2906-4949-9038-b6482bafb674)
          * .history：历史记录
          * node_modules：安装的前端包
          * src：核心的代码文件
   * Src文件夹说明：
     ![image](https://github.com/user-attachments/assets/ad0310ab-2fbc-4dcb-9dc2-6514f3e2b9d1)
          * assent:放置样式和一些图片
          * components:放置一些公共的组件
          ![image](https://github.com/user-attachments/assets/2bb5a2ef-bed3-499a-aab2-d8b86a77d0dc)
               * main-header:公共的头部组件
               * c-cpns：
                    * header-crumb.vue：头部的面包屑组件
                    * header-info.vue :头部个人信息组件
               *main-menu: 侧边的菜单栏
          *router:放置和路由相关的配置
          *service:用来封装请求
          *store:放置需要持久化的东西（如token）
          *utils:放置插件
          *views:放置前端主要的页面（子组件）
          *package.json:
               相关的包的版本
               启动的端口
               ![image](https://github.com/user-attachments/assets/90c6472a-c9d1-459b-bbd0-62c1bf85ca7e)
          *vite。config.ts:运行npm run dev 后自动打开网页
          ![image](https://github.com/user-attachments/assets/647085be-fcdc-4978-9b37-867717882884)
* PLCCommunicationAPI：用来放后端的代码
       ![image](https://github.com/user-attachments/assets/1b493188-015a-46dd-aeac-868c7ddd1b71)
       *Model：用来存放模型(包括实体模型、DTO模型、API模型、后台任务模型、identity模型)
       *
       

* 其他的文件夹是过程中学习的记录
       * Modbus-Test：使用NModbus库读写变量点(使用的是控制台)
       * ModbusTcpAPI：使用NModbus库读写变量点(使用的是Webapi)
       * Read Configuration01：ASP.NET Core 读取系统的配置文件
       * S7Test：测试S7读写
       * 运维：放初始的配置数据
  

     
