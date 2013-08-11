using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;

namespace ClinSpec
{
    public partial class WebForm4 : System.Web.UI.Page
    {

        protected int StudyId;


        DataAccess.Study study = null;

        protected void Page_Load(object sender, EventArgs e)
        {



            if (string.IsNullOrWhiteSpace(Request.QueryString["StudyID"]))
            {
                Response.Redirect("Error.aspx?msg=No Study Select to Manage Domains");
            }

            StudyId = Convert.ToInt32(Request.QueryString["StudyID"]);

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                study = (from s in db.Studies where s.Id == StudyId select s).FirstOrDefault();

                if (study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());

                if (!IsPostBack)
                {
                    string fileName = string.Format("{0}_Specification_{1}.xlsx", study.Name.CleanInvalidFileChars(), DateTime.Now.ToString("MMM-dd-yy-HHmmss"));

                    string fullPath = Server.MapPath("~/Exports/" + fileName);

                    // create file & add conents
                    using (var specDoc = ExcelUtils.CreateWorkbook(fullPath))
                    {

                        ExcelUtils.AddBasicStyles(specDoc);

                        foreach (var dm in study.Domains)
                        {
                            ExcelUtils.AddWorksheet(specDoc, dm.Name);

                            Worksheet sheet = null;

                            foreach (var iSheet in specDoc.WorkbookPart.Workbook.Descendants<Sheet>())
                            {
                                if (iSheet.Name == dm.Name)
                                {
                                    var worksheetPart = (WorksheetPart)specDoc.WorkbookPart.GetPartById(iSheet.Id);
                                    sheet = worksheetPart.Worksheet;
                                    break;
                                }
                            }

                            //write header
                            var headers = new string[] { 
"METADATA_VERSION_NM",
"DOMAIN_NM",
"DOMAIN_DESC",
"DOMAIN_CLASS_CD",
"STRUCTURE_DESC",
"VARIABLE_NM",
"LABEL_TXT",
"DATA_TYPE_CD",
"LENGTH_NUM",
"SIGNIFICANT_DIGITS_NUM",
"CORE_CD",
"ORIGIN_CD",
"MANDATORY_IND",
"ROLE_CD",
"CONTROLLED_TERM_TXT",
"CODED_VALUE",
"DECODED_EN_TXT"};

                            uint headerRow = 1;
                            for (uint i = 0; i < headers.Length; i++)
                            {
                                ExcelUtils.SetCellValue(specDoc, sheet, i + 1, headerRow, headers[i], true, true);
                            }



                            var domainVarData =
                            (
                            from d in db.Domains
                            where d.Id == dm.Id
                            from v in d.Variables
                            join svx in db.StudyDomainVarExclusions
                            on new { VariableId = v.Id, DomainId = v.DomainId, StudyId = (int)d.StudyId } equals new { svx.VariableId, svx.DomainId, svx.StudyId } into g1
                            from svx2 in g1.DefaultIfEmpty()

                            select new
                            {
                                VariableObject = v,
                                DomainId = d.Id,
                                DomainName = d.Name,
                                VaribleName = v.LableText + " (" + v.Name + ")",
                                VariableId = v.Id,
                                IsRequired = (v.Core == DataAccess.VariableCore.Req),
                                CodeListIdInVar = v.CodeListId,
                                IsVarExcluded = svx2 != null ? true : false
                            }
                            ).ToList();


                            var codeListData =
            (
            from d in db.Domains
            where d.Id == dm.Id
            join v in db.Variables on d.Id equals v.DomainId

            join vcl in db.CodeLists on v.CodeListId equals vcl.Id into gCL
            from vcl2 in gCL

            join cvl in db.CodeListValues on v.CodeListId equals cvl.CodeListId into gCLV
            from clv2 in gCLV

            join svx in db.StudyCodeListValueExclusions
            on new { VariableId = v.Id, DomainId = v.DomainId, StudyId = (int)d.StudyId, CodeListValueId = clv2.Id } equals new { svx.VariableId, svx.DomainId, svx.StudyId, svx.CodeListValueId } into g1
            from svx2 in g1.DefaultIfEmpty()
            select new
            {
                DomainId = d.Id,
                DomainName = d.Name,
                VaribleName = v.LableText + " (" + v.Name + ")",
                VariableId = v.Id,
                CodeListIdInVar = v.CodeListId,
                CodeListName = vcl2 != null ? vcl2.Name : null,
                CodeListId = vcl2 != null ? (int?)vcl2.Id : null,
                CodeListValueCode = clv2 != null ? clv2.Name : null,
                CodeListValueDecode = clv2 != null ? clv2.Value : null,
                CodeListValueId = clv2 != null ? (int?)clv2.Id : null,
                IsCLVExcluded = svx2 != null ? true : false
            }
            ).ToList();



                            uint dataRow = 2;
                            foreach (var v in domainVarData)
                            {
                                if (!v.IsVarExcluded)
                                {
                                    uint dataCol = 1;
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, (dm.MetaDataVersion != null) ? dm.MetaDataVersion.Name : "", true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, dm.Name, true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, dm.Description, true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, dm.Class.ToString().Replace("_", " "), true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, dm.StructureDescription, false, true);

                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.Name, true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.LableText, true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.DataType.ToString(), true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, string.Format("{0}", v.VariableObject.Length), true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, string.Format("{0}", v.VariableObject.SignificantDigits), true, true);

                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.Core.ToString(), true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.Origin.ToString(), true, true);

                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.IsRequired ? "Yes" : "No", true, true);
                                    ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, v.VariableObject.Role.ToString(), true, true);

                                    if (v.CodeListIdInVar != null)
                                    {
                                        var clvs = codeListData.Where(clv => clv.DomainId == v.DomainId && clv.VariableId == v.VariableId && clv.CodeListId == v.CodeListIdInVar && clv.IsCLVExcluded==false).ToList();


                                        ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, clvs.First().CodeListName, true, true);
                                        ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, clvs.First().CodeListValueCode, true, true);
                                        ExcelUtils.SetCellValue(specDoc, sheet, dataCol++, dataRow, clvs.First().CodeListValueDecode, true, true);

                                        
                                    }

                                    dataRow++;


                                    


                                }



                            }

                        }

                    }

                    ////set up link & force download
                    exportLink.HRef = "~/Exports/" + fileName;

                    //Response.ContentType = "application/octet-stream";
                    //Response.AppendHeader("Content-Disposition", "filename=" + fileName);
                    //Response.TransmitFile(fullPath);
                    //Response.End();

                    this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name, "window.location.href='/Exports/" + fileName + "'", true);



                }
            }

        }


    }
}