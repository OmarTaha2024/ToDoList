namespace ToDoList.Core.Wrappers
{
    public class OffsetPaginatedResult<T>
    {
        internal OffsetPaginatedResult(bool succeeded, List<T> data, int totalCount, int offset, int limit, List<string> messages = null)
        {
            Succeeded = succeeded;
            Data = data;
            TotalCount = totalCount;
            Offset = offset;
            Limit = limit;
            Messages = messages ?? new List<string>();
        }

        public bool Succeeded { get; set; }
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public List<string> Messages { get; set; }

        public bool HasNext => Offset + Data.Count < TotalCount;
        public bool HasPrevious => Offset > 0;

        public static OffsetPaginatedResult<T> Success(List<T> data, int totalCount, int offset, int limit)
        {
            return new OffsetPaginatedResult<T>(true, data, totalCount, offset, limit);
        }

    }

}
