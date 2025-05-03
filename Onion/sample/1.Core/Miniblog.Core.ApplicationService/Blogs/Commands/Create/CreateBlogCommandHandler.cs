using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Create;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;

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

        await _blogCommandRepository.CommitAsync();

        return Ok(blog.BusinessId.Value);
    }
}
