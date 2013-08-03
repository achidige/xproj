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
</Query>

void Main()
{
//	SetupCompAndStudy();
//	SetupSpeciaications();
//	
//		
//	(from c in  Components
//	select c).Dump();
//	(from s in  Studies
//	select s).Dump();
//	(from spec in Specifications
//	select spec).Dump();		

//SteupFirstSpec();
	
	(from spec in Specifications
	
	let specdms = spec.SpecDomains
	from specdm in specdms
	let d = specdm.Domain
	select new { SpecName =spec.Name,d } ).Dump();							





}

void SteupFirstSpec()
{
	var spec  = (from sp in Specifications
				where sp.Name == "Comp 1 Study 1 Spec 1"
				select sp).FirstOrDefault();
	
	var standardDomains = from mdr in MDRs
						  let dms = mdr.StandardDomains
						  from d in dms
					      select d;
						  
	foreach (var standardDomain in standardDomains)
	{
		var specDomain  = new SpecDomain();
		specDomain.Domain = standardDomain;
		specDomain.Specification  = spec;
		
		SpecDomains.Add(specDomain);
		
	}
	this.SaveChanges();
		
}

void SetupSpeciaications()
{
	for(var iComp = 1; iComp<=4;iComp++)
	{
		for(var iStudy = 1; iStudy<=2;iStudy++)
		{
			for(var iSpec = 1; iSpec<=2;iSpec++)
			{
					var compName = string.Format("Comp {0}",Convert.ToString(iComp));
					var studyName = string.Format("{0} Study {1}",compName, Convert.ToString(iStudy));
					var specName = string.Format("{0} Spec {1}",studyName,Convert.ToString(iSpec));
					
						
		
					var checkSpec = (from sp in Specifications
									where sp.Name == specName 
									select sp).FirstOrDefault();
									
					if(checkSpec == null)
					{
						var spec = new Specification();
						spec.Name = specName;
						spec.Description = "";
						spec.StudyId = (from st in Studies
										where st.Name == studyName
										select st.Id).FirstOrDefault();
								
						Specifications.Add(spec);
						
					}														
			}
		}
	}
	
	this.SaveChanges();
}

void GenerateSpecificationOutput()
{


}

void SetupCompAndStudy()
{

	var studyAdded = false;
	var compAdded= false;
	//add 5 components
	for(var i=1;i<=4;i++)
	{
		var compName = string.Format("Comp {0}",Convert.ToString(i));
		
		var checkComp = (from c in Components
						where c.Name == compName 
						select c).FirstOrDefault();
						
		if(checkComp == null)
		{
			compAdded=true;
			Components.Add( new Component() { Name = compName } );
		}
	}
	
	if(compAdded)
		this.SaveChanges();

//add 3 studies per component

	
	var comps  = from c in  Components
	select c;
	
	foreach(var comp in comps)
	{
		for(var i=1;i<=2;i++)
		{
			var studyName = string.Format("{0} Study {1}",comp.Name, Convert.ToString(i));
			
			var checkStudy= (
						from s in Studies
						where  s.Name == studyName && s.ComponentId == comp.Id
						select s).FirstOrDefault();
						
			if(checkStudy == null)
			{
				studyAdded = true;	
				Studies.Add( new Study() { Name = studyName, ComponentId=comp.Id } );
			}
		}
	}
	
	if(studyAdded)	
		this.SaveChanges();

}

// Define other methods and classes here
