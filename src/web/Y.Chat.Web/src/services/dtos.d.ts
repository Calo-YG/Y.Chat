interface INoticeDto{
  id:String|undefined;
  requestUserId:String|undefined;
  requestUserName:String|undefined;
  requestAvatar:String|undefined;
  content:String|undefined;
  recivedId:String;
  noticeType:Number|undefined;
  agree:boolean;
  read:boolean;
  creationTime:String;
  sendUserId:String;
  reciveUserName:String;
  remark:String|undefined;
  sendAvatar:String;
}

export type {
    INoticeDto
}