namespace ATS_Master.Data.Entities
{
    public class PhoneInstallationQueue
    {
        public int Id { get; set; }

        public virtual AtsUser AtsUser { get; set; }

        public int UserId { get; set; }
    }
}