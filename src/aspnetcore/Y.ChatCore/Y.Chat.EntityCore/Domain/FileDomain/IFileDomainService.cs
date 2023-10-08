using Calo.Blog.Common.Minio;

namespace Y.Chat.EntityCore.Domain.FileDomain
{
    public interface IFileDomainService
    {
        Task UploadMinio(Stream stream, string file, string contentType);
        Task<ObjectOutPut> GetFile(string filename);
    }
}
