using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using Arqom.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.Blogs
{
    public class BlogCommandRepository :
        BaseCommandRepository<Blog, MiniblogCommandDbContext, int>,
        IBlogCommandRepository
    {
        public BlogCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
