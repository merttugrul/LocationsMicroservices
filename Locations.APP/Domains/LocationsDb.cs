using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Domain;

public class LocationsDb : DbContext
{
    public LocationsDb(DbContextOptions<LocationsDb> options) : base(options)
    {
    }
    
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
}