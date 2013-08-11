<Query Kind="Statements">
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

var DomainId = 43;


var vars =
(
from d in Domains
where d.Id == DomainId
from v in d.Variables
join svx in StudyDomainVarExclusions
on new { VariableId=v.Id, DomainId=v.DomainId,StudyId=(int)d.StudyId} equals new {svx.VariableId,svx.DomainId,svx.StudyId} into g1
from svx2 in g1.DefaultIfEmpty()

select new
{
  DomainId = d.Id,
  DomainName = d.Name,
  VaribleName = v.LableText + " (" + v.Name + ")",
  VariableId = v.Id,
  CodeListIdInVar = v.CodeListId,
  IsVarExcluded = svx2 != null ? true:false

//  
//  CodeListName = vcl2 != null ? vcl2.Name : null,
//  CodeListId = vcl2 != null ? (int?)vcl2.Id : null,
//  CodeListValueCode = clv2 != null ? clv2.Name : null,
//  CodeListValueDecode = clv2 != null ? clv2.Value : null,
//  CodeListValueId = clv2 != null ? (int?)clv2.Id : null

}
);
vars.Dump();