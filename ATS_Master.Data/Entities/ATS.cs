using System.Collections.Generic;

namespace ATS_Master.Data.Entities
{
    public class ATS
    {
        public ATS()
        {
            PhoneNumbers = new HashSet<PhoneNumber>();
        }

        public int Id { get; set; }

        public ATSType ATSType { get; set; }

        public virtual CityATSAttributes CityAtsAttributes { get; set; }

        public int CityATSAttributesId { get; set; }

        public virtual DepartmentalATSAttributes DepartmentalAtsAttributes { get; set; }

        public int DepartmentalATSAttributesId { get; set; }

        public virtual InstitutionalATSAttributes InstitutionalAtsAttributes { get; set; }

        public int InstitutionalATSAttributesId { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }

    public enum ATSType
    {
        City,
        Departmental,
        Institutional
    }
}