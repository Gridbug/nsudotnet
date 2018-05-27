using System.Data.Entity;
using ATS_Master.Data.Entities;

namespace ATS_Master.Data
{
    public class AtsContext : DbContext
    {
        public AtsContext() : base(@"Data Source=DESKTOP-QJ987JK;Initial Catalog=ATS_Master;Integrated Security=True")
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Ats> AtsStations { get; set; }

        public DbSet<CityAtsAttributes> CityAtsAttributes { get; set; }

        public DbSet<DepartmentalAtsAttributes> DepartmentalAtsAttributes { get; set; }

        public DbSet<InstitutionalAtsAttributes> InstitutionalAtsAttributes { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PhoneHistory> PhoneHistories { get; set; }

        public DbSet<PhoneInstallationQueue> PhoneInstallationQueues { get; set; }

        public DbSet<PhoneInstallationPreferentialQueue> PhoneInstallationPreferentialQueues { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<AtsUser> AtsUsers { get; set; }

        public DbSet<UserAccounting> UserAccountings { get; set; }
    }
}