using Microsoft.EntityFrameworkCore;
using TodoApi;

class TodoDb(DbContextOptions<TodoDb> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}
