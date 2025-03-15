// export const BASE_URL = 'http://codercba.com:8000'
export const TIME_OUT = 10000

let BASE_URL = ''
if (import.meta.env.PROD) {
    BASE_URL = 'http://localhost:8888'
} else {
    BASE_URL = 'http://localhost:8888'
}

export { BASE_URL }

//通过创建.env文件直接创建变量
