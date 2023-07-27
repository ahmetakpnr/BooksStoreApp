using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFuture;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")] (for conventions)
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    [ResponseCache(CacheProfileName = "5mins")]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
    public class BooksController : ControllerBase
        {
            private readonly IServiceManager _manager;
            public BooksController(IServiceManager manager)
            {
                _manager = manager;
            }

            [Authorize]
            [HttpHead]
            [HttpGet(Name = "GetAllBooksAsync")]
            [ServiceFilter(typeof(ValidationMediaTypeAttribute))]
            [ResponseCache(Duration = 60)]
            public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
            {      
                    var linkParameters = new LinkParameters()
                    {
                        BookParameters = bookParameters,
                        HttpContext = HttpContext
                    };
                    
                    var result = await _manager
                        .BookServices
                        .GetAllBooksAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", 
                JsonSerializer.Serialize(result.metaData));

                    return result.linkResponse.HasLinks ?
                        Ok(result.linkResponse.LinkedEntities) :
                        Ok(result.linkResponse.ShapedEntites);
            }

            [Authorize]
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetOneBooksAsync([FromRoute(Name = "id")] int id)
            {
            var book = await _manager
            .BookServices
            .GetOneBookByIdAsync(id, false);

             return Ok(book);
            }

            [Authorize(Roles = "Admin, Editor")]
            [ServiceFilter(typeof(LogFilterAttribute))]
            [ServiceFilter(typeof(ValidationFilterAttribute))]
            [HttpPost(Name = "CreateOneBookAsync")]
            public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
            {  
                    var book = await _manager.BookServices.CreateOneBookAsync(bookDto);
                    return StatusCode(201, book); // CreatedAtRoute()
            }
    
            [Authorize(Roles = "Admin, Editor")]
            [ServiceFilter(typeof(ValidationFilterAttribute))]
            [HttpPut("{id:int}")]
            public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id,
                [FromBody] BookDtoForUpdate bookDto)
            {      
                    await _manager.BookServices.UpdateOneBookAsync(id, bookDto, false);
                    return NoContent(); //204 
            }
            
            [Authorize(Roles = "Admin")]
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
            {
                    
                    await _manager.BookServices.DeleteOneBookAsync(id, false);
                    return NoContent();
            }

            [Authorize(Roles = "Admin, Editor")]
            [HttpPatch("{id:int}")]
            public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
                [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
            {

                if(bookPatch is null)
                    return BadRequest(); //400

                var result = await _manager.BookServices.GetOneBookForPatchAsync(id, false);

                bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

                TryValidateModel(result.bookDtoForUpdate);

                if(ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

            await _manager.BookServices.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);
            
                    return NoContent(); //204
            }

            [Authorize]
            [HttpOptions]
            public IActionResult GetBooksOptions()
            {
            Response.Headers.Add("Allow", "GET, PUT, POST, ATCH, DELETE, HEAD, OPTIONS");
            return Ok();
            }
        }
    }

