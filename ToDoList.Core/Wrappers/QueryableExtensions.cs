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

        public static async Task<CursorPaginatedResult<T>> ToCursorPaginatedListAsync<T>(
     this IQueryable<T> source,
     int cursor,
     int pageSize
 )
     where T : class
        {
            if (source == null)
                throw new Exception("Empty source");

            pageSize = pageSize <= 0 ? 10 : pageSize;

            IQueryable<T> query;
            if (cursor == 0)
                query = source.OrderBy(x => EF.Property<int>(x, "Id"));
            else
                query = source.Where(x => EF.Property<int>(x, "Id") > cursor)
                              .OrderBy(x => EF.Property<int>(x, "Id"));

            var items = await query.Take(pageSize + 1).ToListAsync();

            int nextCursor = 0;
            if (items.Count > pageSize)
            {
                var propInfo = typeof(T).GetProperty("Id");
                nextCursor = (int)propInfo.GetValue(items[pageSize]);
                items = items.Take(pageSize).ToList();
            }

            return CursorPaginatedResult<T>.Success(items, nextCursor.ToString(), null);
        }



    }
}
