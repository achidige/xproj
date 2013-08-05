<Query Kind="Program">
  <Connection>
    <ID>03d0d5a5-7b77-4e3a-a9b2-d12df92f702b</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPathEncoded>&lt;MyDocuments&gt;\GitHub\xproj\SampleMVC4\DataAccess\bin\Debug\DataAccess.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\Users\achidige\Documents\GitHub\xproj\SampleMVC4\DataAccess\bin\Debug\DataAccess.dll</CustomAssemblyPath>
    <CustomTypeName>DataAccess.SpecToolModelContext</CustomTypeName>
    <AppConfigPath>C:\Users\achidige\Documents\GitHub\xproj\SampleMVC4\DataAccess\App.config</AppConfigPath>
    <DisplayName>SpecToolModelContext</DisplayName>
  </Connection>
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

void Main()
{
//	SetupCompAndStudy();
//
AddDomain(2,2);

(
from d in Domains
	let vars = d.Variables
	//let domainStudy = d.Studies
	from v in vars
	let cl = v.CodeList
	let clvList = cl.CodeListValues	
	let clvListFirst = 	clvList.FirstOrDefault()
	//from clv in clvList
		orderby clvList.Count descending
	select new { DomainId=d.Id,DomainName=d.Name,VariableId=v.Id,VariablName=v.Name,v.CodeListId,clvList.Count, clvListFirstId= ((clvListFirst==null)?0:clvListFirst.Id) }
	
). Dump();	

//
//
//	(from d in Domains
//	let vars = d.Variables
//	where d.StudyId ==null
//	select new { d,vars }
//	).Dump();
//	
//	(from v in Variables
//	orderby v.Id descending
//	select v).Dump();
	
//	(from c in  Compounds
//	select c).Dump();
//	(from s in  Studies
//	select s).Dump();
//
//
//string pDomain = null;
//string pVar = null;
//string pCL = "AESEV";
//
//
//(
//	from s in Studies
//	let stdms = s.Domains
//	from d in stdms
//	let vars = d.Variables
//	from v in vars
//	let cl = v.CodeList
//	let clv = cl.CodeListValues
//	
//	orderby clv.Count descending
//	select new { DomainName=d.Name,VariablName=v.Name,cl.Name,clv.Count,clv } 
//	
//	
//).Dump();
//
//
//(from cl in CodeLists
//where (pCL ==null || cl.Name==pCL)
//select cl).Dump();
//


}



void AddDomain(int StudyId,int SourceDomainId)
{

var sourceDomain  = (from d in Domains 
			where d.Id == SourceDomainId
			select d).FirstOrDefault();

//sourceDomain.Dump();

var study  = (from d in Studies 
			where d.Id == StudyId
			select d).FirstOrDefault();

//study.Dump();

var clonedDomain = 
this.Domains.AsNoTracking()
.Include(d => d.Variables.Select(v=> v.CodeList))
.Include(d => d.Variables.Select(v=> v.CodeList.CodeListValues))
.FirstOrDefault(e => e.Id == SourceDomainId);

clonedDomain.StudyId=StudyId;
clonedDomain.SourceDomain = sourceDomain;
clonedDomain.MetaDataVersionId = null;

this.Domains.Add(clonedDomain);

this.SaveChanges();

}


void SetupCompAndStudy()
{

	var studyAdded = false;
	var compAdded= false;
	//add 5 Compounds
	for(var i=1;i<=4;i++)
	{
		var compName = string.Format("Comp {0}",Convert.ToString(i));
		
		var checkComp = (from c in Compounds
						where c.Name == compName 
						select c).FirstOrDefault();
						
		if(checkComp == null)
		{
			compAdded=true;
			Compounds.Add( new Compound() { Name = compName } );
		}
	}
	
	if(compAdded)
		this.SaveChanges();

//add 3 studies per component

	
	var comps  = from c in  Compounds
	select c;
	
	foreach(var comp in comps)
	{
		for(var i=1;i<=2;i++)
		{
			var studyName = string.Format("{0} Study {1}",comp.Name, Convert.ToString(i));
			
			var checkStudy= (
						from s in Studies
						where  s.Name == studyName && s.CompoundId == comp.Id
						select s).FirstOrDefault();
						
			if(checkStudy == null)
			{
				studyAdded = true;	
				Studies.Add( new Study() { Name = studyName, CompoundId=comp.Id } );
			}
		}
	}
	
	if(studyAdded)	
		this.SaveChanges();

}