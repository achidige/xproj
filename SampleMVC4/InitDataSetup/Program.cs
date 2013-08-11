using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using DataAccess;

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
            SetupMDRRecord();

            LoadCodeListData();

            LoadDomainData();

            StandardTemplates();
            

            WritePrompt("Ener key to exit");
        }
        static string MDRVERSIONID = "INTERNAL 425:2:ASSET:0:24271";

        static void SetupMDRRecord()
        {
            using(var db = new DataAccess.SpecToolModelContext())
            {

                
                var qMDRCheck = from item in db.MetaDataVersions
                                where item.Name == MDRVERSIONID
                                select item;

                var mdrCheck = qMDRCheck.FirstOrDefault();

                if (mdrCheck == null)
                {
                    WriteTitle(string.Format("Adding MDR: {0} :", MDRVERSIONID));

                    db.MetaDataVersions.Add(new MetaDataVersion() { Name = MDRVERSIONID, Version = MDRVERSIONID });

                    db.SaveChanges();

                    WriteTitle("Done!");

                }
            }
        }
        static void LoadCodeListData()
        {
            DataTable dt = GetInputdate("domain_variable_TestStudy.xls", "select * From [codelist_item_all$] where CODELIST_SOURCE_NM='MDR' and	CODELIST_VERSION_NM='DEV' and CODELIST_SOURCE_DESC='Sanofi MDR Dev instance' and CODELIST_CD<>'' and DECODED_EN_TXT<>'' ");

            using (var db = new SpecToolModelContext())
            {
                var qMDRCheck = from item in db.MetaDataVersions
                                where item.Name == MDRVERSIONID
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

        static void StandardTemplates()
        {
            DataTable dt = GetInputdate("stditems.xls", "select * from [STDITEMS$] where [Class]<>''");
            using (var db = new SpecToolModelContext())
            {
                foreach (DataRow row in dt.Rows)
                {
                    string domainClass = row["CLASS"].ToString();
                    
                    string templateDomainNameAbbr = domainClass +"-DomainAbbr";
                    string templateDomainName = domainClass +"-DomainName";

                    var dmCheck = (from dm in db.Domains
                               where dm.IsTemplate == true && dm.Name == templateDomainNameAbbr
                              && dm.MetaDataVersionId==1
                              select dm
                              );

                    var dm1 = dmCheck.FirstOrDefault();

                    if (dm1 == null)
                    {
                        db.Domains.Add(new Domain()
                        {
                             Class = (DomainClass)Enum.Parse(typeof(DomainClass),domainClass.ToUpper()),
                             Name = templateDomainNameAbbr,
                             IsTemplate=true,
                             Description = templateDomainName,
                             IsStandard=true,
                             MetaDataVersionId=1,
                             StructureDescription=row["DomainStructures"].ToString(),
                             CommentText=""
                        });

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex) 
                        {
                            Console.Write(ex);
                                             
                        }
                    }

                    var dm2 = dmCheck.FirstOrDefault();


                    try
                    {
                        dm2.Variables.Add(new Variable()
                        {
                            Name = (row["AddDomain"].ToString() == "Yes") ? "{DomainName}" + row["VARIABLENAMEGENERAL"].ToString() : row["VARIABLENAMEGENERAL"].ToString(),
                            BaseName = (row["AddDomain"].ToString() == "Yes") ? "{DomainName}" + row["VARIABLENAMEGENERAL"].ToString() : row["VARIABLENAMEGENERAL"].ToString(),
                            LableText = row["LABEL"].ToString(),
                            DataType = (VariableDataType)Enum.Parse(typeof(VariableDataType), row["DATATYPE"].ToString()),
                            Role = (VariableRole)Enum.Parse(typeof(VariableRole), row["Role"].ToString().Replace(" ","_")),
                            Core = (VariableCore)Enum.Parse(typeof(VariableCore), row["Core"].ToString()),
                            Length = Convert.ToInt32(row["STDLEN"]),
                            IsStandard = true,
                            IsTemplate = true,
                            SignificantDigits = Convert.ToInt32(row["FIXLEN"].ToString()),

                            //origin value
                            Origin = VariableOrgin.CRF
                        });

                    }
                    catch (Exception ex)
                    {

                        Console.Write(ex);
                    }

                  


                
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);

                }
            }
        }
   

    

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
