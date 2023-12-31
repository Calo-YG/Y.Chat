﻿using Masa.BuildingBlocks.Data;
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

        public Conversation ChatGroup { get; private set; }

        public FileType FileType { get; private set; }

        public string MinioName { get; private set; }   

        public FileSystem() { }

        public FileSystem(string name,string description="",bool isfolder=false, FileType fileType=FileType.Avatar, Guid? groupId=null,Guid? parentId = null) 
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;
            Description = description;
            Isfolder = isfolder;
            ParentId = parentId;
            CreationTime = DateTime.Now;
            FileType = fileType;
        }

        public void SetAvatar()
        {
            FileType = FileType.GroupAvatar;
            ParentId = null;
            GroupId = null;
            Isfolder = false;
        }

        public void SetGroupAvatar()
        {
            FileType = FileType.Avatar;
            ParentId = null;
            GroupId = null;
            Isfolder = false;
        }

        public void SetMinioName(string name)
        {
            MinioName = name;
        }
    }
}
