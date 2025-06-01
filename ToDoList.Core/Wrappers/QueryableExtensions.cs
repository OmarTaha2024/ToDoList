using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<OffsetPaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0) return OffsetPaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return OffsetPaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        public static async Task<CursorPaginatedResult<T>> ToCursorPaginatedListAsync<T, TCursor>(
     this IQueryable<T> source,
     TCursor cursor,
     int pageSize,
     Func<T, TCursor> getIdFunc
 )
     where T : class
     where TCursor : IComparable<TCursor>
        {
            if (source == null)
                throw new Exception("Empty source");

            pageSize = pageSize <= 0 ? 10 : pageSize;

            IQueryable<T> query;
            if (EqualityComparer<TCursor>.Default.Equals(cursor, default))
            {
                query = source.OrderBy(x => getIdFunc(x));
            }
            else
            {
                query = source.Where(x => getIdFunc(x).CompareTo(cursor) > 0)
                              .OrderBy(x => getIdFunc(x));
            }

            var items = await query.Take(pageSize + 1).ToListAsync();

            TCursor nextCursor = default;
            if (items.Count > pageSize)
            {
                nextCursor = getIdFunc(items[pageSize]);
                items = items.Take(pageSize).ToList();
            }


            return CursorPaginatedResult<T>.Success(items, nextCursor?.ToString(), null);
        }


    }
}
