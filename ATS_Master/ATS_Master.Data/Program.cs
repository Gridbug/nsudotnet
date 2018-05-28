using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS_Master.Data.Entities;

namespace ATS_Master.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var atsContext = new AtsContext())
            {
//                Person p = new Person()
//                {
//                    Age = 19,
//                    Gender = "M",
//                    Id = 123,
//                    Name = "Aaa",
//                    Middlename = "Bbb",
//                    Surname = "Ccc"
//                };
//
//                atsContext.Persons.Add(p);
//
//                atsContext.SaveChanges();



                var q = from x in atsContext.Persons
                        where x.Name == "Aaa"
                        select new
                        {
                            x.Name,
                            x.Age,
                            x.Gender,
                            x.Id,
                            x.Middlename
                        };


                var data = q.ToArray();

                foreach (var d in data)
                {
                    Console.WriteLine($"Name {d.Name} Age {d.Age} {d.Gender} {d.Id} {d.Middlename}");
                }

                //var ord = atsContext.Persons.First();

                Console.ReadKey();
            }
        }
    }
}
