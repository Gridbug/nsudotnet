namespace ATS_Master.Data.Entities
{
    public class AtsUser
    {
        public int Id { get; set; }

        public virtual Person Person { get; set; }

        public int PersonId { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }

        public int PhoneNumberId { get; set; }

        public bool IntercityAccess { get; set; }

        public bool PreferentialUser { get; set; }

        public int UserFee { get; set; }

        public int CityBalance { get; set; }

        public int IntercityBalance { get; set; }

        public bool PhoneInstalled { get; set; }
    }
}