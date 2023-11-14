import Request from './htttpRequest'

const baseUrl="v1/Notices";
const userNotices='UserNotices';

class NoticeService{

    userNotices(userId:String,type:String){
          const url = baseUrl+userNotices+"?userId="+userId+"&type="+type;
          return Request.get(url);
    }
}

export default new NoticeService();