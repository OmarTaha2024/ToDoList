﻿using ToDoList.Core.Features.ToDoItem.Queries.Result;
using ToDoList.Data.Entities;

namespace ToDoList.Core.Mapping.TodoItemMapping
{
    public partial class ToDoitemProfile
    {
        public void GetTodoitemQueryMapping() =>
       CreateMap<ToDoItem, GetToDoItemResponse>();
    }
}
