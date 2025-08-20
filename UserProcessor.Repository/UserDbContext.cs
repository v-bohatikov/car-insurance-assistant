using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using UserProcessor.Domain.Entities.Users;
using UserProcessor.Domain.Entities.Vehicles;
using UserProcessor.Repository.ModelConfigurations;

namespace UserProcessor.Repository;

public class UserDbContext(DbContextOptions<UserDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    public DbSet<UserPassport> UserPassports => Set<UserPassport>();

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all type configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ModelConfigurationsIndicator).Assembly);
    }
}