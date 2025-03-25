using APP.BLOG.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APP.BLOG.Features.BlogTags
{
    public class BlogTagUpdateRequest : Request, IRequest<CommandResponse>
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int TagId { get; set; }
    }

    public class BlogTagUpdateHandler : BlogDbHandler, IRequestHandler<BlogTagUpdateRequest, CommandResponse>
    {
        public BlogTagUpdateHandler(BlogDb db) : base(db)
        {
        }
        public async Task<CommandResponse> Handle(BlogTagUpdateRequest request, CancellationToken cancellationToken)
        {
            if(await _db.BlogTags.AnyAsync(x => x.BlogId == request.BlogId && x.TagId == request.TagId, cancellationToken))
            {
                return Error("BlogTag already exists");
            }
            var entity = await _db.BlogTags.FindAsync(request.Id, cancellationToken);
            entity.BlogId = request.BlogId;
            entity.TagId = request.TagId;
            _db.BlogTags.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("BlogTag Updated Successfuly", entity.Id);
        }
    }
}
