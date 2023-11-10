class LocalCache{
    setCache(key:string,obj:any){
         var jsonobj=JSON.stringify(obj);
         window.localStorage.setItem(key,jsonobj);
    }
    clear(){
        window.localStorage.clear();
    }
    getCache(key:string){
        var jsonobj= window.localStorage.getItem(key);
        return JSON.parse(jsonobj!);
    }
    delete(key:string){
        window.localStorage.removeItem(key);
    }
}

export default new LocalCache();