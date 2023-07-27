using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RootsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootsController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.btkakademi.apiroot"))
            {
                var list = new List<Link>()
                {
                    new Link()
                    {
                        hyperReference = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new { }),
                        Relation = "self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        hyperReference = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.GetAllBooksAsync), new { }),
                        Relation = "books",
                        Method = "GET"
                    },
                    new Link()
                    {
                        hyperReference = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.CreateOneBookAsync), new { }),
                        Relation = "books",
                        Method = "POST"
                    },
                };
                return Ok(list);
            }
            return NoContent(); //204
        }
    }
}
