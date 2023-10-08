using Calo.Blog.Common.Minio;
using Masa.BuildingBlocks.Ddd.Domain.Services;
using Microsoft.Extensions.Options;

namespace Y.Chat.EntityCore.Domain.FileDomain
{
    public class FileDomainService:DomainService,IFileDomainService
    {
        private readonly YChatContext _context;
        private readonly IMinioService _minioService;
        private readonly MinioConfig _minioOptions;
        public FileDomainService(YChatContext context
            ,IMinioService minioService
            ,IOptions<MinioConfig> minioOptions)
        {
            _context = context;
            _minioService = minioService;
            _minioOptions = minioOptions.Value;
        }
        public async Task UploadMinio(Stream stream,string file,string contentType) 
        {
            var obj = new UploadObjectInput(_minioOptions.DefaultBucket
                ,file
                ,contentType
                ,stream);

            await _minioService.UploadObjectAsync(obj);
        }

        public async Task<ObjectOutPut> GetFile(string filename)
        {
            var obj = new GetObjectInput()
            {
                ObjectName = filename,
                BucketName = _minioOptions.DefaultBucket    
            };

            return await _minioService.GetObjectAsync(obj);
        }
    }
}
