using System;
using System.Collections.Generic;
using System.Linq;
using Events.Data.Contracts;

namespace Events.Data.Helpers
{
    public class PageList<T> : List<T> where T : IEntity
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }

        public PageList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < PageSize; }
        }

        public static PageList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageIndex, pageSize);
        }
    }
}
