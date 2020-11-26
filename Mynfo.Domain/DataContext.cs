namespace Mynfo.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public System.Data.Entity.DbSet<Mynfo.Domain.User> Users { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.UserType> UserTypes { get; set; }
        #region Constructor
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Configurations.Add(new Box_ProfileSMMap());
        }
        #endregion

        public System.Data.Entity.DbSet<Mynfo.Domain.Box> Boxes { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfileEmail> ProfileEmails { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfilePhone> ProfilePhones { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfileSM> ProfileSMs { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.RedSocial> RedSocials { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.ProfileWhatsapp> ProfileWhatsapps { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfileEmail> Box_ProfileEmail { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfilePhone> Box_ProfilePhone { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfileSM> Box_ProfileSM { get; set; }

        public System.Data.Entity.DbSet<Mynfo.Domain.Box_ProfileWhatsapp> Box_ProfileWhatsapp { get; set; }
    }
}
