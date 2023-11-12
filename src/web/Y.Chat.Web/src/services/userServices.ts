import Request from './htttpRequest'

const baseUrl="v1/Users";
const login =baseUrl+"/login";
const sendCode = baseUrl+"/SendCode";

class UserServices{
    login( username:string, password:string){
       const data={
        userName:username,
        password:password
       }

       return Request.post(login,data)
    }

    sendCode(email:string){
       return Request.get(sendCode+"?email="+email);
    }
  
    register(data:any){
        return Request.post(baseUrl,data);
    }
}

export default new UserServices();
