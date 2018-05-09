namespace ATS_Master.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string Index { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public int FlatNumber { get; set; }
    }
}