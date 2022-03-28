using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Helper
{
    public class Paginated<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }



        public Paginated(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool hasNext
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public bool hasPrevious
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public static async Task<Paginated<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paginated<T>(items, count, pageIndex, pageSize);
        }
    }

}
