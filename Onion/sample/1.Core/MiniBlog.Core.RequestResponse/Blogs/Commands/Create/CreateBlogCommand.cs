using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Create;

public class CreateBlogCommand : ICommand<Guid>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Path => "/api/Blog/Create";
}