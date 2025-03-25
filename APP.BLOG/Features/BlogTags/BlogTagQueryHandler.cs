using CORE.APP.Features;
using APP.BLOG.Domain;
using MediatR;


namespace APP.BLOG.Features.BlogTags
{
    public class BlogTagQueryRequest : Request, IRequest<IQueryable<BlogTagQueryResponse>>
    {

    }
    public class BlogTagQueryResponse : QueryResponse
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }
    }
    public class BlogTagQueryHandler : BlogDbHandler, IRequestHandler<BlogTagQueryRequest, IQueryable<BlogTagQueryResponse>>
    {
        public BlogTagQueryHandler(BlogDb db) : base(db)
        {
        }
        public Task<IQueryable<BlogTagQueryResponse>> Handle(BlogTagQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<BlogTagQueryResponse> query = _db.BlogTags.Select(x => new BlogTagQueryResponse
            {
                Id = x.Id,
                BlogId = x.BlogId,
                TagId = x.TagId
            });
            return Task.FromResult(query);
        }
    }
}
