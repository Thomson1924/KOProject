using System.Data.Entity;

namespace AgilityPack
{
    public class ListContext : DbContext
    {
        public DbSet<ListSite> ListSites { get; set; }
        public ListContext()
            : base("DefaultConnection")
        {
            Database.Initialize(false);
        }
    }
}

