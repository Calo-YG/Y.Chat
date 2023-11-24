import Request from './htttpRequest'

const baseUrl="v1/Groups";
const usergroup =baseUrl+"/UserGroup";
const groupUser = baseUrl+"/GroupUser";

class GroupServices{
   userGroup(userid:string){
    return Request.get(usergroup+"?userId="+userid);
   }
   groupUser(groupId:string){
    return Request.get(groupUser+"?groupId="+groupId);
   }
}

export default new GroupServices();