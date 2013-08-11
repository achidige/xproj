using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace ClinSpec
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected int StudyId;


        DataAccess.Study study = null;

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

                study = (from s in db.Studies where s.Id == StudyId select s).Include(s=>s.Compound).FirstOrDefault();

                if (study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());

            }

            this.Title = "Select/Create Study...";
           
           // this.Title = "Study: " + study.Name + " - Manage Domains";

            if (!IsPostBack)
                BindTree(true,true);


        }

        void BindTree(bool rebindAvailable,bool reBindSelected)
        {

            if (rebindAvailable)
            {
                treeAvailable.Nodes.Clear();
                using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
                {

                    var globalNode = new TreeNode() { Text = "Global", Value = "Global:0" };

                    treeAvailable.Nodes.Add(globalNode);

                    var studyDomains = db.Domains.Where(d => d.MetaDataVersionId == 1 && d.IsTemplate == false);

                    foreach (var d in studyDomains)
                    {

                        var dNode = new TreeNode() { Text = d.NodeText(), Value = d.NodeId(), ShowCheckBox = true };

                        globalNode.ChildNodes.Add(dNode);

                    }

                    //select all studies with in current study compounds who has at least one domain.
                    var compoundStudies = from s in db.Studies
                                          join c in db.Compounds
                                          on s.CompoundId equals c.Id
                                          join s2 in db.Studies on c.Id equals s2.CompoundId
                                          let dms = s2.Domains
                                          where s.Id == StudyId && s2.Id != StudyId && dms.Count != 0
                                          select s2;


                    if (compoundStudies != null && compoundStudies.Count() > 0)
                    {
                        var compNode = new TreeNode() { Text = study.Compound.NodeText(), Value = study.Compound.NodeId() };
                        treeAvailable.Nodes.Add(compNode);

                        foreach (var s in compoundStudies)
                        {
                            var studyNode = new TreeNode() { Text = s.NodeText(), Value = study.NodeId() };
                            compNode.ChildNodes.Add(studyNode);

                            foreach (var d in s.Domains)
                            {
                                var dNode = new TreeNode() { Text = d.NodeText(), Value = d.NodeId(), ShowCheckBox = true };

                                studyNode.ChildNodes.Add(dNode);

                                //foreach (var v in d.Variables)
                                //{
                                //    var vNode = new TreeNode() { Text = v.NodeText(), Value = v.NodeId(), ShowCheckBox = false };

                                //    dNode.ChildNodes.Add(vNode);

                                //    if (v.CodeList != null)
                                //    {
                                //        foreach (var clValue in v.CodeList.CodeListValues)
                                //        {

                                //            var clValueNode = new TreeNode() { Text = clValue.NodeText(), Value = clValue.NodeId(), ShowCheckBox = false };
                                //            vNode.ChildNodes.Add(clValueNode);
                                //        }                                        
                                //    }
                                //}

                            }
                        }

                    }
                }
            }

            if (reBindSelected)
            {
                treeSelected.Nodes.Clear();

                using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
                {
                    var studyDomains = db.Domains.Where(d => d.StudyId == StudyId);

                    foreach (var d in studyDomains)
                    {

                        var dNode = new TreeNode() { Text = d.NodeText(), Value = d.NodeId(), ShowCheckBox = true };
                        treeSelected.Nodes.Add(dNode);

                    }
                }

                treeSelected.ExpandDepth = 0;
            }

            

            
        }


        string AddDomain(int StudyId, int SourceDomainId)
        {

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                var sourceDomain = (from d in db.Domains
                                    where d.Id == SourceDomainId
                                    select d).FirstOrDefault();

                //sourceDomain.Dump();

                var found = (from d in db.Domains
                             where d.StudyId == StudyId && d.Name == sourceDomain.Name
                             select d
                                 ).FirstOrDefault();

                if(found!=null)
                {
                    return string.Format(Messages.ERROR_DOMAIN_EXISTS,sourceDomain.Name,study.Name);
                }
                    
                

                var clonedDomain = db.Domains.AsNoTracking()
                .Include(d => d.Variables
                    
                    //.Select(v => v.CodeList)
                    )
                .FirstOrDefault(e => e.Id == SourceDomainId);

                clonedDomain.StudyId = StudyId;
                clonedDomain.SourceDomain = sourceDomain;
                clonedDomain.MetaDataVersionId = null;

                db.Domains.Add(clonedDomain);

                db.SaveChanges();
            }

            return Messages.SUCCESS;
        }


        void RemoveDomain(int StudyId, int studyDomainId)
        {

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {


                var dms = (from d in db.Domains
                           where d.Id == studyDomainId && d.StudyId == StudyId
                           select d).ToList();

                (
                        from d in dms
                        let vars = d.Variables
                        from v in vars
                        select v
                    ).ToList().ForEach(v => db.Variables.Remove(v));

                dms.ForEach(d => db.Domains.Remove(d));

                db.SaveChanges();

            }
        }



        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            foreach (TreeNode tn in treeAvailable.CheckedNodes)
            {
                if (tn.Value.StartsWith("Domain:"))
                {
                    int domainId = Convert.ToInt32(tn.Value.Replace("Domain:", ""));

                    string msg = AddDomain(study.Id, domainId);

                    if (msg != Messages.SUCCESS)
                    {
                        lblMessages.Text = msg;
                    }
                }
            }

            BindTree(false,true);

        }

        protected void btnRemove_ServerClick(object sender, EventArgs e)
        {
            foreach (TreeNode tn in treeSelected.CheckedNodes)
            {
                if (tn.Value.StartsWith("Domain:"))
                {
                    int domainId = Convert.ToInt32(tn.Value.Replace("Domain:", ""));

                    RemoveDomain(study.Id, domainId);
                }
            }

            BindTree(false,true);
        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect("UpdateDomain.aspx?StudyID=" + Convert.ToString(StudyId));
        }

        protected void btnAddNewDomain_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("AddNewDomain.aspx?StudyID=" + Convert.ToString(StudyId));

        }

    }
}