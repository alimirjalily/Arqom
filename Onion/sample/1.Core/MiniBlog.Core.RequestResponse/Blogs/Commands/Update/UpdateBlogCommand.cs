
using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Update;

public class UpdateBlogCommand : ICommand, IWebRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Path => "/api/Blog/Update";
}