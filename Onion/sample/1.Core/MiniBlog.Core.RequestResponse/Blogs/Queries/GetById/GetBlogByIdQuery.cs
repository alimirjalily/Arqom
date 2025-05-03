using Arqom.Core.RequestResponse.Endpoints;
using Arqom.Core.RequestResponse.Queries;

namespace MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;

public class GetBlogByIdQuery : IQuery<BlogQr?>, IWebRequest
{
    public int BlogId { get; set; }

    public string Path => "/api/Blog/GetById";
}