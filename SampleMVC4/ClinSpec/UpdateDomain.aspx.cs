using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Text;
using DataAccess;

namespace ClinSpec
{

    public partial class UpdateDomain : System.Web.UI.Page
    {
        protected int StudyId;


        DataAccess.Study study = null;
        protected int DomainId { 
        get
        {
            return Convert.ToInt32((lstDomains.SelectedValue));
        }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessages.Text = "";

            if (string.IsNullOrWhiteSpace(Request.QueryString["StudyID"]))
            {
                Response.Redirect("Error.aspx?msg=No Study Select to Manage Domains");
            }

            StudyId = Convert.ToInt32(Request.QueryString["StudyID"]);

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                study = db.Studies.Include(s => s.Compound).Where(s => s.Id == StudyId).Select(s => s).FirstOrDefault();


                (from s in db.Studies where s.Id == StudyId select s).FirstOrDefault();

                if (study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());

                if (!IsPostBack)
                {
                    foreach (var dm in study.Domains)
                    {
                        lstDomains.Items.Add(new ListItem(string.Format("{0} ({1})", dm.Description, dm.Name), dm.Id.ToString()));                        
                    }

                    if (!string.IsNullOrWhiteSpace(Request.QueryString["DomainId"]))
                    {
                        lstDomains.SelectedValue = Request.QueryString["DomainId"];
                    }

                    lstDomains_SelectedIndexChanged(null, null);
                }
            }

        }


        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
                
            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                var varExclList = (from varx in db.StudyDomainVarExclusions
                                   where varx.DomainId == DomainId && varx.StudyId == StudyId
                                   select varx).ToList();

                var clExclList = (from varx in db.StudyCodeListValueExclusions
                                  where varx.DomainId == DomainId && varx.StudyId == StudyId
                                  select varx).ToList();


                foreach (GridViewRow row in grdDomainData.Rows)
                {

                    bool IsChecked = ((CheckBox)row.FindControl("lblChecked")).Checked;

                    string Rowtype = ((Label)row.FindControl("lblRowtype")).Text;
                    int VariableId = Convert.ToInt32(((Label)row.FindControl("lblVariableId")).Text);
                    int CodeListId = Convert.ToInt32(((Label)row.FindControl("lblCodeListId")).Text);
                    int CodeListValueId = Convert.ToInt32(((Label)row.FindControl("lblCodeListValueId")).Text);

                    bool isRowupdated = false;

                    if (Rowtype == "Variable")
                    {
                        var varFound = varExclList.Find(x => x.VariableId == VariableId);

                        if (varFound != null && !IsChecked)
                        {
                            //remove if already there.
                            db.StudyDomainVarExclusions.Remove(varFound);
                            isRowupdated = true;
                        }
                        else if (varFound == null && IsChecked)
                        {
                            isRowupdated = true;
                            db.StudyDomainVarExclusions.Add(new DataAccess.StudyDomainVarExclusion() { VariableId = VariableId, StudyId = StudyId, DomainId = DomainId });
                        }


                    }
                    else
                    {
                        var clFound = clExclList.Find(x => x.VariableId == VariableId && x.CodeListValueId == CodeListValueId);

                        if (clFound != null && !IsChecked)
                        {
                            isRowupdated = true;
                            //remove if already there and unchecked
                            db.StudyCodeListValueExclusions.Remove(clFound);
                        }
                        else if (clFound == null && IsChecked)
                        {
                            isRowupdated = true;
                            db.StudyCodeListValueExclusions.Add(new DataAccess.StudyCodeListValueExclusion() { VariableId = VariableId, StudyId = StudyId, DomainId = DomainId, CodeListValueId = CodeListValueId });
                        }

                    }

                    if(isRowupdated)
                        sb.AppendFormat("<br/>RowType:{0} VariableId={1} CodeListId={2} CodeListValueId={3}", Rowtype, VariableId, CodeListId, CodeListValueId);

                    //read the label            
                }

                db.SaveChanges();

