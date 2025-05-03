using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.DeleteGraph;

public class DeleteGraphBlogCommand : ICommand, IWebRequest
{
    public int Id { get; set; }

    public string Path => "/api/Blog/DeleteGraph";
}