namespace dotnet_angular_starter.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public virtual DbSet<Todo> Todos => Set<Todo>();
}
