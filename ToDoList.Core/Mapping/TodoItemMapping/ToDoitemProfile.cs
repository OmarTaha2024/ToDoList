using AutoMapper;

namespace ToDoList.Core.Mapping.TodoItemMapping
{
    public partial class ToDoitemProfile : Profile
    {
        public ToDoitemProfile()
        {
            AddToDoitemCommandMapping();
            UpdateToDoitemCommandMapping();
            GetTodoListQueryMapping();
            GetTodoitemQueryMapping();
        }
    }
}
