namespace ATS_Master.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public ATS Ats { get; set; }

        public Person Person { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public bool IntercityAccess { get; set; }

        public bool PreferentialUser { get; set; }

        public int UserFee { get; set; }

        public int CityBalance { get; set; }

        public int IntercityBalance { get; set; }

        public bool PhoneInstalled { get; set; }
    }
}