using APP.BLOG.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOG.Features.BlogTags
{
    
    public class BlogTagDeleteRequest : Request, IRequest<CommandResponse>
    {
        public int Id { get; set; }
    }
    public class BlogTagDeleteHandler : BlogDbHandler, IRequestHandler<BlogTagDeleteRequest, CommandResponse>
    {
        public BlogTagDeleteHandler(BlogDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(BlogTagDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.BlogTags.FindAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                return Error("BlogTag not found");
            }
            _db.BlogTags.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("BlogTag Deleted Successfuly", entity.Id);
        }
    }
}
