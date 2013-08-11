using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinSpec
{
    public partial class WebForm5 : System.Web.UI.Page
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

            }

        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var db = new SpecToolModelContext())
                {

                    var domainClassification =  (DomainClass)Enum.Parse(typeof(DomainClass),ddlClassification.SelectedValue);

                    var newDomain = new Domain()
                    {
                        Name = txtName.Text.Trim(),
                        Description = txtDomainDescription.Text.Trim(),
                        StructureDescription = txtStructureDescription.Text.Trim(),
                        Class = domainClassification,
                        StudyId=StudyId,
                        IsTemplate=false,
                        IsStandard=false,
                        CommentText=txtComments.Text.Trim()
                    };

                    db.Domains.Add(newDomain);

                    var domainDescriptionSingular =  txtDomainDescription.Text.TrimEnd('s');

                    db.SaveChanges();

                    var tmplDomain = (from d in db.Domains
                                      where d.IsTemplate == true && d.Class == domainClassification
                                      select d
                                      ).First();

                    foreach (var tmplVar in tmplDomain.Variables)
                    {
                        var newVar = new Variable()
                        {
                            Name = tmplVar.BaseName.Replace("{DomainName}",newDomain.Name),
                            BaseName = tmplVar.BaseName.Replace("{DomainName}", newDomain.Name),

                            Core = tmplVar.Core,
                            DataType =tmplVar.DataType,
                            DomainId = newDomain.Id,
                            IsStandard = true,
                            LableText = tmplVar.LableText.Replace("{DomainDescription}",domainDescriptionSingular),
                            Length = tmplVar.Length,
                            Mandatory = tmplVar.Mandatory,
                            Origin = tmplVar.Origin,
                            Role  = tmplVar.Role,
                            SignificantDigits = tmplVar.SignificantDigits,
                            CodeListId = tmplVar.CodeListId
                        };

                        db.Variables.Add(newVar);

                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }

                Response.Redirect("ManageDomains.aspx?StudyId=" + StudyId);
                
            }
        }

        protected void ddlClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlClassification.SelectedValue)
            {
                case "EVENTS":
                    txtStructureDescription.Text = string.Format("One record per {0} per subject", txtDomainDescription.Text.TrimEnd('s'));
                    break;
                case "INTERVENSIONS":
                    txtStructureDescription.Text = string.Format("One record per {0} measurement per time point per visit per subject", txtDomainDescription.Text.TrimEnd('s'));
                    break;
                case "FINDINGS":
                    txtStructureDescription.Text = string.Format("One record per {0} measurement per time point per visit per subject", txtDomainDescription.Text.TrimEnd('s'));;
                    break;
                default:
                    txtStructureDescription.Text = "";
                    break;
            }

        }

        protected void RegularExpressionValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (var db = new SpecToolModelContext())
            {
                args.IsValid = ( (from d in db.Domains
                                  where d.StudyId == StudyId && d.Name == txtName.Text select d).FirstOrDefault()==null);

            }

        }
    }
}