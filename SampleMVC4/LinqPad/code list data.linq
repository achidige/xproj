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

var codeLists =
(
from d in Domains
                                  where d.Id == DomainId
                                  join v in Variables on d.Id equals v.DomainId

                                  join vcl in CodeLists on v.CodeListId equals vcl.Id into gCL
                                  from vcl2 in gCL

                                  join cvl in CodeListValues on v.CodeListId equals cvl.CodeListId into gCLV
                                  from clv2 in gCLV
								  
join svx in StudyCodeListValueExclusions
on new { VariableId=v.Id, DomainId=v.DomainId,StudyId=(int)d.StudyId,CodeListValueId=clv2.Id} equals new {svx.VariableId,svx.DomainId,svx.StudyId,svx.CodeListValueId} into g1
from svx2 in g1.DefaultIfEmpty()
                                  select new
                                  {
                                     DomainId = d.Id,
                                     DomainName = d.Name,
                                     VaribleName = v.LableText + " (" + v.Name + ")",
                                     VariableId = v.Id,
                                     CodeListIdInVar = v.CodeListId,
                                     CodeListName = vcl2 != null ? vcl2.Name : null,
                                     CodeListId = vcl2 != null ? (int?)vcl2.Id : null,
                                     CodeListValueCode = clv2 != null ? clv2.Name : null,
                                     CodeListValueDecode = clv2 != null ? clv2.Value : null,
                                     CodeListValueId = clv2 != null ? (int?)clv2.Id : null,
									 IsCLVExcluded = svx2 != null ? true:false


                                  }
								  );
codeLists.Dump();