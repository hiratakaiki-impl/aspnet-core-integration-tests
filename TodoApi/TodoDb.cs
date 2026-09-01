using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoApi;

public class TodoDb(DbContextOptions<TodoDb> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}
