using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace ClinSpec
{
    public static class TreeDisplayExtensions
    {
        public static string NodeText(this DataAccess.Domain e)
        {
            return string.Format("{0} [DomainId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Domain e)
        {
            return string.Format("Domain:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.Variable e)
        {
            return string.Format("{0} [VariableId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Variable e)
        {
            return string.Format("Variable:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.Compound e)
        {
            return string.Format("{0} [CompoundId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Compound e)
        {
            return string.Format("Compound:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.Study e)
        {
            return string.Format("{0} [StudyId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Study e)
        {
            return string.Format("Study:{0}", e.Id);
        }
    }
    public partial class WebForm1 : System.Web.UI.Page
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

                study = db.Studies.Include(s => s.Compound).Where(s => s.Id == StudyId).Select(s => s).FirstOrDefault();
                
                    
                    (from s in db.Studies where s.Id == StudyId select s).FirstOrDefault();

                if(study == null)
                    Response.Redirect("Error.aspx?msg=No Study Found with Id " + StudyId.ToString());

            }


            if (!IsPostBack)
                BindTree();
            

        }

        void BindTree()
        {
            treeSelected.Nodes.Clear();
            treeAvailable.Nodes.Clear();

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

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {

                var globalNode = new TreeNode() { Text = "Global", Value = "Global:0" };

                treeAvailable.Nodes.Add(globalNode);

                var studyDomains = db.Domains.Where(d => d.MetaDataVersionId == 1);

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
                            var dNode = new TreeNode() { Text = d.NodeText(), Value = d.NodeId(), ShowCheckBox=true };

                            studyNode.ChildNodes.Add(dNode);

                        }

                    }

                }


            }
        }


        void AddDomain(int StudyId, int SourceDomainId)
        {

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                var sourceDomain = (from d in db.Domains
                                    where d.Id == SourceDomainId
                                    select d).FirstOrDefault();

                //sourceDomain.Dump();

                var study = (from d in db.Studies
                             where d.Id == StudyId
                             select d).FirstOrDefault();

                //study.Dump();

                var clonedDomain =

                db.Domains.AsNoTracking()
                .Include(d => d.Variables.Select(v => v.CodeList))
                .Include(d => d.Variables.Select(v => v.CodeList.CodeListValues))
                .FirstOrDefault(e => e.Id == SourceDomainId);

                clonedDomain.StudyId = StudyId;
                clonedDomain.SourceDomain = sourceDomain;
                clonedDomain.MetaDataVersionId = null;

                db.Domains.Add(clonedDomain);

                db.SaveChanges();
            }
        }


        void RemoveDomain(int StudyId, int SourceDomainId)
        {

            using (DataAccess.SpecToolModelContext db = new DataAccess.SpecToolModelContext())
            {
                var sourceDomain = (from d in db.Domains
                                    where d.Id == SourceDomainId
                                    select d).FirstOrDefault();


                sourceDomain.StudyId = null;
               
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
                    
                    AddDomain(study.Id, domainId);                    
                }
            }

            BindTree();

            
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

            BindTree();
        }
        
    }
}