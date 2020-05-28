using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Common.Pagination
{
    public static class PaginationExtension
    {
        public static async Task<PaginationObject<T>> PaginationAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var results = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var count = await query.CountAsync();

            var currentPage = pageNumber;
            var totalPages = (int)Math.Ceiling(decimal.Divide(count, pageSize));
            var firstPage = 1;
            var lastPage = totalPages;
            var prevPage = Math.Max(pageNumber - 1, firstPage);
            var nextPage = Math.Min(pageNumber + 1, lastPage);

            return new PaginationObject<T>(
                currentPage,
                totalPages,
                firstPage,
                lastPage,
                prevPage,
                nextPage,
                results);
        }

        public static PaginationObject<TDestination> Pagination<T, TDestination>(this IQueryable<T> query, IMapper mapper, int? page, int pageSize = 30)
        {
            var pageNumber = (page ?? 1);
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var results = query
                .Skip(skip)
                .Take(take)
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToList();

            var count = query.Count();

            var currentPage = pageNumber;
            var totalPages = (int)Math.Ceiling(decimal.Divide(count, pageSize));
            var firstPage = 1;
            var lastPage = totalPages;
            var prevPage = Math.Max(pageNumber - 1, firstPage);
            var nextPage = Math.Min(pageNumber + 1, lastPage);

            return new PaginationObject<TDestination>(
                currentPage,
                totalPages,
                firstPage,
                lastPage,
                prevPage,
                nextPage,
                results);
        }

        public static IHtmlContent PaginationElement<T>(this IHtmlHelper htmlHelper, PaginationObject<T> paginationObject)
        {
            var element = new StringBuilder()
                .Append("<ul class=\"pagination\">");

            if(paginationObject.TotalPages == 0)
            {
                element
                    .Append("<li class=\"page-item disabled\">")
                    .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                    .Append("Anterior")
                    .Append("</a>")
                    .Append("</li>")
                    .Append("<li class=\"page-item active\">")
                    .Append($"<a class=\"page-link\" href=\"?page={paginationObject.CurrentPage}\">")
                    .Append(paginationObject.CurrentPage)
                    .Append("</a>")
                    .Append("</li>")
                    .Append("<li class=\"page-item disabled\">")
                    .Append($"<a class=\"page-link\" href=\"?page={paginationObject.NextPage}\">")
                    .Append("Próxima")
                    .Append("</a>")
                    .Append("</li>");
            } 
            else
            {
                if (paginationObject.CurrentPage == 1)
                {
                    element
                        .Append("<li class=\"page-item disabled\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                        .Append("Anterior")
                        .Append("</a>")
                        .Append("</li>")
                        .Append("<li class=\"page-item active\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.CurrentPage}\">")
                        .Append(paginationObject.CurrentPage)
                        .Append("</a>")
                        .Append("</li>");

                    if(paginationObject.TotalPages > 1)
                    {
                        element
                            .Append("<li class=\"page-item\">")
                            .Append($"<a class=\"page-link\" href=\"?page={paginationObject.NextPage}\">")
                            .Append(paginationObject.NextPage)
                            .Append("</a>")
                            .Append("</li>");

                        if (paginationObject.TotalPages > 3)
                        {
                            element
                                .Append("<li class=\"page-item\">")
                                .Append($"<a class=\"page-link\" href=\"?page=3\">")
                                .Append(3)
                                .Append("</a>")
                                .Append("</li>");
                        }
                    }
                } 
                else if(paginationObject.CurrentPage != paginationObject.TotalPages)
                {
                    element
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                        .Append("Anterior")
                        .Append("</a>")
                        .Append("</li>");

                    if ((paginationObject.CurrentPage - 1) > 1)
                    {
                        element
                            .Append("<li class=\"page-item\">")
                            .Append($"<a class=\"page-link\" href=\"?page=1\">")
                            .Append(1)
                            .Append("</a>")
                            .Append("</li>")
                            .Append("<li>")
                            .Append("&nbsp;&nbsp;&nbsp;...&nbsp;&nbsp;&nbsp;")
                            .Append("</li>");
                    }

                    element
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                        .Append(paginationObject.PreviousPage)
                        .Append("</a>")
                        .Append("</li>")
                        .Append("<li class=\"page-item active\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.CurrentPage}\">")
                        .Append(paginationObject.CurrentPage)
                        .Append("</a>")
                        .Append("</li>")
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.NextPage}\">")
                        .Append(paginationObject.NextPage)
                        .Append("</a>")
                        .Append("</li>");
                }
                else
                {
                    element
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                        .Append("Anterior")
                        .Append("</a>")
                        .Append("</li>");

                    if ((paginationObject.CurrentPage - 1) > 1)
                    {
                        element
                            .Append("<li class=\"page-item\">")
                            .Append($"<a class=\"page-link\" href=\"?page=1\">")
                            .Append(1)
                            .Append("</a>")
                            .Append("</li>")
                            .Append("<li>")
                            .Append("&nbsp;&nbsp;&nbsp;...&nbsp;&nbsp;&nbsp;")
                            .Append("</li>");
                    }

                    element
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.PreviousPage}\">")
                        .Append(paginationObject.PreviousPage)
                        .Append("</a>")
                        .Append("</li>")
                        .Append("<li class=\"page-item active\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.CurrentPage}\">")
                        .Append(paginationObject.CurrentPage)
                        .Append("</a>")
                        .Append("</li>");
                }

                if (paginationObject.CurrentPage != paginationObject.TotalPages && (paginationObject.CurrentPage + 1) != paginationObject.TotalPages)
                {
                    element
                        .Append("<li>")
                        .Append("&nbsp;&nbsp;&nbsp;...&nbsp;&nbsp;&nbsp;")
                        .Append("</li>")
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.TotalPages}\">")
                        .Append(paginationObject.TotalPages)
                        .Append("</a>")
                        .Append("</li>");
                }

                if(paginationObject.CurrentPage == paginationObject.TotalPages)
                {
                    element
                        .Append("<li class=\"page-item disabled\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.NextPage}\">")
                        .Append("Próxima")
                        .Append("</a>")
                        .Append("</li>");
                }
                else
                {
                    element
                        .Append("<li class=\"page-item\">")
                        .Append($"<a class=\"page-link\" href=\"?page={paginationObject.NextPage}\">")
                        .Append("Próxima")
                        .Append("</a>")
                        .Append("</li>");
                }
            }

            element.Append("</ul>");

            return new HtmlString(element.ToString());
        }
    }
}
