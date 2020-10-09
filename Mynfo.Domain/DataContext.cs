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
    }
}
