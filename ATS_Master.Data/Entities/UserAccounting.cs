namespace ATS_Master.Data.Entities
{
    public class UserAccounting
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int Total { get; set; }
    }
}