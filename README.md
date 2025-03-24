# WCS-PLCcommunication--v2.0
架构图：
![image](https://github.com/user-attachments/assets/5d4bc35a-1937-4c35-bd13-35f2cff12a13)

# 文件夹说明：
![image](https://github.com/user-attachments/assets/826feb49-4fe5-475d-a3ff-d8fd352da37a)

 * UI里面是一个单独的前端的模板，支持动态的菜单，找到文件里面模拟后端返回的菜单数据后端传类似格式的就可以；支持Token（也是模拟的在F12后在应用中删掉token就需要重新登录）下面是效果图：
     ![image](https://github.com/user-attachments/assets/8adfe0a6-2d2b-4276-be30-b798a34c7984)
     ![image](https://github.com/user-attachments/assets/caa1df73-a1fc-4afd-9f60-b1f0e4cb554f)
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
  

     
