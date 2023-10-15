import { useCookies } from "vue3-cookies";
import { useRouter } from "vue-router";
const { cookies } = useCookies();
const router = useRouter();

class Authorise{
   Token:string | undefined;
   UserId:string|undefined;
   UserName:string|undefined;
   Avatar:string|undefined
}

class Authorization{

    private authorise:Authorise|undefined;

    constructor(){
       this.authorise=this.getAuthorization()
    }

    private getAuthorization():Authorise|undefined{
       const obj=cookies.get("authentication")
       if(!!obj){
        return cookies.get("authentication") as any
       }else{
        router.push("/");
       }
       return undefined
    }

    private getAutication(param:any) {
      const obj = cookies.get("authentication");
      if (!!obj && !!obj[param]) {
        return obj[param];
      }
      return null;
    };

    public getUserId=()=>this.getAutication('userId');
    public getToken=()=>this.getAutication('token')
    public getUserName=()=>this.getAutication('userName');
    public getAvatar=()=>this.getAutication('avatar');
    public getEmail=()=>this.getAutication('email');
}

export default new Authorization()