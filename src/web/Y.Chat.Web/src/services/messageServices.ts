import Request from './htttpRequest'

const baseurl = '/v1/ChatMessages'
const messages = '/QueryMessage'

class MessageServices{
    query(chatId:string,page:Number=0,pagesize:10){
        const url = `${baseurl}${messages}?chatId=${chatId}&page=${page}&pageSize=${pagesize}`
        return Request.get(url)
    }
}

export default new MessageServices()