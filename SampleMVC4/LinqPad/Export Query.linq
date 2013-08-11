<Query Kind="Expression">
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

(
from s in Studies
from d in s.Domains
	let vars = d.Variables
	//let domainStudy = d.Studies
	from v in vars
	let cl = v.CodeList
	let clvList = cl.CodeListValues	
	from clvListItem in cl.CodeListValues	
	where s.Id==2
	//from clv in clvList
	select new { 
	
	//StudyId=s.Id,
	StudyName=s.Name,CompoundName=s.Compound.Name, 
	
	//DomainId=d.Id,	
	IsStandardDomain=d.IsStandard,DomainName=d.Name,DomainDescription=d.Description,DomainCassification= d.Class,DomainCommentText=d.CommentText,
	
	//VariableId=v.Id,
	IsStandardVariable=v.IsStandard, VariablName=v.Name, VariableLavelText=v.LableText,VariableCore=v.Core,VariableOrigin=v.Origin,
	VariableRole=v.Role,VariableDataType=v.DataType,VariableLength=v.Length,VariableSignificantDigits = v.SignificantDigits,VariableCommentText=v.CommentText,
	CodeListName=cl.Name, CodeListEncodedValue=clvListItem.Name,CodeListDecodedValue=clvListItem.Value
	}
	
). Dump()