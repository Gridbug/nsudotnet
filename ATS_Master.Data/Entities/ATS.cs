using System.Collections.Generic;

namespace ATS_Master.Data.Entities
{
    public class Ats
    {
        public Ats()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
        }

        public string GenerateAtsName()
        {
            return AtsType.ToString() + " ATS " + Id.ToString();
        }

        public int Id { get; set; }

        public AtsType AtsType { get; set; }

        public virtual CityAtsAttributes CityAtsAttributes { get; set; }

        public int CityAtsAttributesId { get; set; }

        public virtual DepartmentalAtsAttributes DepartmentalAtsAttributes { get; set; }

        public int DepartmentalAtsAttributesId { get; set; }

        public virtual InstitutionalAtsAttributes InstitutionalAtsAttributes { get; set; }

        public int InstitutionalAtsAttributesId { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }

    public enum AtsType
    {
        City,
        Departmental,
        Institutional
    }
}