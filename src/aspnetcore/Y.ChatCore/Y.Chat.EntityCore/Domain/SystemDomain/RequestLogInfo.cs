using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;

namespace Y.Chat.EntityCore.Domain.SystemDomain
{
    public class RequestLogInfo:Entity<Guid>
    {
        public string Method { get;private set; }

        public string? Ip { get;private set; }

        public string? Param { get;private set; }

        public string Duration { get;private set; }

        public bool Success { get;private set; }

        public string? Exception { get;private set; }

        public string Path { get;private set; }

        public DateTime CreationDate { get;private set; }

        public Guid? RequestUserId { get;private set; }

        public RequestLogInfo(string method, string? ip, string? param, string duration,string path, Guid? requestUserId)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Method = method;
            Ip = ip;
            Param = param;
            Duration = duration;
            Success = true;
            Path = path;
            RequestUserId = requestUserId;
            CreationDate = DateTime.Now;
        }
        /// <summary>
        /// 设置异常信息
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="UserFriendlyException"></exception>
        public void SetException(string exception)
        {
            if(exception is null)
            {
                throw new UserFriendlyException("异常信息不能为空");
            }
            Success = false;
            Exception = exception;
        }

    }
}
