namespace dotnet_angular_starter.Tests;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_angular_starter.Services.TodoService;
using EntityFrameworkCore.Testing.Moq;
using Moq;
using NUnit.Framework;

[TestFixture]
public class TodoServiceTests
{
    private TodoService _todoService;
    private Mock<IMapper> _mapperMock;
    private DataContext _dataContextMock;

    [SetUp]
    public void Setup()
    {
        _dataContextMock = Create.MockedDbContextFor<DataContext>();
        _mapperMock = new Mock<IMapper>();
        _todoService = new TodoService(_mapperMock.Object, _dataContextMock);
    }

    [Test]
    public async Task CreateTodo_ReturnsSuccessfulServiceResponse()
    {
        // Arrange
        var todoRequest = new CreateTodoRequest();
        var mappedTodo = new Todo();
        var getTodoResponse = new GetTodoResponse();
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        _mapperMock.Setup(m => m.Map<Todo>(todoRequest)).Returns(mappedTodo);
        _mapperMock.Setup(m => m.Map<GetTodoResponse>(mappedTodo)).Returns(getTodoResponse);

        // Act
        var result = await _todoService.CreateTodo(todoRequest);

        // Assert
        Assert.That(result.Successful, Is.True);
        Assert.That(result.Data, Is.EqualTo(getTodoResponse));
    }

    [Test]
    public async Task DeleteTodo_WithValidId_RemovesTodoFromDatabaseAndReturnsSuccessfulServiceResponse()
    {
        // Arrange
        var todoId = 1;
        var dbTodo = new Todo { Id = todoId };
        _dataContextMock.Set<Todo>().Add(dbTodo);
        _dataContextMock.SaveChanges();

        // Act
        var result = await _todoService.DeleteTodo(todoId);

        // Assert
        bool isDeleted = _dataContextMock.Todos.FirstOrDefault(t => t.Id == todoId) == null;
        Assert.That(result.Successful, Is.True);
        Assert.That(result.Data, Is.EqualTo($"Todo with Id '{todoId}' has been successfully delete!"));
        Assert.That(isDeleted, Is.True);

    }

    [Test]
    public async Task GetAllTodos_ReturnsListOfGetTodoResponse()
    {
        // Arrange
        var dbTodos = new List<Todo> { new Todo(), new Todo() };
        var getTodoResponses = new List<GetTodoResponse>();
        _dataContextMock.AddRange(dbTodos);
        _dataContextMock.SaveChanges();
        _mapperMock.Setup(m => m.Map<GetTodoResponse>(It.IsAny<Todo>()))
            .Returns((Todo t) => new GetTodoResponse { Id = t.Id });

        // Act
        var result = await _todoService.GetAllTodos();

        // Assert
        Assert.That(result.Successful, Is.True);
        Assert.That(result.Data.Count, Is.EqualTo(dbTodos.Count));
        Assert.That(result.Data.Select(t => t.Id), Is.EquivalentTo(dbTodos.Select(t => t.Id)));
    }

    [Test]
    public async Task GetTodo_WithValidId_ReturnsGetTodoResponse()
    {
        // Arrange
        var todoId = 1;
        var dbTodo = new Todo { Id = todoId };
        var getTodoResponse = new GetTodoResponse();
        _dataContextMock.Add(dbTodo);
        _dataContextMock.SaveChanges();
        _mapperMock.Setup(m => m.Map<GetTodoResponse>(dbTodo)).Returns(getTodoResponse);

        // Act
        var result = await _todoService.GetTodo(todoId);

        // Assert
        Assert.That(result.Successful, Is.True);
        Assert.That(result.Data, Is.EqualTo(getTodoResponse));
    }

    [Test]
    public async Task GetTodo_WithValidTodoId_ReturnsServiceResponseWithData()
    {
        // Arrange
        int todoId = 1;
        var expectedTodo = new Todo { Id = todoId };
        _dataContextMock.Set<Todo>().Add(expectedTodo);
        _dataContextMock.SaveChanges();
        var expectedResponse = new ServiceResponse<GetTodoResponse> { Data = new GetTodoResponse { Id = todoId } };
        _mapperMock.Setup(m => m.Map<GetTodoResponse>(expectedTodo)).Returns(expectedResponse.Data);

        // Act
        var actualResponse = await _todoService.GetTodo(todoId);

        // Assert
        Assert.That(actualResponse.Successful, Is.True);
        Assert.That(actualResponse.Data.Id, Is.EqualTo(expectedResponse.Data.Id));
    }

    [TearDown]
    public void cleanDBContext()
    {
        _dataContextMock.Database.EnsureDeleted();
    }

}
