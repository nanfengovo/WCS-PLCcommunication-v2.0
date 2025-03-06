import { BASE_URL, TIME_OUT } from './config'
import HYRequest from './request'

const hyRequest = new HYRequest({
    baseURL: BASE_URL,
    timeout: TIME_OUT
})

const hyRequest2 = new HYRequest({
    baseURL: 'http://codercba.com:1888/airbnb/api',
    timeout: 8000,
    interceptors: {
        requestSuccessFn: (config: any) => {
            console.log('请求成功的拦截')
            return config
        },
        requestFailureFn: (err: any) => {
            console.log('请求失败的拦截')
            return err
        },
        responseSuccessFn: (res: any) => {
            console.log('响应成功的拦截')
            return res
        },
        responseFailureFn: (err: any) => {
            console.log('响应失败的拦截')
            return err
        }
    }
})

export { hyRequest, hyRequest2 }
