using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.RemovePost;

public class RemovePostCommand : ICommand, IWebRequest
{
    public int BlogId { get; set; }
    public int PostId { get; set; }

    public string Path => "/api/Blog/RemovePost";
}