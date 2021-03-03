import request from '@/utils/request'
//首页文章列表
export function fetchList(params) {
    return request({
        url: '/Article/Load',
        method: 'get',
        params: params
    })
}
//焦点
export function fetchFocus() {
    return request({
        url: '/UserInfo/list',
        method: 'get',
        params: {}
    })
}
//文章分类
export function fetchCategory() {
    return request({
        url: '/UserInfo/category',
        method: 'get',
        params: {}
    })
}
//关联网站
export function fetchFriend() {
    return request({
        url: '/UserInfo/friend',
        method: 'get',
        params: {}
    })
}
//用户详情
export function fetchSiteInfo() {
    return request({
        url: '/UserInfo/GetUserDetails',
        method: 'get',
        params: {}
    })
}
//评论
export function fetchComment() {
    return request({
        url: '/UserInfo/comment',
        method: 'get',
        params: {}
    })
}
//文章详情

export function fetcharticle(params) {
    return request({
        url: '/article/getDetails',
        method: 'get',
        params: params
    })
}
