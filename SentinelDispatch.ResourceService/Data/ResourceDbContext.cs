using Microsoft.EntityFrameworkCore;

namespace SentinelDispatch.ResourceService.Data;

public class ResourceDbContext : DbContext
{
    public ResourceDbContext(DbContextOptions<ResourceDbContext> options) : base(options)
    {
    }

    public DbSet<Resource> Resources { get; set; }
}
