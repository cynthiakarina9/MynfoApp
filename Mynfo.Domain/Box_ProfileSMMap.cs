using System.Data.Entity.ModelConfiguration;

namespace Mynfo.Domain
{
    internal class Box_ProfileSMMap : EntityTypeConfiguration<object>
    {
        public Box_ProfileSMMap()
        {
            //HasRequired(o => o.Local)
            //    .WithMany(m => m.Locals)
            //    .HasForeignKey(m => m.LocalId);

            //HasRequired(o => o.Visitor)
            //    .WithMany(m => m.Visitors)
            //    .HasForeignKey(m => m.VisitorId);
        }
    }
}