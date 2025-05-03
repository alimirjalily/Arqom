using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.DeleteGraph;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.DeleteGraph;

public sealed class DeleteGraphBlogCommandHandler : CommandHandler<DeleteGraphBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public DeleteGraphBlogCommandHandler(ArqomServices ArqomServices,
                                    IBlogCommandRepository blogCommandRepository) : base(ArqomServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteGraphBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetAsync(command.Id);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.DeleteGraph();

        _blogCommandRepository.DeleteGraph(blog.Id);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}