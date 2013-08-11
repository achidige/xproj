using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Data;

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

                    Lateral8.Articles.OpenXML.ExcelExport exp = new Lateral8.Articles.OpenXML.ExcelExport();

                    exp.ExportDataTable(study, fullPath);

                    //set up link & force download
                    exportLink.HRef = "~/Exports/" + fileName;
                    this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name, "window.location.href='/Exports/" + fileName + "'", true);
                }
            }
        }
    }
}