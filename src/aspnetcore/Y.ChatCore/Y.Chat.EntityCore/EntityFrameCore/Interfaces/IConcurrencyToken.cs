using System.ComponentModel.DataAnnotations;

namespace Y.Chat.EntityCore.EntityFrameCore.Interfaces
{
    //处理efcore 并发冲突接口
    public interface IConcurrencyToken
    {
        [ConcurrencyCheck]
        public Guid ConcurrencyToken { get; set; }
    }
}
