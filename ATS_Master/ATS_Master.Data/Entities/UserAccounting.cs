﻿namespace ATS_Master.Data.Entities
{
    public class UserAccounting
    {
        public int Id { get; set; }

        public virtual AtsUser AtsUser { get; set; }

        public int AtsUserId { get; set; }

        public int Total { get; set; }
    }
}