                lblMsg.InnerHtml = sb.ToString();
            }
        }

        protected void grdDomainData_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void lstDomains_SelectedIndexChanged(object sender, EventArgs e)
        {


            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                // variable data with code list


                var domainVarData =
                (
                from d in db.Domains
                where d.Id == DomainId
                from v in d.Variables
                join svx in db.StudyDomainVarExclusions
                on new { VariableId = v.Id, DomainId = v.DomainId, StudyId = (int)d.StudyId } equals new { svx.VariableId, svx.DomainId, svx.StudyId } into g1
                from svx2 in g1.DefaultIfEmpty()

                select new
                {
                    DomainId = d.Id,
                    DomainName = d.Name,
                    VaribleName = v.Name,
                    VarObject = v,
                    VariableId = v.Id,
                    IsRequired = (v.Core == DataAccess.VariableCore.Req),
                    CodeListIdInVar = v.CodeListId,
                    IsVarExcluded = svx2 != null ? true : false
                }
                ).ToList();


                var codeListData =
(
from d in db.Domains
where d.Id == DomainId
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
    VaribleName = v.Name,
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


                List<GridData> gridData = new List<GridData>();

                foreach (var v in domainVarData)
                {
                    var varRow = new GridData() { RowType = "Variable", VariableId = v.VariableId, Name = v.VaribleName, IsExcluded = v.IsVarExcluded, AllowExclusion = !v.IsRequired, VarObj = v.VarObject };
                    gridData.Add(varRow);

                    if (v.CodeListIdInVar != null)
                    {
                        var clvs = codeListData.Where(clv => clv.DomainId == v.DomainId && clv.VariableId == v.VariableId && clv.CodeListId == v.CodeListIdInVar).ToList();

                        varRow.CodeListName = "Code List Name: " + clvs.First().CodeListName;
                        varRow.CodeListId = clvs.First().CodeListId.Value;

                        foreach (var clv in clvs)
                        {
                            gridData.Add(new GridData() { RowType = "CodeList", VariableId = v.VariableId, VarObj = v.VarObject, CodeListId = clv.CodeListId.Value, CodeListValueId = clv.CodeListValueId.Value, Name = v.VaribleName, IsExcluded = clv.IsCLVExcluded, AllowExclusion = true, CodeListName = clv.CodeListName, CodeListCode = clv.CodeListValueCode, CodeListValue = clv.CodeListValueDecode });

                        }
                    }

                }





                grdDomainData.DataSource = gridData;
                grdDomainData.DataBind();



            }
        }

        protected void grdDomainData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rowcolor = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RowType"));

                string sep = string.IsNullOrEmpty(e.Row.CssClass) ? string.Empty : " ";
                e.Row.CssClass += sep + rowcolor + "-rowstyle";

                if (rowcolor == "CodeList")
                {
                    sep = string.IsNullOrEmpty(e.Row.Cells[0].CssClass) ? string.Empty : " ";
                    e.Row.Cells[0].CssClass += sep + "coldlist-valuecol";

                    sep = string.IsNullOrEmpty(e.Row.Cells[1].CssClass) ? string.Empty : " ";
                    e.Row.Cells[1].CssClass += sep + "coldlist-namecol";

                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                string sep = string.IsNullOrEmpty(e.Row.CssClass) ? string.Empty : " ";
                e.Row.CssClass += sep + "header-rowstyle";

            }
        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {
            btnSave_ServerClick(null, null);
            Response.Redirect("ExportStudy.aspx?StudyId=" + StudyId);
        }

        protected void btnAddNew_ServerClick(object sender, EventArgs e)
        {
            btnSave_ServerClick(null, null);
            
            Response.Redirect(string.Format("AddEditVariable.aspx?Mode=AddNew&StudyId={0}&DomainId={1}",StudyId,DomainId));

        }


    }

    class GridData
    {
        public string RowType { get; set; }

        public int VariableId { get; set; }
        public int CodeListId { get; set; }

        public int CodeListValueId { get; set; }

        public string RowColor { get; set; }

        public string Name { get; set; }

        public string CodeListName { get; set; }
        public string CodeListCode { get; set; }
        public string CodeListValue { get; set; }

        public bool AllowExclusion { get; set; }

        public bool IsExcluded { get; set; }

        public Variable VarObj { get; set; }


    }
}