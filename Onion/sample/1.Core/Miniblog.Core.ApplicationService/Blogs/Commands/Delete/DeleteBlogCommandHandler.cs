using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;
using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Contracts.Data.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Delete;

public sealed class DeleteBlogCommandHandler(ArqomServices ArqomServices,
                                IBlogCommandRepository blogCommandRepository,
                                IUnitOfWork blogUnitOfWork) : CommandHandler<DeleteBlogCommand>(ArqomServices)
{
    private readonly IUnitOfWork _blogUnitOfWork = blogUnitOfWork;
    private readonly IBlogCommandRepository _blogCommandRepository = blogCommandRepository;

    public override async Task<CommandResult> Handle(DeleteBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetGraphAsync(command.Id) ?? throw new InvalidEntityStateException("بلاگ یافت نشد");
        
        blog.Delete();

        _blogCommandRepository.Delete(blog);

        await _blogCommandRepository.CommitAsync();

        return Ok();
    }
}
