namespace ATS_Master.Data.Entities
{
    public class PhoneInstallationPreferentialQueue
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}