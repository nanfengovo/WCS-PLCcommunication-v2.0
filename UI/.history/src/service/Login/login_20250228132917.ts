import { hyRequest } from '@/service'

export function accountLoginRequest(account: any) {
    return hyRequest.post({
        url: '/login',
        data: account
    })
}
