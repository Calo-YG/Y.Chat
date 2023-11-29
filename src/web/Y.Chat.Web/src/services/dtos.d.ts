class Register{
    userName: string | undefined;
    password: string | undefined;
    code: string | undefined;
    email: string | undefined;
}

interface FriendGroupListDto{
  id:String|undefined;
  name:String|undefined;
  avatar:String|undefined;
  comment:String|undefined;
  description:String | undefined
}

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
    Register,
    FriendGroupListDto,
    INoticeDto
}