using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.SqlClient;

namespace ClinSpec
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessages.Text = "";

            if (!IsPostBack)
            {
                BindCompounds();
                BindStudy(null);

                
            }
        }

        void BindCompounds()
        {
            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                var comps = (
                    from c in db.Compounds
                    let stds = c.Studies
                    select new { Name = c.Name + " (" + SqlFunctions.StringConvert((double)stds.Count) + " Studies)", c.Id}
                    ).ToList();

                lstComponent.DataSource = comps;
                lstComponent.DataTextField = "Name";
                
                lstComponent.DataValueField = "Id";

                lstComponent.DataBind();

                lstComponent.Items.Insert(0, new ListItem() { Text = "Select a Compound...", Value = "0" });

            }
        }

        void BindStudy(int? CompoundId)
        {
            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                var studies = (
                   from c in db.Studies
                   let stds = c.Domains
                   where (CompoundId == null || c.CompoundId == CompoundId)
                   select new { Name = c.Name + " (" + SqlFunctions.StringConvert((double)stds.Count) + " Domains)", c.Id }
                   ).ToList();



                lstStudy.DataSource = studies;
                lstStudy.DataTextField = "Name";
                lstStudy.DataValueField = "Id";
                lstStudy.DataBind();
                lstStudy.Items.Insert(0, new ListItem() { Text = "Select a study...", Value = "0" });

                
            }
        }

        protected void lstComponent_ServerChange(object sender, EventArgs e)
        {
           

        }

        protected void lstStudy_ServerChange(object sender, EventArgs e)
        {
           

        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                //add component if required

                int? newCompId = null;

                if (!string.IsNullOrWhiteSpace(txtComponent.Text))
                {
                    var newComp = new DataAccess.Compound() { Name = txtComponent.Text.Trim() };

                    db.Compounds.Add(newComp);

                    db.SaveChanges();

                    newCompId = newComp.Id;

                }

                int? newStudyId = null;

                if (!string.IsNullOrWhiteSpace(txtNewStudy.Text))
                {

                    if (newCompId == null && lstComponent.SelectedValue == "0")
                    {
                        lblMessages.Text = "To add a study you must select a Compound or Enter  a new Compound name.";
                        return;
                    }

                    
                    

                    var newStudy = new DataAccess.Study() { Name = txtComponent.Text.Trim() };

                    newStudy.CompoundId = (newCompId == null) ? Convert.ToInt32(lstComponent.SelectedValue) : newCompId.Value;

                    db.Studies.Add(newStudy);

                    db.SaveChanges();

                    newStudyId = newStudy.Id;
                }

                if (newStudyId == null && lstStudy.SelectedValue=="0")
                {
                    lblMessages.Text = "Select or add a study to continue...";
                    return;
 
                }



                Response.Redirect("ManageDomains.aspx?StudyId=" + ((newStudyId == null) ? lstStudy.SelectedValue : newStudyId.Value.ToString()));
              
            }
        }

        protected void lstComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstComponent.SelectedIndex > 0)
            {
                BindStudy(Convert.ToInt32(lstComponent.SelectedValue));
            }

        }

        protected void lstStudy_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        protected void btnAddComponent_ServerClick(object sender, EventArgs e)
        {

        }

       

        protected void btnAddStudy_ServerClick(object sender, EventArgs e)
        {


        }
    }
}