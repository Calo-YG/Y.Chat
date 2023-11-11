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

export type {
    Register,
    FriendGroupListDto
}