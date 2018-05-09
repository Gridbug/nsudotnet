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

        public string ATSType { get; set; }

        public CityATSAttributes CityAtsAttributes { get; set; }

        public DepartmentalATSAttributes DepartmentalAtsAttributes { get; set; }

        public InstitutionalATSAttributes InstitutionalAtsAttributes { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}