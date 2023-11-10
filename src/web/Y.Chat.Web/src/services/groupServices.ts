import Request from './htttpRequest'

const baseUrl="v1/Groups";
const usergroup =baseUrl+"/UserGroup";
const sendCode = baseUrl+"/SendCode";

class GroupServices{
   userGroup(userid:string){
    return Request.get(usergroup+"?userId="+userid);
   }
}

export default new GroupServices();