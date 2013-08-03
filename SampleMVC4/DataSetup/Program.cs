using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataSetup
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new SpecToolModelContext())
            {

                db.MDRs.Add(new MDR() { Name = "CDISC V1.1", Version = "V1.1" });
                db.SaveChanges();


                var query = from mdr in db.MDRs
                            select mdr;

                foreach (var item in query)
                {
                    Console.WriteLine("Id: {0} Name {1} Version {2}", item.Id, item.Name, item.Version);
                }
                
            }


        }
    }
}
