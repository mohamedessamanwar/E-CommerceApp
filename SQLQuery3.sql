if exists(select * from sys.tables where name = 'Products')
begin 
select 'error' ; 
end
------------------------------------------------------------------
begin TRY 
create Table Products (
id int primary Key 
)
end Try 
Begin Catch 
select @@ERROR
end catch
---------------------------------------------------------------------
alter  proc AllP (@name nvarchar(255) , @Id int)
as 
declare @idd int 
declare @namee nvarchar(255)
select   Name , id    from Products 
where Id = @Id and Name = @name 
--return @idd


declare @t table (name nvarchar(255) , Id int null)
insert into @t 
execute  AllP @Id=1061 , @name = 'bzvb'
select * from  @t
------------------------------------------------------------------------
create  proc AllPP (@name nvarchar(255) , @Id int)
as 
declare @idd int 
declare @namee nvarchar(255)
select @idd =  id, @name =  Name   from Products 
where Id = @Id and Name = @name 
return @idd


declare @idd int
execute @idd = AllPP @Id=1061 , @name = 'bzvb'
select @idd
-------------------------------------------------------------------------
alter  proc AllPPP ( @Id int, @Name nvarchar(255) Output)
as 
declare @idd int 
select @idd =  id, @Name =  Name   from Products 
where Id = @Id  
return @idd

-- Declare variables to hold the output
DECLARE @idd INT
DECLARE @Name NVARCHAR(255)

-- Execute the procedure, capturing the return value and output parameter
EXECUTE @idd = AllPPP  1061, @Name OUTPUT

-- Select the returned value (ID)
SELECT @idd AS ReturnedId

-- Select the output parameter (Name)
SELECT @Name AS ProductName
 
select * from Product
select * from Products