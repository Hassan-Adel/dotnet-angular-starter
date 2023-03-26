namespace dotnet_angular_starter.Services.TodoService
{
    public class TodoService: ITodoService
    {
        private static readonly Todo todo = new Todo();
        private static readonly List<Todo> todos = new List<Todo>{
            todo,
            new Todo(){Id = 1, Title = "test todo"}
        };

        public Todo CreateTodo(Todo todo)
        {
            todos.Add(todo);
            return todo;
        }

        public void DeleteTodo(int todoId)
        {
            Todo todoToDelete = todos.FirstOrDefault(t => t.Id == todoId);
            todos.Remove(todoToDelete);
        }

        public List<Todo> GetAllTodos()
        {
            return todos;
        }

        public Todo GetTodo(int todoId)
        {
            return todos.FirstOrDefault(t => t.Id == todoId);
        }
    }
}