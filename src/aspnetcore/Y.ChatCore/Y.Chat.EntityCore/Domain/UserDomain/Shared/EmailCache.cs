namespace Y.Chat.EntityCore.Domain.UserDomain.Shared
{
    public class EmailCache
    {
        public string Email { get;private set; }

        public string Code {  get; private set; }   

        public EmailCache(string email,string code) 
        {
            Email = email;
            Code = code;
        }
    }
}
