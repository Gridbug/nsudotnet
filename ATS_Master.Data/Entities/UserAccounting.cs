namespace ATS_Master.Data.Entities
{
    public class UserAccounting
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public int Total { get; set; }
    }
}