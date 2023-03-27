CREATE TABLE Stores(
	Store_Id int PRIMARY KEY ,
	Store_Name nvarchar(50) NOT NULL,
	Address nvarchar(100) NOT NULL,
	Manager_Name nvarchar(50) NOT NULL)
CREATE TABLE Items(
	Item_Code int PRIMARY KEY,
	Item_Name nvarchar(50) NOT NULL,
	Prod_Date DATE NOT NULL,
	Validity_Period DATE NOT NULL)
CREATE TABLE Store_Items(
    Store_Id int NOT NULL,
	Item_Code int NOT NULL,
	Item_Total_Count int NOT NULL,
	Constraint C1 PRIMARY KEY (Store_Id,Item_Code),
	Constraint C2 FOREIGN KEY (Store_Id) REFERENCES Stores (Store_Id),
	Constraint C3 FOREIGN KEY (Item_Code) REFERENCES Items (Item_Code)
)
CREATE TABLE Measurement(
    Id int PRIMARY KEY Identity(1,1),
	Item_Code int NOT NULL,
	measure_Name varchar(50) NOT NULL
    Constraint C4 FOREIGN KEY (Item_Code) REFERENCES Items (Item_Code)
)
CREATE TABLE Customers (
   Cust_Id int PRIMARY KEY,
   Cust_Name varchar(50) NOT NULL,
   Phone varchar(50) NOT NULL,
   Fax varchar(50) NOT NULL,
   EMail varchar(50) NOT NULL,
)
CREATE TABLE Suppliers (
   Sup_Id int PRIMARY KEY,
   Sup_Name varchar(50) NOT NULL,
   Phone varchar(50) NOT NULL,
   Fax varchar(50) NOT NULL,
   EMail varchar(50) NOT NULL,
   Website varchar(50) NOT NULL,
)
CREATE TABLE Permissions(
    Perm_Id int PRIMARY KEY,
	Perm_Type varchar(50) NOT NULL,
	Store_Id int NOT NULL,
	Perm_Date DATE NOT NULL,
	Sup_ID int NOT NULL,
	Constraint C5 FOREIGN KEY (Store_Id) REFERENCES Stores (Store_Id),
	Constraint C6 FOREIGN KEY (Sup_ID) REFERENCES Suppliers (Sup_ID)
)
CREATE TABLE Permissions_Items(
    Id int PRIMARY KEY Identity(1,1),
	Perm_Id int NOT NULL,
    Item_Code int NOT NULL,
	Item_Count int NOT NULL,
    Constraint C7 FOREIGN KEY (Perm_Id) REFERENCES Permissions (Perm_Id),
	Constraint C8 FOREIGN KEY (Item_Code) REFERENCES Items (Item_Code)
)
CREATE TABLE Items_Movement(
    Id int PRIMARY KEY Identity(1,1),
	FromStore_Id int NOT NULL,
	ToStore_Id int NOT NULL,
	Item_Code int NOT NULL,
	Item_Count int NOT NULL,
	Sup_Id int NOT NULL,
	Prod_Date DATE NOT NULL,
	Validity_Period DATE NOT NULL,
	Constraint C12 FOREIGN KEY (Item_Code) REFERENCES Items (Item_Code),
	Constraint C13 FOREIGN KEY (FromStore_Id) REFERENCES Stores (Store_Id),
	Constraint C14 FOREIGN KEY (ToStore_Id) REFERENCES Stores (Store_Id),
	Constraint C15 FOREIGN KEY (Sup_Id) REFERENCES Suppliers (Sup_Id),
)