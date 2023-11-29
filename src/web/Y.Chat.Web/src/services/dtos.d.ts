interface INoticeDto{
  id:String|undefined;
  requestUserId:String|undefined;
  requestUserName:String|undefined;
  requestAvatar:String|undefined;
  content:String|undefined;
  recivedId:String|undefined;
  noticeType:Number|undefined;
  agree:boolean;
  read:boolean;
  creationTime:String;
}

export type {
    INoticeDto
}