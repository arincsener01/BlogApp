using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APP.BLOG.Features.BlogTags;

namespace API.BLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BlogTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/BlogTags
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IQueryable<BlogTagQueryResponse> query = await _mediator.Send(new BlogTagQueryRequest());
            List<BlogTagQueryResponse> list = await query.ToListAsync();
            if (list.Count > 0)
            {
                return Ok(list);
            }
            return NotFound();
        }

        // GET: api/BlogTags/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = await _mediator.Send(new BlogTagQueryRequest());
            var item = await query.SingleOrDefaultAsync(x => x.Id == id);
            if (item is not null)
                return Ok(item);
            return NoContent();
        }

        // POST: api/BlogTags
        public async Task<IActionResult> Post([FromBody] BlogTagCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                ModelState.AddModelError("Error", response.Message);   
            }

            return BadRequest(ModelState);
        }
    }
}
