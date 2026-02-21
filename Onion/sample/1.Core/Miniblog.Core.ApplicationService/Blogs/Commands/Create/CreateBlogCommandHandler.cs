using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.Domain.Blogs.Repositories;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Create;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Create;

public class CreateBlogCommandHandler : CommandHandler<CreateBlogCommand, Guid>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public CreateBlogCommandHandler(ArqomServices ArqomServices,
                                    IBlogCommandRepository blogCommandRepository) : base(ArqomServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult<Guid>> Handle(CreateBlogCommand command)
    {
        Blog blog = Blog.Create(command.Title, command.Description);

        await _blogCommandRepository.InsertAsync(blog);
        return Ok(blog.BusinessId.Value);
    }
}
