import { hyRequest } from '@/service'

export function accountLoginRequest(account: {
    userName: string
    userPwd: string
}) {
    return hyRequest.post({
        url: '/api/Authorize/Login',
        data: account
    })
}
