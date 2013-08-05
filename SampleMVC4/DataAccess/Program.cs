using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace DataAccess
{
    class Program
    {

        const string EXCEL_CONN_STRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
        static string EXEC_PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        static DataTable GetInputdate(string file,string selQuery)
        {

            
            var filePath = System.IO.Path.Combine(EXEC_PATH, file);

            WriteTitle(string.Format("Query: {0} from file: {1}  Located: {2}", selQuery, file, filePath));

            
            var dataAdapoter = new OleDbDataAdapter(selQuery, string.Format(EXCEL_CONN_STRING, filePath));
            DataTable dt = new DataTable();
            dataAdapoter.Fill(dt);

            WriteTitle(string.Format("Read {0} rows.",dt.Rows.Count));
            
            return dt;
        }

        static void Main(string[] args)
        {

            LoadDomainData();

          //  LoadCodeListData();

            WritePrompt("Ener key to exit");
        }

        static void LoadCodeListData()
        {
            DataTable dt = GetInputdate("domain_variable_TestStudy.xls", "select * From [codelist_item_all$] where CODELIST_SOURCE_NM='MDR' and	CODELIST_VERSION_NM='DEV' and CODELIST_SOURCE_DESC='Sanofi MDR Dev instance' and CODELIST_CD<>'' and DECODED_EN_TXT<>'' ");

            using (var db = new SpecToolModelContext())
            {
                var qMDRCheck = from item in db.MetaDataVersions
                                where item.Name == "INTERNAL 425:2:ASSET:0:24271"
                                select item;

                var mdrCheck = qMDRCheck.FirstOrDefault();


                try
                {
                    
                    // MDR version
                    foreach (DataRow row in dt.Rows)
                    {
                        //add domain 
                        string CODELIST_CD = row["CODELIST_CD"].ToString();

                        var qCLCheck = from item in db.CodeLists
                                           where item.Name == CODELIST_CD && item.MetaDataVersionId == mdrCheck.Id
                                           select item;

                        var clCheck = qCLCheck.FirstOrDefault();

                        if (clCheck == null)
                        {
                            WriteTitle(string.Format("Adding Standard CodeList: {0} ", CODELIST_CD));

                            var standardCodeList = new CodeList();

                            standardCodeList.IsStandard = true;
                            standardCodeList.IsTemplate = false;
                            standardCodeList.MetaDataVersionId = mdrCheck.Id;
                            standardCodeList.Name = CODELIST_CD;

                            standardCodeList.DataType = (VariableDataType)Enum.Parse(typeof(VariableDataType), row["DATA_TYPE_CD"].ToString().Replace(" ", "_"));

                            standardCodeList.Description = row["CODELIST_NM"].ToString();
                            standardCodeList.DefinitionText = row["DEFINITION_TXT"].ToString();

                            db.CodeLists.Add(standardCodeList);
                            db.SaveChanges();


                            WriteTitle("Done!");
                        }

                        var cl = qCLCheck.FirstOrDefault();


                        string CODED_VALUE= row["CODED_VALUE"].ToString();

                       var qCLValueCheck = from item in db.CodeListValues
                                           where item.Name == CODED_VALUE && item.CodeListId == cl.Id
                                           select item;

                        var clValueCheck = qCLValueCheck.FirstOrDefault();

                        if (clValueCheck == null)
                        {
                            WriteTitle(string.Format("Adding Standard CodeList Value: {0}  for CodeList:{1}", CODED_VALUE,CODELIST_CD));

                            var standardCodeListValue = new CodeListValues();

                            standardCodeListValue.CodeListId = cl.Id;
                            standardCodeListValue.Name = CODED_VALUE;
                            standardCodeListValue.Value = row["DECODED_EN_TXT"].ToString();

                            db.CodeListValues.Add(standardCodeListValue);
                            
                             db.SaveChanges();


                            WriteTitle("Done!");
                                 
                         }


                        


                    }
                }
                catch (Exception ex)
                {
                    WritException(ex);
                }
            }

           

        }

        static void LoadDomainData()
        {

            DataTable dt = GetInputdate("domain_variable_TestStudy.xls","select * From [domain_variable_all$]");



            //Settting up standard domain schema
            using (var db = new SpecToolModelContext())
            {

                try
                {
                // MDR version
                    foreach (DataRow row in dt.Rows)
                    {

                        string metadataVersion = row["METADATA_VERSION_ID"].ToString();

                        var qMDRCheck = from item in db.MetaDataVersions
                                        where item.Name == metadataVersion
                                        select item;

                        var mdrCheck = qMDRCheck.FirstOrDefault();

                        if (mdrCheck == null)
                        {
                            WriteTitle(string.Format("Adding MDR: {0} :", metadataVersion));

                            db.MetaDataVersions.Add(new MetaDataVersion() { Name = metadataVersion, Version = metadataVersion });

                            db.SaveChanges();

                            WriteTitle("Done!");

                        }

                        var mdr = qMDRCheck.FirstOrDefault();


                        //add domain 
                        string DOMAIN_NM = row["DOMAIN_NM"].ToString();

                        var qDomainCheck = from item in db.Domains
                                           where item.Name == DOMAIN_NM && item.MetaDataVersionId == mdr.Id
                                           select item;

                        var domainCheck = qDomainCheck.FirstOrDefault();

                        if (domainCheck == null)
                        {

                            WriteTitle(string.Format("Adding Standard Domain: {0} :", DOMAIN_NM));

                            var standardDomain = new Domain();
                            standardDomain.Name = DOMAIN_NM;
                            standardDomain.MetaDataVersionId = mdr.Id;
                            standardDomain.Description = row["DOMAIN_DESC"].ToString();
                            standardDomain.IsStandard = true;
                            standardDomain.IsTemplate = false;
                            standardDomain.StructureDescription = row["STRUCTURE_DESC"].ToString();
                            standardDomain.Class = (DomainClass)Enum.Parse(typeof(DomainClass), row["DOMAIN_CLASS_CD"].ToString().Replace(" ", "_"));
                            standardDomain.CommentText = row["DOMAIN_COMMENT_TXT"].ToString();

                            db.Domains.Add(standardDomain);
                            db.SaveChanges();

                            WriteTitle("Done!");
                        }

                        var domain = qDomainCheck.FirstOrDefault();

                        //add domain 
                        string VARIABLE_NM = row["VARIABLE_NM"].ToString();

                        var qVarCheck = from item in db.Variables
                                        where item.Name == VARIABLE_NM &  item.DomainId == domain.Id
                                        select item;

                        var varCheck = qVarCheck.FirstOrDefault();


                        if (varCheck == null)
                        {
                            WriteTitle(string.Format("Adding Standard Varaible: {0} :", VARIABLE_NM));

                            var standardVariable = new Variable();
                            standardVariable.IsStandard = true;
                            standardVariable.IsTemplate = false;
                            standardVariable.Name = VARIABLE_NM;
                            standardVariable.DomainId = domain.Id;
                            standardVariable.BaseName = row["BaseVarName"].ToString();
                            standardVariable.LableText = row["LABEL_TXT"].ToString();
                            standardVariable.DataType = (VariableDataType)Enum.Parse(typeof(VariableDataType), row["DATA_TYPE_CD"].ToString().Replace(" ", "_"));
                            standardVariable.Length = Convert.ToInt32(row["LENGTH_NUM"].ToString());
                            standardVariable.SignificantDigits = ((row["SIGNIFICANT_DIGITS_NUM"].ToString().Equals("")) ? (int?)null : (int?)Convert.ToInt32(row["SIGNIFICANT_DIGITS_NUM"].ToString()));

                            standardVariable.Core = (VariableCore)Enum.Parse(typeof(VariableCore), row["CORE_CD"].ToString().Replace(" ", "_"));
                            standardVariable.Origin = (VariableOrgin)Enum.Parse(typeof(VariableOrgin), row["ORIGIN_CD"].ToString().Replace(" ", "_"));
                            standardVariable.Role = (VariableRole)Enum.Parse(typeof(VariableRole), row["ROLE_CD"].ToString().Replace(" ", "_"));


                            db.Variables.Add(standardVariable);
                            db.SaveChanges();

                            WriteTitle("Done!");

                        }

                        var var = qVarCheck.FirstOrDefault();


                        var codelistName = row["CodeListName"].ToString();


                        Console.WriteLine("Domain {0} Var: {1} Code List {2} ", DOMAIN_NM, VARIABLE_NM, codelistName);


                        
                        if (var.CodeList == null && codelistName != "" && codelistName!="0")
                        {
                            var codeList = ( from cl in db.CodeLists
                                           where cl.Name == codelistName
                                                 select cl).FirstOrDefault();

                            if(codeList != null)
                            {
                                var.CodeList = codeList;

                                db.SaveChanges();
                            }

                        }
                         
                   

                }
                }
                catch (Exception ex)
                {
                    WritException(ex);
                }

                ///DumpData();

            


            }

        }

   

        //static void DumpData()
        //{

        //    using (var db = new SpecToolModelContext())
        //    {

        //        var query = from m in db.MDRs
        //                    select m;

        //        writetitle("MDRs in the database:");

        //        foreach (var item in query)
        //        {
        //            Console.WriteLine("Id: {0} | Name: {1} | Version: {2}", item.Id, item.Name, item.Version);
        //        }


        //        writetitle("Domains in the database:");


        //        var q2 = from domain in db.Domains
        //                 select domain;

        //        foreach (var item in q2)
        //        {
        //            Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Purpose: {3} | Classification: {4} ", ((item is StandardDomain) ? "StandardDomain" : (item is CustomDomain) ? "CustomeDomain" : "[Error: Unkown Domain]"), item.Id, item.Name, item.Purpose, item.Classification);

        //            var qDomVar = from domvar in db.DomainVariables
        //                          join v in db.Variables on
        //                          new { vid = domvar.VariableId, did = domvar.DomainId } equals new { vid = v.Id, did = item.Id }
        //                          select domvar;

        //            writetitle(string.Format("{0} Domain Varaible:", item.Name));

        //            foreach (var item2 in qDomVar)
        //            {
        //                Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Description: {3} | Required: {4} ", ((item2.Variable is StandardVariable) ? "StandardVariable" : (item2.Variable is CustomVariable) ? "CustomVaraible" : "[Error: Unkown Var]"),
        //         item2.Variable.Id,
        //         item2.Variable.Name,
        //         item2.Variable.Description,
        //         item2.Required);
        //            }

        //        }



        //        writetitle("All Variables in the database:");

        //        var qVar = from var in db.Variables
        //                   select var;

        //        foreach (var item in qVar)
        //        {
        //            Console.WriteLine("Type:{0} | Id: {1} | Name: {2} | Description: {3} ", ((item is StandardVariable) ? "StandardVariable" : (item is CustomVariable) ? "CustomVaraible" : "[Error: Unkown Var]"),
        //                item.Id,
        //                item.Name,
        //                item.Description);
        //        }


        //    }
        //}

        static void WriteTitle(string s)
        {
            var col = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(s);

            Console.ForegroundColor = col;
        }


        static void WritException(Exception e)
        {
            var col = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(e);

            Console.ForegroundColor = col;
        }


        static string WritePrompt(string s)
        {
            var col = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(s);

            Console.ForegroundColor = col;

            return Console.ReadLine();
        }

    }
}
