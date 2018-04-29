create trigger tr_WorkHoursDelete on WorkHours for delete
as
begin
	insert into WorkHoursLogs(OldEmployeeID, OldDate, OldTask, OldHours, OldComments, Command)
	select d.EmployeeID, d.[Date], d.Task, d.[Hours], d.Comments, 'delete'
	from deleted d
end

go

create trigger tr_WorkHoursDelete on WorkHours for insert
as
begin
	insert into WorkHoursLogs(NewEmployeeID, NewDate, NewTask, NewHours, NewComments, Command)
	select i.EmployeeID, i.[Date], i.Task, i.[Hours], i.Comments, 'insert'
	from inserted i
end

go

create trigger tr_WorkHoursDelete on WorkHours for update
as
begin
	insert into WorkHoursLogs(OldEmployeeID, NewEmployeeID, OldDate, NewDate, OldTask, NewTask, OldHours, NewHours, OldComments, NewComments, Command)
	select d.EmployeeID, i.EmployeeID, d.[Date], i.[Date], d.Task, i.Task, d.[Hours], i.[Hours], d.Comments, i.Comments, 'update'
	from inserted i, deleted d
end