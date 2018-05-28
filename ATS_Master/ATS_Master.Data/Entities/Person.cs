namespace ATS_Master.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Middlename { get; set; }

        public PersonGender Gender { get; set; }

        public int Age { get; set; }
    }

    public enum PersonGender
    {
        Man,
        Woman
    }
}