using MiniBlog.Core.Contracts.Blogs.Queries;
using MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;
using Arqom.Core.ApplicationServices.Queries;
using Arqom.Core.RequestResponse.Queries;
using Arqom.Utilities;

namespace MiniBlog.Core.ApplicationService.Blogs.Queries.GetById;

public class GetBlogByIdQueryHandler : QueryHandler<GetBlogByIdQuery, BlogQr?>
{
    private readonly IBlogQueryRepository _blogQueryRepository;

    public GetBlogByIdQueryHandler(ArqomServices ArqomServices,
                                   IBlogQueryRepository blogQueryRepository) : base(ArqomServices)
    {
        _blogQueryRepository = blogQueryRepository;
    }

    public override async Task<QueryResult<BlogQr?>> Handle(GetBlogByIdQuery query)
    {
        var blog = await _blogQueryRepository.ExecuteAsync(query);

        return Result(blog);
    }
}
