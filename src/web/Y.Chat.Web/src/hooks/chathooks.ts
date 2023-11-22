import  config  from './../config.ts'
import { defaultavatar } from "./../utils/static.ts"


interface ChatHook{
    checkurl:Function
}

const regex = new RegExp("^(http|https)://([\\w.]+/?)\\S*$")
// 导出chatHook函数
export function chatHook(): ChatHook {
    
    // 定义checkurl函数，用于检查url并返回对应的头像链接
    const checkurl = (url: string | undefined): string => {
        // 如果url为undefined，则返回默认头像链接
        if (!url) return defaultavatar;
        // 如果url匹配正则表达式，则返回url本身作为头像链接
        if (url.match(regex)) return url;
        // 否则返回配置文件中的文件链接作为头像链接
        return config.getFile(url);
    }

    // 返回一个对象，包含checkurl函数
    return {
        checkurl
    }
}