------------------------------------- store -------------------------------------------
-- Insert
create proc Insert_Store @strid int ,  @strname nvarchar(50), @address nvarchar(100) , @managername nvarchar(50)
as
Begin
     IF not EXISTS (SELECT Store_Id FROM Stores WHERE Store_Id = @strid)
      INSERT INTO Stores
      VALUES (@strid, @strname, @address, @managername)
  ELSE
    Select 'Already exist'
End
Go
-- update
alter proc Update_Store  @strid int ,  @strname nvarchar(50), @address nvarchar(100) , @managername nvarchar(50)
as 
Begin 
   IF EXISTS (SELECT Store_Id FROM Stores WHERE Store_Id = @strid)
    UPDATE Stores SET Store_Id = @strid, Store_Name = @strname, Address = @address , Manager_Name = @managername WHERE Store_Id = @strid
   ELSE
     select 'not exist'
End
Go
-- delete
create proc Delete_Store  @strid int
as 
Begin 
  IF EXISTS (SELECT Store_Id FROM Stores WHERE Store_Id = @strid)
    DELETE from Stores where Store_Id = @strid
  ELSE
     select 'not exist'
End
Go
------------------------------------- Items -------------------------------------------
-- Insert
alter proc Insert_Item @itmcode int , @itmname nvarchar(50), @prodDate Date , @expDate Date, @count int 
as
Begin
     IF not EXISTS (SELECT Item_Code FROM Items WHERE Item_Code = @itmcode)
      INSERT INTO Items
      VALUES (@itmcode, @itmname, @prodDate, @expDate, @count)
  ELSE
    Select 'Already exist'
End
Go
-- update
alter proc Update_Item  @itmcode int ,  @itmname nvarchar(50), @prodDate Date , @expDate Date,  @count int =0 ,@type varchar(50) = 'Import'
as 
Begin 
 If(@type = 'Import')
  Begin
   IF EXISTS (SELECT Item_Code FROM Items WHERE Item_Code = @itmcode)
    UPDATE Items SET Item_Code = @itmcode, Item_Name = @itmname, Prod_Date = @prodDate, Validity_Period = @expDate, total_Count = total_Count+ @count WHERE Item_Code = @itmcode
   ELSE
     select 'not exist'
  End
 ELSE
   UPDATE Items SET Item_Code = @itmcode, Item_Name = @itmname, Prod_Date = @prodDate, Validity_Period = @expDate, total_Count = total_Count - @count WHERE Item_Code = @itmcode
End
Go
-- delete
create proc Delete_Item  @itmcode int
as 
Begin 
  IF EXISTS (SELECT Item_Code FROM Items WHERE Item_Code = @itmcode)
    DELETE from Items where Item_Code = @itmcode
  ELSE
     select 'not exist'
End
Go------------------------------------- supplier -------------------------------------------
-- Insert
create proc Insert_Supplier @supid int ,  @supname varchar(50), @phone varchar(50), @fax varchar(50), @email varchar(50), @website varchar(50)
as
Begin
     IF not EXISTS (SELECT Sup_Id FROM Suppliers WHERE Sup_Id = @supid)
      INSERT INTO Suppliers
      VALUES (@supid, @supname, @phone, @fax, @email, @website)
  ELSE
    Select 'Already exist'
End
Go
-- update
create proc Update_Supplier @supid int ,  @supname varchar(50), @phone varchar(50), @fax varchar(50), @email varchar(50), @website varchar(50)
as 
Begin 
     IF EXISTS (SELECT Sup_Id FROM Suppliers WHERE Sup_Id = @supid)
    UPDATE Suppliers SET Sup_Id = @supid, Sup_Name = @supname, Phone = @phone, Fax = @fax, EMail = @email, Website = @website WHERE Sup_Id = @supid
   ELSE
     select 'not exist'
End
Go
-- delete
create proc Delete_Supplier  @supid int
as 
Begin 
  IF EXISTS (SELECT Sup_Id FROM Suppliers WHERE Sup_Id = @supid)
    DELETE from Suppliers where Sup_Id = @supid
  ELSE
     select 'not exist'
End
Go
------------------------------------- customer -------------------------------------------
-- Insert
create proc Insert_Customer @cusname varchar(50), @phone varchar(50), @fax varchar(50), @email varchar(50)
as
Begin
     IF not EXISTS (SELECT Cust_Name FROM Customers WHERE Cust_Name = @cusname)
      INSERT INTO Customers
      VALUES (@cusname, @phone, @fax, @email)
  ELSE
    Select 'Already exist'
End
Go
Insert_Customer 'gggggg','014566','1459','ggggggggggggg'
-- update
create proc Update_Customer  @cusname varchar(50), @phone varchar(50), @fax varchar(50), @email varchar(50)
as 
Begin 
   IF EXISTS (SELECT Cust_Name FROM Customers WHERE Cust_Name = @cusname)
    UPDATE Customers SET Cust_Name = @cusname, Phone = @phone, Fax = @fax, EMail = @email WHERE Cust_Name = @cusname
   ELSE
     select 'not exist'
End
Go
-- delete
alter proc Delete_Customer  @cusname varchar(50)
as 
Begin 
   IF EXISTS (SELECT Cust_Name FROM Customers WHERE Cust_Name = @cusname)
	   Begin 
			DELETE from Customers where Cust_Name = @cusname
	   End
  ELSE
     select 'not exist'
End
Go
---------------------------------------- Permissions -----------------------------------------------
-- insert
create proc Insert_Permmission @PermId int , @PermType varchar(50), @storeid int, @supid int, @perm_Date Date
as
Begin 
   IF not EXISTS (SELECT Perm_Id FROM Permissions WHERE Perm_Id = @PermId)
    Begin
      IF EXISTS (SELECT Store_Id FROM Stores WHERE Store_Id = @storeid)
	    Begin 
		  IF EXISTS (SELECT Sup_Id FROM Suppliers WHERE Sup_Id = @supid)
		     INSERT INTO Permissions
             VALUES (@PermId, @PermType, @storeid, @supid, @perm_Date)
		End   
	End
	ELSE
	   SELECT 'Already EXist'
End
Go
--update
create proc Update_Permmission @PermId int , @PermType varchar(50), @storeid int, @supid int, @perm_Date Date
as
Begin 
   IF EXISTS (SELECT Perm_Id FROM Permissions WHERE Perm_Id = @PermId)
 		UPDATE Permissions
		SET Perm_Id = @PermId, Perm_Type = @PermType, Store_Id = @storeid, Sup_ID = @supid, Perm_Date = @perm_Date
		WHERE Perm_Id = @PermId
   ELSE
	    SELECT 'Not EXist'
End
Go
-- delete
create proc Delete_Permmission @PermId int 
as
Begin 
   IF EXISTS (SELECT Perm_Id FROM Permissions WHERE Perm_Id = @PermId)
		DELETE FROM Permissions
		WHERE Perm_Id = @PermId
	ELSE
	    SELECT 'Not EXist'
End
Go
----------------------------------------------- Reports -----------------------------------------------------
-- Report 1
-- all data
alter proc showAllStore @storename nvarchar(50)
as
Begin 
	SELECT s.* ,i.Item_Name,si.Item_Total_Count, i.Prod_Date,i.Validity_Period as Expire_Date FROM Stores s, Store_Items si, Items i
	WHERE Store_Name = @storename AND s.Store_Id = si.Store_Id AND si.Item_Code = i.Item_Code
End
