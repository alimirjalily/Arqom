using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;

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

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
