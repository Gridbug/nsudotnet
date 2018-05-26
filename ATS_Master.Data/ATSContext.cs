using System.Data.Entity;
using ATS_Master.Data.Entities;

namespace ATS_Master.Data
{
    public class ATSContext : DbContext
    {
        public ATSContext() : base(@"Data Source=DESKTOP-QJ987JK;Initial Catalog=ATS_Master;Integrated Security=True")
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ATS> AtsStations { get; set; }

        public DbSet<CityATSAttributes> CityATSAttributes { get; set; }

        public DbSet<DepartmentalATSAttributes> DepartmentalATSAttributes { get; set; }

        public DbSet<InstitutionalATSAttributes> InstitutionalATSAttributes { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PhoneHistory> PhoneHistories { get; set; }

        public DbSet<PhoneInstallationQueue> PhoneInstallationQueues { get; set; }

        public DbSet<PhoneInstallationPreferentialQueue> PhoneInstallationPreferentialQueues { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAccounting> UserAccountings { get; set; }
    }
}