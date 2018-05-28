using System;

namespace ATS_Master.Data.Entities
{
    public class PhoneHistory
    {
        public int Id { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }

        public int PhoneNumberId { get; set; }

        public int Duration { get; set; }

        public DateTime PhoneDate { get; set; }

        public string Caller { get; set; }

        public string Callee { get; set; }

        public string CallerCity { get; set; }

        public string CalleeCity { get; set; }
    }
}