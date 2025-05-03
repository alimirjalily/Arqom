using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.RemovePost;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.RemovePost;

public sealed class RemovePostCommandHandler : CommandHandler<RemovePostCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public RemovePostCommandHandler(ArqomServices ArqomServices,
                                    IBlogCommandRepository blogCommandRepository) : base(ArqomServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(RemovePostCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.BlogId);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.RemovePost(command.PostId);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
