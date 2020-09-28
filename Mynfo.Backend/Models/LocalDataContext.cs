namespace Mynfo.Backend.Models
{
    using Domain;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Mynfo.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.UserType> UserTypes { get; set; }
    }
}