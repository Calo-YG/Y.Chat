using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Shared;

namespace Y.Chat.EntityCore.Domain.UserDomain.Entities
{
    public class EmailRecords:Entity<Guid>
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Conteent { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }   
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }

        public EmailRecordType RecordType { get; set; }

        public EmailRecords(string conteent, string email, DateTime sendTime, DateTime expiredTime,EmailRecordType recordType)
        {
            Id=IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Conteent = conteent;
            Email = email;
            SendTime = sendTime;
            ExpiredTime = expiredTime;
            RecordType = recordType;
        }   
    }
}
