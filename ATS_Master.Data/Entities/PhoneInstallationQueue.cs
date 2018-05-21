namespace ATS_Master.Data.Entities
{
    public class PhoneInstallationQueue
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}