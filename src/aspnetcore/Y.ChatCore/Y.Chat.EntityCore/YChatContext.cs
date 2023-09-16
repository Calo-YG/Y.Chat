using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Y.Chat.EntityCore.EntityFrameCore.Interfaces;

namespace Y.Chat.EntityCore
{
    public class YChatContext:DbContext
    {
        public YChatContext(DbContextOptions<YChatContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 禁用查询跟踪
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
        /// <summary>
        /// 过滤器增加软删除过滤
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureSoftDelete(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //判断是否继承了软删除类
                if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) continue;

                const string isDeleted = nameof(ISoftDelete.IsDelete);
                builder.Entity(entityType.ClrType).Property<bool>(isDeleted);
                var parameter = Expression.Parameter(entityType.ClrType, isDeleted);

                // 添加过滤器
                var body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter,
                        Expression.Constant(isDeleted)),
                    Expression.Constant(false));

                builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }
    }
}
