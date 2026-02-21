using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;
using MiniBlog.Core.Domain.Blogs.Repositories;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.AddPost;

public sealed class AddPostCommandHandler : CommandHandler<AddPostCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public AddPostCommandHandler(ArqomServices ArqomServices,
                                 IBlogCommandRepository blogCommandRepository) : base(ArqomServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(AddPostCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.BlogId);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.AddPost(command.Title);

        return Ok();
    }
}
