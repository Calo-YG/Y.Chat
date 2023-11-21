import Request from './htttpRequest'

const baseurl = '/ChatMessage'
const messages = '/QueryMessage'

class MessageServices{
    query(chatId:string,page:Number=0,pagesize:10){
        const url = `${baseurl}${messages}?chatId=${chatId}&page=${page}&pageSize=${pagesize}`
        return Request.get(url)
    }
}

export default new MessageServices()