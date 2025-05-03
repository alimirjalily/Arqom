using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.AddPost;

public class AddPostCommand : ICommand, IWebRequest
{
    public int BlogId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Path => "/api/Blog/AddPost";
}

