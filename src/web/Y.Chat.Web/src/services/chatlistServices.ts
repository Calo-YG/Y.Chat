import Request from './htttpRequest'

const baseUrl="v1/ChatLists";
const query='/Query?userId=';

class ChatlistService{
    query(userId:string){
        const url = baseUrl+query+userId
        return Request.get(url)
    }
    
    find(chatId:string,userId:string){
        const url = baseUrl+'/Find?chatId='+chatId+'&userId='+userId
        return Request.get(url)
    }
}

export default new ChatlistService()