namespace ToDoList.Core.Features.ToDoItem.Queries.Result
{
    public class GetToDoItemoffsetPaginatedListResponse
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public GetToDoItemoffsetPaginatedListResponse(int id, string title, bool iscompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = iscompleted;
        }
    }
}
