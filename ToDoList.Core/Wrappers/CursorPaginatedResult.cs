namespace ToDoList.Core.Wrappers
{
    public class CursorPaginatedResult<T>
    {
        public CursorPaginatedResult(bool succeeded, List<T> data, string nextCursor, string previousCursor, List<string> messages = null)
        {
            Succeeded = succeeded;
            Data = data;
            NextCursor = nextCursor;
            PreviousCursor = previousCursor;
            Messages = messages ?? new List<string>();
        }

        public bool Succeeded { get; set; }
        public List<T> Data { get; set; }
        public string NextCursor { get; set; }
        public string PreviousCursor { get; set; }
        public List<string> Messages { get; set; }

        public static CursorPaginatedResult<T> Success(List<T> data, string nextCursor, string previousCursor)
        {
            return new CursorPaginatedResult<T>(true, data, nextCursor, previousCursor);
        }

    }

}
