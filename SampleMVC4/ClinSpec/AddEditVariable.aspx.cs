using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinSpec
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected int StudyId;

        DataAccess.Study study = null;

        protected int DomainId;
        DataAccess.Domain domain = null;


        protected int VariableId;
        DataAccess.Variable variable = null;


        protected bool IsEditMode = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (string.IsNullOrWhiteSpace(Request.QueryString["StudyID"]))
            {
                Response.Redirect("Error.aspx?msg=No Study Selected To Manage Domains");
            }

            StudyId = Convert.ToInt32(Request.QueryString["StudyID"]);

            if (string.IsNullOrWhiteSpace(Request.QueryString["DomainId"]))
            {
                Response.Redirect("Error.aspx?msg=No Domain to Manage Domains");
            }

            DomainId = Convert.ToInt32(Request.QueryString["DomainId"]);

            if (string.IsNullOrWhiteSpace(Request.QueryString["Mode"]))
            {
                IsEditMode = false;
            }
            else
            {
                IsEditMode = Request.QueryString["Mode"].ToString().Equals("Edit");
            }


            if (IsEditMode)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["VariableId"]))
                {
                    Response.Redirect("Error.aspx?msg=No Variable Manage");
                }

                VariableId = Convert.ToInt32(Request.QueryString["VariableId"]);

            }

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                study = (from s in db.Studies where s.Id == StudyId select s).FirstOrDefault();

                if (study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());

                domain = (from s in db.Domains where s.Id == DomainId && s.StudyId == StudyId select s).FirstOrDefault();

                if (study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());


                if (IsEditMode)
                {
                    variable = (from s in db.Variables where s.DomainId == DomainId && s.Id == VariableId select s).FirstOrDefault();

                    if (variable == null)
                        Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());



                }


                //setup dropdowns
                if (!IsPostBack)
                {
                   

                    BindEnumDDL<DataAccess.VariableCore>(ddlCore);
                    BindEnumDDL<DataAccess.VariableOrgin>(ddlOrigin);
                    BindEnumDDL<DataAccess.VariableRole>(ddlRole);
                    BindEnumDDL<DataAccess.VariableDataType>(ddlDataType);


                    ddlCodeLists.DataSource = (from cl in db.CodeLists select cl).ToList();

                    ddlCodeLists.DataValueField = "Id";
                    ddlCodeLists.DataTextField = "Name";
                    ddlCodeLists.DataBind();

                    ddlCodeLists.Items.Insert(0, new ListItem("-- Select A Value --", "0"));
                    
                    if (IsEditMode)
                    {
                        txtName.Text = variable.Name;
                        txtLabelText.Text = variable.LableText;

                        ddlCore.SelectedValue = variable.Core.ToString();
                        ddlOrigin.SelectedValue = variable.Origin.ToString();
                        ddlRole.SelectedValue = variable.Role.ToString();
                        ddlDataType.SelectedValue = variable.DataType.ToString();

                        txtLength.Text = variable.Length.ToString();
                        txtSigDigits.Text = variable.SignificantDigits.ToString();

                        txtComments.Text = variable.CommentText;
                        if (variable.CodeListId != null)
                            ddlCodeLists.SelectedValue = variable.CodeListId.ToString();


                        if (variable.IsStandard)
                        {
                            txtName.ReadOnly = true;
                            txtLabelText.ReadOnly = true;
                            ddlCore.Enabled = false;
                            ddlOrigin.Enabled = false;
                            ddlRole.Enabled = false;
                            ddlDataType.Enabled = false;
                            ddlCodeLists.Enabled = false;
                            
                        }

                        

                    }
                }
            }


        }

        private void BindEnumDDL<EnumType>(DropDownList ddl)
        {
            ddl.Items.Clear();

            var enumVals = Enum.GetValues(typeof(EnumType));

            ddl.Items.Add(new ListItem("-- Select A Value --", "0"));
            foreach (var item in enumVals)
            {
                ddl.Items.Add(new ListItem(item.ToString().Replace("_", " "), item.ToString()));
            }



        }

        private EnumType GetEnumdDDLValue<EnumType>(DropDownList ddl)
        {

            if (ddl.SelectedValue == "0")
                throw new Exception("No Value Selected");

            return (EnumType)Enum.Parse(typeof(EnumType), ddl.SelectedValue);

        }
        
        protected void RegularExpressionValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IsEditMode)
            {
                using (var db = new DataAccess.SpecToolModelContext())
                {
                    args.IsValid = ((from x in db.Variables where x.DomainId == DomainId && x.Name == txtName.Text.Trim() select x).FirstOrDefault() == null);
                } 
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var db = new DataAccess.SpecToolModelContext())
                {
                    if (!IsEditMode)
                    {
                        variable = new DataAccess.Variable();
                        variable.IsStandard = false;
                    }
                    else
                        db.Variables.Attach(variable);


                    variable.Name = txtName.Text.Trim();
                    variable.BaseName = txtName.Text.Trim();
                    variable.LableText = txtLabelText.Text.Trim();

                    variable.Core = GetEnumdDDLValue<DataAccess.VariableCore>(ddlCore);
                    variable.Origin = GetEnumdDDLValue<DataAccess.VariableOrgin>(ddlOrigin);
                    variable.Role = GetEnumdDDLValue<DataAccess.VariableRole>(ddlRole);
                    variable.DataType= GetEnumdDDLValue<DataAccess.VariableDataType>(ddlDataType);

                    if (ddlCodeLists.SelectedValue == "0")
                        variable.CodeListId = null;
                    else
                        variable.CodeListId = Convert.ToInt32(ddlCodeLists.SelectedValue);

                    variable.Length = Convert.ToInt32(txtLength.Text.Trim());

                    if (string.IsNullOrWhiteSpace(txtSigDigits.Text))
                        variable.SignificantDigits = null;
                    else
                        variable.SignificantDigits = Convert.ToInt32(txtSigDigits.Text.Trim());


                    variable.CommentText = txtComments.Text.Trim();

                    if (!IsEditMode)
                    {
                        variable.DomainId = DomainId;
                        
                    }

                   
                        if (!IsEditMode)
                            db.Variables.Add(variable);

                        db.SaveChanges();
                    }

                
            }
        }

        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("UpdateDomain.aspx?StudyID=" + StudyId.ToString());
        }
    }
}