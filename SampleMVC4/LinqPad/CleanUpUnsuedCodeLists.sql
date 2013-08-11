--drop table #temp

select cl.Name,cl.Id,cl.MetaDataVersionId,count(distinct v.Id) 'cntvars' into #temp
from dbo.CodeLists cl left join dbo.Variables v on cl.Id = v.CodeListId
group by cl.Name,cl.Id,cl.MetaDataVersionId

select * from #temp
where cntvars>0
order by Name
	

-- update 
update v
set v.CodeListId = x.IdToKeep
--select  v.Id,v.Name,v.DomainId,v.CodeListId,x.Name,x.IdToKeep,cl.Id
from Variables v inner join CodeLists cl on v.CodeListId = cl.Id
inner join 
(
select Name,min(Id) IdToKeep From #temp
	where cntvars>0
	group by Name
	union
	select Name,min(Id) IdToKeep From #temp
	where cntvars=0 and Name not in 
	(
	select Name
	From #temp
	where cntvars>0
	group by Name
	)
	group by Name

) x on cl.Name = x.Name
where x.IdToKeep<>cl.Id

-- delete extra code list values

delete clv
from CodeListValues clv inner join CodeLists cl on cl.Id = clv.CodeListId
where cl.id not in 
(
select IdToKeep
from
(
	select Name,min(Id) IdToKeep From #temp
	where cntvars>0
	group by Name
	union
	select Name,min(Id) IdToKeep From #temp
	where cntvars=0 and Name not in 
	(
	select Name
	From #temp
	where cntvars>0
	group by Name
	)
	group by Name
) x
)


-- delete unused code lists

delete cl 
from CodeLists cl 
where cl.id not in 
(
select IdToKeep
from
(
	select Name,min(Id) IdToKeep From #temp
	where cntvars>0
	group by Name
	union
	select Name,min(Id) IdToKeep From #temp
	where cntvars=0 and Name not in 
	(
	select Name
	From #temp
	where cntvars>0
	group by Name
	)
	group by Name
) x
)



select cl.Name,cl.Id,cl.MetaDataVersionId,count(distinct v.Id) 'cntvars' 
from dbo.CodeLists cl left join dbo.Variables v on cl.Id = v.CodeListId
group by cl.Name,cl.Id,cl.MetaDataVersionId


delete from Domains where StudyId is not null
delete from Variables where DomainId in (select Id from Domains where StudyId is not null)
delete from studies
