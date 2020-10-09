namespace Mynfo.Backend.Models
{
    using Domain;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Mynfo.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.UserType> UserTypes { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.RedSocial> RedSocials { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfilePhone> ProfilePhones { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfileEmail> ProfileEmails { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box> Boxes { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfileSM> ProfileSMs { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfileSM> Box_ProfileSM { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfileEmail> Box_ProfileEmail { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfilePhone> Box_ProfilePhone { get; set; }
    }
}