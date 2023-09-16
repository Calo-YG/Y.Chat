namespace Y.Chat.EntityCore.EntityFrameCore.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; }
    }

    public interface IEntity<TPrimaryKey> where TPrimaryKey : new()
    {
        public TPrimaryKey Id { get; set; }
    }
}
