namespace dotnet_angular_starter;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Todo, GetTodoResponse>();

        CreateMap<CreateTodoRequest, Todo>();
        CreateMap<UpdateTodoRequest, Todo>();
    }
}
