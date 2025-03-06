import { hyRequest } from '@/service'

export function accountLogin(account: any) {
    hyRequest.post({
        url: '/login',
        data: account
    })
}
