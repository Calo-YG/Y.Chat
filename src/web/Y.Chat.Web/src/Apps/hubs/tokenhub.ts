import * as signalR from "@microsoft/signalr";

class TokenHub {
  url = "http://localhost:5103/token-hub";
  private connection: signalR.HubConnection | undefined;

  constructor() {
     this.initHunConnection();
  }

  private initHunConnection() {
    if (!this.connection) {
        this.url
        this.connection = new signalR.HubConnectionBuilder()
          .withAutomaticReconnect()
          .withUrl(this.url, {
            accessTokenFactory:()=>'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsIklkIjoiMSIsImV4cCI6MTY5ODMwMDI3MiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9leHBpcmF0aW9uIjoiMjAyMy8xMC8yNiAxNDowNDozMiIsIm5iZiI6MTY5ODI5OTY3MiwiaXNzIjoiWS5DYWxvIiwiYXVkIjoiWS5DYWxvIn0.oH73qhMO1mulIVT4GRPZ8epicpwjh00Urg2HsLHYEhY',
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
          })
          //.withHubProtocol(new signalR..MessagePackHubProtocol())
          .configureLogging(signalR.LogLevel.Information)
          .build();
        this.connection.keepAliveIntervalInMilliseconds = 5;
        this.connection.on("GetNewToken",(userid,token)=>{
            console.info("token-hub-id:"+userid);
            console.info("token-hub-token:"+token);
        })
      }
  }

  public start() {
    this.initHunConnection();
    this.connection?.start().then(() => {
      console.info("signalr-token启动成功");
    });
    if(this.connection?.state===signalR.HubConnectionState.Disconnected){
      this.connection.onreconnected(() => {
        console.info("signalr重连连接成功");
      })
    }
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

export {
    TokenHub
}
