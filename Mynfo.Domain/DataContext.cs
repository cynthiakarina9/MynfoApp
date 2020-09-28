namespace Mynfo.Domain
{
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Mynfo.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.UserType> UserTypes { get; set; }
    }
}
