using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_angular_starter.Services.TodoService
{
    public interface ITodoService
    {
        List<Todo> GetAllTodos();
        Todo GetTodo(int todoId);
        Todo CreateTodo(Todo todo);
        void DeleteTodo(int todoId);
    }
}