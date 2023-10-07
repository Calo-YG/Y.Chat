using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.FileDomain.Shared;

namespace Y.Chat.EntityCore.Domain.FileDomain.Entitis
{
    public class FileSystem : AuditAggregateRoot<Guid, Guid>
    {
        public Guid? ParentId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool Isfolder { get; private set; }

        public Guid? GroupId { get; private set; }

        public ChatGroup ChatGroup { get; private set; }

        public FileType FileType { get; private set; }

        public FileSystem() { }

        public FileSystem(string name,string description,bool isfolder, FileType fileType, Guid? groupId,Guid? parentId) 
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;
            Description = description;
            Isfolder = isfolder;
            ParentId = parentId;
            CreationTime = DateTime.Now;
            FileType = fileType;
        }

    }
}
