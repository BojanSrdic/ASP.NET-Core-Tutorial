CREATE TABLE tbl_Cars(
    car_id bigint NOT NULL PRIMARY KEY,
    model_of_the_car varchar(50),
	purchase_price int
);

-- Drop table
DROP TABLE list_of_cars;
-- Delete data from table but keep blue print of the table
DELETE FROM list_of_cars

CREATE TABLE tbl_Drivers (
    driver_id bigint NOT NULL PRIMARY KEY,
    age_of_the_driver int,
);

DROP TABLE list_of_drivers;

INSERT INTO dbo.list_of_cars VALUES (1, 'BMW', '200')
INSERT INTO dbo.list_of_cars VALUES (2, 'AUDI', '170')
INSERT INTO dbo.list_of_cars VALUES (3, 'PORSHE', '450')
INSERT INTO dbo.list_of_cars VALUES (4, 'McClaren', '700')
INSERT INTO dbo.list_of_cars VALUES (5, 'Bugatti', '2000')
INSERT INTO dbo.list_of_cars VALUES (6, 'Lamborgini', '3200')

---------------------------------------------
		-- -- Stored Procedures -- --
---------------------------------------------
-- How to execute stored procedure in sql
sp_UpdateCarDetails -- just execute
sp_GetCarById id	-- just execute -- in case of more params sp_name param1, param2, param3

-- Drop procedure 
DROP PROCEDURE [sp_GetCarById];  
GO  

---------------------------------------------

CREATE PROCEDURE [dbo].[sp_GetListOfCars]
AS
BEGIN
	-- select * from [dbo].[list_of_cars]
	-- for better performance
	SELECT car_id, model_of_the_car, purchase_price FROM [dbo].[list_of_cars]
END

---------------------------------------------

CREATE PROCEDURE [dbo].[sp_GetCarById]
	@car_id int
AS
BEGIN
	SELECT car_id, model_of_the_car, purchase_price FROM [dbo].[list_of_cars] WHERE car_id = @car_id;
END

---------------------------------------------

CREATE PROCEDURE [dbo].[sp_InsertCarInfo]
	@car_id int,
	@model_of_the_car varchar(50),
	@purchase_price int
AS
BEGIN

INSERT INTO [dbo].[list_of_cars]
VALUES (@car_id, @model_of_the_car, @purchase_price)
END

---------------------------------------------

CREATE PROCEDURE [dbo].[sp_UpdateCarDetails]
	@car_id int,
	@model_of_the_car varchar(50),
	@purchase_price varchar(50)
AS
BEGIN
    UPDATE [QoverDB].[dbo].[list_of_cars]
    SET car_id = @car_id, model_of_the_car = @model_of_the_car, purchase_price = @purchase_price
    WHERE car_id = @car_id;
END

---------------------------------------------
