using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Entites;
using System.Reflection.Metadata.Ecma335;

namespace MYAcademy_Portfolya.Context
{
    public class PortfolioContex:DbContext
    {
        public DbSet<About>Abouts { get; set; }
        public DbSet<Education>Educations { get; set; }
        public DbSet<Experience>Experiences { get; set; }
        public DbSet<Message>Messges { get; set; }

    }
}
