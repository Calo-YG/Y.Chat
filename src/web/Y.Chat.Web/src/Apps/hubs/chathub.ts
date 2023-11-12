import * as signalR from "@microsoft/signalr";
import config from "../../config";
import * as msgpack from "@microsoft/signalr-protocol-msgpack";
import localStorage from './../../services/localStorage.ts'
class ChatHub {
  private connection: signalR.HubConnection | undefined;

  constructor() {
    this.initHunConnection();
  }

  private initHunConnection() {
    if (!this.connection) {
      const url = config.API + "/chat";
      this.connection = new signalR.HubConnectionBuilder()
        .withAutomaticReconnect()
        .withUrl(url, {
          accessTokenFactory: () => this.getToken(),
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
        })
        .withHubProtocol(new msgpack.MessagePackHubProtocol())
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.connection.keepAliveIntervalInMilliseconds = 5;
      this.connection.on("InitChat",(msg)=>{
        console.info(msg);
      })
    }
  }

  private getToken(): string {
    const obj=localStorage.getCache("user")
    if(!!obj){
      return obj["token"]
    }
    return "";
  }


  public start() {
    this.initHunConnection();
    this.connection?.start().then(() => {
      console.info("signalr启动成功");
    });
    if(this.connection?.state===signalR.HubConnectionState.Disconnected){
      this.connection.onreconnected(() => {
        console.info("signalr重连连接成功");
      })
    }
  }

  public send(messsage:string):void{
    this.initHunConnection();
    this.connection!.send("",messsage)
  }

  public close(){
    this.connection!.onclose((error:Error|undefined)=>{
      console.info(error?.message??"断开连接")
    })
    this.connection!.stop().then(()=>{
      console.info(this.connection?.state)
    });
    
  }
}

export default new ChatHub();
