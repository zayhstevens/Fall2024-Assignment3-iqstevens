using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fall2024_Assignment3_iqstevens.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Fall2024_Assignment3_iqstevens.Models.Movie> Student { get; set; } = default!;

public DbSet<Fall2024_Assignment3_iqstevens.Models.Actor> Course { get; set; } = default!;

public DbSet<Fall2024_Assignment3_iqstevens.Models.MovieActor> CourseStudent { get; set; } = default!;
}

