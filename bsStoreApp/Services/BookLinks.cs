using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<BookDto> _dataShapaer;
        public BookLinks(LinkGenerator linkGenerator,
            IDataShaper<BookDto> dataShapaer)
        {
            _linkGenerator = linkGenerator;
            _dataShapaer = dataShapaer;
        }



        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> booksDto,
            string fields, 
            HttpContext httpContext)
        {
            var shapedBooks = ShapeData(booksDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(booksDto, fields, httpContext, shapedBooks);
            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> booksDto,
            string fields,
            HttpContext httpContext,
            List<Entity> shapedBooks)
        {
            var bookDtoList = booksDto.ToList();

            for (int index = 0; index < bookDtoList.Count(); index++)
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[index], fields);
                shapedBooks[index].Add("Links", bookLinks);
            }
            var bookCollecitonWrapper = new LinkCollectionWrapper<Entity>(shapedBooks);
            CreateForBooks(httpContext, bookCollecitonWrapper);
            return new LinkResponse { HasLinks = true, LinkedEntities = bookCollecitonWrapper };
        }

        private LinkCollectionWrapper<Entity> CreateForBooks(HttpContext httpContext, 
            LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.Add(new Link() 
            {
                hyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Relation = "self",
                Method = "GET",
            });
            return bookCollectionWrapper;
        }

        private List<Link> CreateForBook(HttpContext httpContext, 
            BookDto bookDto, 
            string fields)
        {
            var links = new List<Link>()
            {
                new Link()
                {
                    hyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                    $"/{bookDto.Id}",
                    Relation = "self",
                    Method = "GET" 
                },
                new Link()
                {
                    hyperReference = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                    Relation = "create",
                    Method = "Post"
                }
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse() { ShapedEntites = shapedBooks };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<BookDto> booksDto, string fields)
        {
            return _dataShapaer
                .ShapeData(booksDto, fields)
                .Select(b => b.Entity) 
                .ToList();
        }
    }
}
