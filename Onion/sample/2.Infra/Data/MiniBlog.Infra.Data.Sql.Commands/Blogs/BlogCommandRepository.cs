using Arqom.Infra.Data.Sql.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.Domain.Blogs.Repositories;
using MiniBlog.Infra.Data.Sql.Commands.Common;

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
