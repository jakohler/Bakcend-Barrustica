using Backend_Barrustica.Models;
using Microsoft.EntityFrameworkCore;

public class BarrusticaDbContext : DbContext
{
    public DbSet<Taller> TallerEntity { get; set; }
    public DbSet<Seminario> SeminarioEntity { get; set; }
    public DbSet<Piece> PieceEntity { get; set; }
    public DbSet<User> UserEntity { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Barrustica.sqlite");
    }
}