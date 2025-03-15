import { hyRequest } from '@/service'

export function accountLoginRequest(account: {
    name: string
    password: string
}) {
    return hyRequest.post({
        url: '/api/Authorize/Login',
        data: account
    })
}
