using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Endpoints;

namespace MiniBlog.Core.RequestResponse.Blogs.Commands.Delete;

public class DeleteBlogCommand : ICommand, IWebRequest
{
    public int Id { get; set; }

    public string Path => "/api/Blog/Delete";
}