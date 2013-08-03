using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    class Program
    {

        static DataTable GetInputdate()
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter("select * From [domain_variable_all$]", System.Configuration.ConfigurationManager.ConnectionStrings["SpecToolMDRInput"].ConnectionString);

            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            return dt;

        }

        static void Main(string[] args)
        {

            DataTable dt = GetInputdate();

            writetitle("Numbr of rows:");

            Console.Write(dt.Rows.Count);

            
            //Settting up standard domain schema
            using (var db = new SpecToolModelContext())
            {

                // MDR version
                foreach (DataRow row in dt.Rows)
                {

                    string metadataVersion = row["METADATA_VERSION_ID"].ToString();

                    var qMDRCheck = from item in db.MDRs
                                where item .Name == metadataVersion
                                select item;

                    var mdrCheck = qMDRCheck.FirstOrDefault();

                    if (mdrCheck == null)
                    {
                        writetitle(string.Format("Adding MDR: {0} :",metadataVersion));

                        db.MDRs.Add(new MDR() { Name = metadataVersion, Version = metadataVersion });
                        db.SaveChanges();
                        
                        writetitle("Done!");

                    }

                var mdr = qMDRCheck.FirstOrDefault();


                //add domain 
                string DOMAIN_NM = row["DOMAIN_NM"].ToString();

                var qDomainCheck = from item in db.Domains.OfType<StandardDomain>()
                             where item.Name == DOMAIN_NM && item.MDRId == mdr.Id
                             select item;
                
                var domainCheck = qDomainCheck.FirstOrDefault();

                if (domainCheck == null)
                {

                    writetitle(string.Format("Adding Standard Domain: {0} :", DOMAIN_NM));

                    var standardDomain = new StandardDomain();
                    standardDomain.Name = DOMAIN_NM;
                    standardDomain.MDRId = mdr.Id;
                    
                    standardDomain.Purpose = (Purpose) Enum.Parse(typeof(Purpose), row["PURPOSE_CD"].ToString().Replace(" ", "_"));
                    standardDomain.Classification = (Domaintype) Enum.Parse(typeof(Domaintype), row["DOMAIN_CLASS_CD"].ToString().Replace(" ", "_"));
                    
                    db.Domains.Add(standardDomain);
                    db.SaveChanges();
                    
                    writetitle("Done!");
                }

                var domain = qDomainCheck.FirstOrDefault();

                //add domain 
                string VARIABLE_NM = row["VARIABLE_NM"].ToString();

                var qVarCheck = from item in db.Variables.OfType<StandardVariable>()
                                   where item.Name == VARIABLE_NM && item.MDRId == mdr.Id
                                   select item;

                var varCheck = qVarCheck.FirstOrDefault();


                if (varCheck == null)
                {
                    writetitle(string.Format("Adding Standard Varaible: {0} :", VARIABLE_NM));

                    var standardVariable = new StandardVariable();

                    standardVariable.Name = VARIABLE_NM;

                    standardVariable.MDRId = mdr.Id;

                    standardVariable.Description = row["LABEL_TXT"].ToString();

                    standardVariable.DomainVariablePattern = "";

                    db.Variables.Add(standardVariable);
                    db.SaveChanges();

                    writetitle("Done!");
 
                }

                var var = qVarCheck.FirstOrDefault();

                var qVarDomainCheck = from item in db.DomainVariables
                                      where item.DomainId == domain.Id && item.VariableId == var.Id
                                      select item;

                var varDomainCheck = qVarDomainCheck.FirstOrDefault();

                if (varDomainCheck == null)
                {
                    writetitle(string.Format("Adding Domain Varaible: {0} {1} :", DOMAIN_NM,VARIABLE_NM) );

                    var domainVar = new DomainVariable();

                    domainVar.VariableId = var.Id;
                    domainVar.DomainId = domain.Id;

                    domainVar.Required = row["MANDATORY_IND"].ToString().ToUpper();
                    
                    db.DomainVariables.Add(domainVar);
                    db.SaveChanges();

                    writetitle("Done!");
 
                }
                
                }


                DumpData();

                writeprompt("Ener key to exit");

               
            }

        }
        /*
         * 
                
                //var enums = Enum.GetValues(typeof(Domaintype));
                //var i = 0;
                
                //foreach (Domaintype item in enums)
                //{
                //    Console.WriteLine("{0} {1}", ++i,item.ToString());
                //}

                //var domainTypeString = writeprompt("Enter numeric value of classification of standard domain:");

         */


        static void DumpData()
        {

            using (var db = new SpecToolModelContext())
            {

                var query = from m in db.MDRs
                            select m;

                writetitle("MDRs in the database:");

                foreach (var item in query)
                {
                    Console.WriteLine("Id: {0} | Name: {1} | Version: {2}", item.Id, item.Name, item.Version);
                }


                writetitle("Domains in the database:");


                var q2 = from domain in db.Domains
                         select domain;

                foreach (var item in q2)
                {
                    Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Purpose: {3} | Classification: {4} ", ((item is StandardDomain) ? "StandardDomain" : (item is CustomDomain) ? "CustomeDomain" : "[Error: Unkown Domain]"), item.Id, item.Name, item.Purpose, item.Classification);

                    var qDomVar = from domvar in db.DomainVariables
                    join v in db.Variables on
                    new { vid=domvar.VariableId, did=domvar.DomainId } equals new {vid=v.Id, did=item.Id}
                    select domvar;

                    writetitle(string.Format("{0} Domain Varaible:",item.Name ));

                    foreach (var item2 in qDomVar)
	                {
                               Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Description: {3} | Required: {4} ", ((item2.Variable is StandardVariable) ? "StandardVariable" : (item2.Variable is CustomVariable) ? "CustomVaraible" : "[Error: Unkown Var]"), 
                        item2.Variable.Id, 
                        item2.Variable.Name, 
                        item2.Variable.Description,
                        item2.Required);
                    }
                                 
                }



                writetitle("All Variables in the database:");

                var qVar = from var in db.Variables
                           select var;

                foreach (var item in qVar)
                {
                    Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Description: {3} ", ((item is StandardVariable) ? "StandardVariable" : (item is CustomVariable) ? "CustomVaraible" : "[Error: Unkown Var]"), 
                        item.Id, 
                        item.Name, 
                        item.Description);
                }


            }
        }
        static void writetitle(string s)
        {
            var col = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(s);

            Console.ForegroundColor = col;
        }

        static string writeprompt(string s)
        {
            var col = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(s);

            Console.ForegroundColor = col;

            return Console.ReadLine();
        }

    }
}
