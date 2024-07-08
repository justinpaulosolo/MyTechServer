using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTech.Domain;

namespace MyTech.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Collection> Collections { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<CollectionItem> CollectionItems { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CollectionItem>()
            .HasKey(ci => new { ci.CollectionId, ci.ItemId });
        
        modelBuilder.Entity<CollectionItem>()
            .HasOne(ci => ci.Collection)
            .WithMany(c => c.CollectionItems)
            .HasForeignKey(ci => ci.CollectionId);
        
        modelBuilder.Entity<CollectionItem>()
            .HasOne(ci => ci.Item)
            .WithMany(i => i.CollectionItems)
            .HasForeignKey(ci => ci.ItemId);
    }
    
    public override int SaveChanges()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);
        
        foreach (var entity in modifiedEntries)
        {
            switch (entity.Entity)
            {
                case Collection collection:
                    collection.ModifiedAt = DateTime.UtcNow;
                    break;
                case Item item:
                    item.ModifiedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in modifiedEntries)
        {
            switch (entity.Entity)
            {
                case Collection collection:
                    collection.ModifiedAt = DateTime.UtcNow;
                    break;
                case Item item:
                    item.ModifiedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}