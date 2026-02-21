using Arqom.Core.Domain.Repositories;
using MiniBlog.Core.Domain.Blogs.Entities;

namespace MiniBlog.Core.Domain.Blogs.Repositories;

public interface IBlogCommandRepository : ICommandRepository<Blog, int>
{
}
