using System;
using System.Collections.Generic;

namespace HWParts.Core.Infrastructure.Common.Pagination
{
    public class BasePaginationObject
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstPage { get; private set; }
        public int LastPage { get; private set; }
        public int PreviousPage { get; private set; }
        public int NextPage { get; private set; }

        public BasePaginationObject(
            int currentPage,
            int totalPages,
            int firstPage,
            int lastPage,
            int previousPage,
            int nextPage)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            FirstPage = firstPage;
            LastPage = lastPage;
            PreviousPage = previousPage;
            NextPage = nextPage;
        }
    }

    public class PaginationObject<T> : BasePaginationObject
    {
        public List<T> Result { get; private set; }

        public PaginationObject(
            int currentPage,
            int totalPages, 
            int firstPage, 
            int lastPage, 
            int previousPage, 
            int nextPage,
            List<T> result)
            : base(currentPage, totalPages, firstPage, lastPage, previousPage, nextPage)
        {
            Result = result;
        }

        public T ModelObject
        {
            get
            {
                var type = typeof(T).MakeGenericType(typeof(T));
                return (T)Activator.CreateInstance(type);
            }
        }
    }
}
