using CORE.APP.Features;
using APP.BLOG.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations; 

namespace APP.BLOG.Features.BlogTags
{
    public class BlogTagCreateRequest : Request, IRequest<CommandResponse>
    { 
        public int BlogId { get; set; }
        public int TagId { get; set; }
    }

    public class BlogTagCreateHandler : BlogDbHandler, IRequestHandler<BlogTagCreateRequest, CommandResponse>
    {
        public BlogTagCreateHandler(BlogDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(BlogTagCreateRequest request, CancellationToken cancellationToken)
        { 
           if(await _db.BlogTags.AnyAsync(x => x.BlogId == request.BlogId && x.TagId == request.TagId))
            {
                return Error("BlogTag already exists");
            }
            var entity = new BlogTag()
            {
                BlogId = request.BlogId,
                TagId = request.TagId
            };
            _db.BlogTags.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("BlogTag Created Successfuly", entity.Id);
        }

            
    }
}
