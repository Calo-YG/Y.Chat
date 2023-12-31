import * as signalR from "@microsoft/signalr";
import config from "../../config";
import * as msgpack from "@microsoft/signalr-protocol-msgpack";
import localStorage from './../../services/localStorage'
import { ElNotification } from 'element-plus'
import {chatChangeState} from './../../hooks/chatchange'
import mitt from "./../../utils/mitt";

const store = chatChangeState();
const { updateLastMessae , updateChatListWithDraw,updateNociteCount}=store;
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

      this.connection.keepAliveIntervalInMilliseconds = 15 * 100; // 心跳检测15s

      this.connection.on("ReciveMessage",(groupid,sendUserId,msg,type,messageId)=>{
        const data={
          groupId:groupid,
          sendUserId,
          msg,
          type,
          messageId
        }
        // 接收消息并且更新当前消息列表的消息
        updateLastMessae(groupid,msg,type,sendUserId)
        // 触发接收消息事件
        mitt.emit("ReciveMessage",data)
      })
      //消息限制提示
      this.connection.on("MessageLimit",msg=>{
        ElNotification({
          title: "消息提示",
          message: msg,
          type: "warning",
          position: "bottom-right",
        });
      })
      //处理消息撤回
      this.connection.on('WithDrawMessage',(groupId,userId,messageId)=>{
        updateChatListWithDraw(groupId,messageId)
        mitt.emit("WithDrawMessage",{
          groupId,
          messageId,
          userId
        })
      })
      //处理通知消息
      this.connection.on("Notifiy",type=>{
         updateNociteCount(type)
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

  public send(messsage:string,groupid:string,type:string):void{
    this.initHunConnection();
    this.connection!.send("SendMessage",messsage,groupid,type).catch(error=>{
      console.warn(error.message)
      ElNotification({
        title: "消息提示",
        message: error.message,
        type: "error",
        position: "bottom-right",
      });
    })
  }

  public withDrawMessage(messageId:string,groupid:string){
    this.initHunConnection();
    this.connection!.send("WithDrawMessage",messageId,groupid);
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
