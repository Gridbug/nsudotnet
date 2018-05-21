namespace ATS_Master.Data.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public PhoneType PhoneType { get; set; }

        public virtual Address Address { get; set; }

        public int AddressId { get; set; }

        public bool IsFree { get; set; }
    }

    public enum PhoneType
    {
        Main,
        Parallel,
        Paired
    }
}