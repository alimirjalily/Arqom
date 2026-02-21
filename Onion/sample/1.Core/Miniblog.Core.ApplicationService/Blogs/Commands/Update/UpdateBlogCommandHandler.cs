using Arqom.Core.ApplicationServices.Commands;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Utilities;
using MiniBlog.Core.Domain.Blogs.Repositories;
using MiniBlog.Core.RequestResponse.Blogs.Commands.Update;

namespace MiniBlog.Core.ApplicationService.Blogs.Commands.Update;

public sealed class UpdateBlogCommandHandler : CommandHandler<UpdateBlogCommand>
{
    private readonly IBlogCommandRepository _blogCommandRepository;

    public UpdateBlogCommandHandler(ArqomServices ArqomServices,
                                    IBlogCommandRepository blogCommandRepository) : base(ArqomServices)
    {
        _blogCommandRepository = blogCommandRepository;
    }

    public override async Task<CommandResult> Handle(UpdateBlogCommand command)
    {
        var blog = await _blogCommandRepository.GetAsync(command.Id);

        if (blog is null)
            throw new InvalidEntityStateException("بلاگ یافت نشد");

        blog.Update(command.Title, command.Description);

        return Ok();
    }
}
