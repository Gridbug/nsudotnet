namespace ATS_Master.Data.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string PhoneType { get; set; }

        public Address Address { get; set; }

        public bool IsFree { get; set; }
    }
}