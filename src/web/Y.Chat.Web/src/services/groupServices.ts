import Request from './htttpRequest'

const baseUrl="v1/Groups";
const usergroup =baseUrl+"/UserGroup";
const groupUser = baseUrl+"/GroupUser";

class GroupServices{
   userGroup(userid:string){
    return Request.get(usergroup+"?userId="+userid);
   }
   groupUser(groupId:string,type:string){
    const url = groupUser+"?groupId="+groupId+"&type="+type;
    return Request.get(url);
   }
}

export default new GroupServices();