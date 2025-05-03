using MiniBlog.Core.Domain.Blogs.Entities;
using Arqom.Core.Contracts.Data.Commands;

namespace MiniBlog.Core.Contracts.Blogs.Commands;

public interface IBlogCommandRepository : ICommandRepository<Blog, int>
{
}
