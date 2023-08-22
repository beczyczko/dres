﻿using Dres.Catwalk.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dres.Catwalk.Database;

public class ResourcesDbContext : DbContext
{
    public ResourcesDbContext(DbContextOptions<ResourcesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Specification> Specifications { get; set; } = null!;
    private DbSet<Resource> Resources { get; set; } = null!;
    private DbSet<Property> Properties { get; set; } = null!;
}