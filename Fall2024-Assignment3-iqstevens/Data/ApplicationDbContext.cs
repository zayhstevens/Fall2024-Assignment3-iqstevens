using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fall2024_Assignment3_iqstevens.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Fall2024_Assignment3_iqstevens.Models.Movie> Movie { get; set; } = default!;

public DbSet<Fall2024_Assignment3_iqstevens.Models.Actor> Actor { get; set; } = default!;

public DbSet<Fall2024_Assignment3_iqstevens.Models.MovieActor> MovieActor { get; set; } = default!;
}

