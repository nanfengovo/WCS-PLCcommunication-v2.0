// export const BASE_URL = 'http://codercba.com:8000'
export const TIME_OUT = 10000

let BASE_URL = ''
if (import.meta.env.PROD) {
    BASE_URL = 'http://codercba.com:8000'
} else {
    BASE_URL = 'http://codercba.com:8000'
}

export { BASE_URL }
