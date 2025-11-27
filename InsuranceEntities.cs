using System.Data.Entity;

namespace InsureeQuoteApp.Models
{
    public class InsuranceEntities : DbContext
    {
        public InsuranceEntities() : base("DefaultConnection")
        {
        }

        public DbSet<Insuree> Insurees { get; set; }
    }
}
