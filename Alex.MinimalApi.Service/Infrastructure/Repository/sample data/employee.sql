
--YOU CAN REPEAT QUERY SAFELY
insert employees values('Don','lemon',45,getdate(),getdate())
declare @id int
select @id = max(id) from employees
insert taxfile values ('December Taxfile', @id, getdate(),getdate())
select @id = max(id) from taxfile
insert taxfileRecord values(2001,300,289,@id,getdate(),getdate())
insert taxfileRecord values (2002,365,444, @id,getdate(),getdate())
insert taxfileRecord values (2003,4655,34, @id,getdate(),getdate())
