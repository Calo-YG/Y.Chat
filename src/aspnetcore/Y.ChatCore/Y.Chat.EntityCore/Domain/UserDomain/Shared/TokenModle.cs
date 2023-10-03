using Calo.Blog.Common.Authorization;

namespace Y.Chat.EntityCore.Domain.UserDomain.Shared
{
    public class TokenModle:UserTokenModel
    {
        public new Guid UserId {  get; set; }
    }
}
