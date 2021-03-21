using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOProject
{
    class ListSiteContext :DbContext
    {
        public DbSet<ListSite> ListSites { get; set; }
    }
}
