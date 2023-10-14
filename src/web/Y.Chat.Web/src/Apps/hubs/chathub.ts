import * as signalR from "@microsoft/signalr";
import config from "../../config";
import { useCookies } from "vue3-cookies";
const { cookies } = useCookies();

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
        //.withHubProtocol(new signalR..MessagePackHubProtocol())
        .configureLogging(signalR.LogLevel.Information)
        .build();
      this.connection.keepAliveIntervalInMilliseconds = 300000;
    }
  }

  private getToken(): string {
    const obj = cookies.get("authentication");
    if (!!obj && !!(obj as any)["token"]) {
      return (obj as any)["token"];
    }
    return "";
  }

  private getUserId(): string {
    const obj = cookies.get("authentication");
    if (!!obj && !!(obj as any)["userId"]) {
      return (obj as any)["userId"];
    }
    return "";
  }

  public start() {
    this.initHunConnection();
    this.connection?.start().then(() => {
      console.info("signalr启动成功");
    });
    this.connection?.onreconnected(() => {
      console.info("signalr连接成功");
    });
  }

  public send():void{
    this.initHunConnection();
    connection.send("")
  }
}

export default new ChatHub();
