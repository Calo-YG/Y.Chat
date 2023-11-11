namespace Y.Chat.EntityCore.Domain.FileDomain.Shared
{
    public class UploadGroupFileModel
    {
        public Guid? ParentId {  get; set; }

        public Guid GroupId {  get; set; }

        public Guid UserId { get; set; }
    }
}
