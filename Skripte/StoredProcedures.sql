USE [VehicleReservations]
GO
/****** Object:  StoredProcedure [dbo].[createClient]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:35
-- Description:	Create a client
-- =============================================
CREATE PROCEDURE [dbo].[createClient]
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(100) = NULL,
	@DOB date = NULL,
	@Gender nvarchar(50) = NULL,
	@Country nvarchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Client (FirstName,LastName,DOB,Gender,Country)
	VALUES(@FirstName,@LastName,@DOB,@Gender,@Country);
END
GO
/****** Object:  StoredProcedure [dbo].[createReservation]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 23:20
-- Description:	Create a reservation
-- =============================================
CREATE PROCEDURE [dbo].[createReservation]
	@ClientId int,
	@VehicleId int,
	@ReservedFrom datetime = NULL,
	@ReservedUntil datetime = NULL
AS
BEGIN
	DECLARE @NumberOfCars int = NULL
	SET NOCOUNT ON;

	SELECT @NumberOfCars = COUNT(ClientId) FROM Reservation
	WHERE ClientId = @ClientId;
	
	IF @NumberOfCars < 3 
			INSERT INTO dbo.Reservation(ClientId,VehicleId,ReservedFrom, ReservedUntil)
			VALUES(@ClientId, @VehicleId, @ReservedFrom, @ReservedUntil)
	ELSE 
		RAISERROR ('Cannot have more than 3 reservations on 1 client',15,5); 
END
GO
/****** Object:  StoredProcedure [dbo].[createVehicle]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:35
-- Description:	Create a Vehicle
-- =============================================
CREATE PROCEDURE [dbo].[createVehicle]
	@Manufacturer nvarchar(100) = NULL,
	@Model nvarchar(100) = NULL,
	@Type nvarchar(50) = NULL,
	@Color nvarchar(50) = NULL,
	@Year smallint = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Vehicle(Manufacturer,Model,[Type],Color,[Year])
	VALUES(@Manufacturer,@Model,@Type,@Color,@Year);
END
GO
/****** Object:  StoredProcedure [dbo].[deleteClient]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:11
-- Description:	Delete a Client
-- =============================================
CREATE PROCEDURE [dbo].[deleteClient]
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Client WHERE Id = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[deleteReservation]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 00:01
-- Description:	Delete a reservation
-- =============================================
CREATE PROCEDURE [dbo].[deleteReservation]
	@ReservationId int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Reservation WHERE Id = @ReservationId
END
GO
/****** Object:  StoredProcedure [dbo].[deleteVehicle]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:12
-- Description:	Delete a Vehicle
-- =============================================
CREATE PROCEDURE [dbo].[deleteVehicle]
	@VehicleId int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Vehicle WHERE Id = @VehicleId
END
GO
/****** Object:  StoredProcedure [dbo].[getAllClients]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:25
-- Description:	Basic select all
-- =============================================
CREATE PROCEDURE [dbo].[getAllClients]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Client
	ORDER BY Id ASC;
END
GO
/****** Object:  StoredProcedure [dbo].[getAllReservations]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:26
-- Description:	Select all reservations
-- =============================================
CREATE PROCEDURE [dbo].[getAllReservations]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		c.LastName, c.DOB, c.Gender, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		FROM Reservation r
	LEFT JOIN Client c
		ON r.ClientId = c.Id
	LEFT JOIN Vehicle v
		ON r.VehicleId = v.Id
	ORDER BY r.Id ASC
END
GO
/****** Object:  StoredProcedure [dbo].[getAllVehicles]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:26
-- Description:	Basic select all
-- =============================================
CREATE PROCEDURE [dbo].[getAllVehicles]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Vehicle
	ORDER BY Id ASC
END
GO
/****** Object:  StoredProcedure [dbo].[getClient]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:24
-- Description:	Select a client
-- =============================================
CREATE PROCEDURE [dbo].[getClient]
	@ClientId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Client
	WHERE Id = @ClientId;
END
GO
/****** Object:  StoredProcedure [dbo].[getReservation]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:28
-- Description:	Select a reservation
-- =============================================
CREATE PROCEDURE [dbo].[getReservation]
	@ReservationId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		c.LastName, c.DOB, c.Gender, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		FROM Reservation r
	LEFT JOIN Client c
		ON r.ClientId = c.Id
	LEFT JOIN Vehicle v
		ON r.VehicleId = v.Id
	WHERE r.Id = @ReservationId
END
GO
/****** Object:  StoredProcedure [dbo].[getVehicle]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:28
-- Description:	Select a vehicle
-- =============================================
CREATE PROCEDURE [dbo].[getVehicle]
	@VehicleId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Vehicle
	WHERE Id = @VehicleId;
END
GO
/****** Object:  StoredProcedure [dbo].[searchClient]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:35
-- Description:	search for clients
-- =============================================
CREATE PROCEDURE [dbo].[searchClient]
	@FirstName nvarchar(50) = '',
	@LastName nvarchar(100) = '',
	@DOB nvarchar(50) = '',
	@Gender nvarchar(50) = '',
	@Country nvarchar(100) = ''
AS
BEGIN
	SET NOCOUNT ON;

	Select * FROM dbo.Client
	WHERE
		ISNULL(FirstName,'') like '%'+@FirstName+'%' AND
		ISNULL(LastName,'') like '%'+@LastName+'%' AND
		ISNULL(DOB,'') like '%'+@DOB+'%' AND
		ISNULL(Gender,'') like '%'+@Gender+'%' AND
		ISNULL(Country,'') like '%'+@Country+'%'
	ORDER BY Id ASC
END
GO
/****** Object:  StoredProcedure [dbo].[searchReservation]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:38
-- Description:	search for reservations
-- =============================================
CREATE PROCEDURE [dbo].[searchReservation]
	@Manufacturer nvarchar(100) = '',
	@Model nvarchar(100) = '',
	@Type nvarchar(50) = '',
	@Color nvarchar(50) = '',
	@Year nvarchar(4) = '',
	@FirstName nvarchar(50) = '',
	@LastName nvarchar(100) = '',
	@DOB nvarchar(50) = '',
	@Gender nvarchar(50) = '',
	@Country nvarchar(100) = '',
	@ReservedFrom nvarchar(10) = '',
	@ReservedUntil nvarchar(10) = ''
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Reservation r
		LEFT JOIN dbo.Client c
	ON c.Id = r.ClientId
		LEFT JOIN dbo.Vehicle v
	ON v.Id = r.VehicleId
	WHERE 
		ISNULL(c.FirstName,'') like '%'+@FirstName+'%' AND
		ISNULL(c.LastName,'') like '%'+@LastName+'%' AND
		ISNULL(c.DOB,'') like '%'+@DOB+'%' AND
		ISNULL(c.Gender,'') like '%'+@Gender+'%' AND
		ISNULL(c.Country,'') like '%'+@Country+'%' AND
		ISNULL(v.Manufacturer,'') like '%'+@Manufacturer+'%' AND
		ISNULL(v.Model,'') like '%'+@Model+'%' AND
		ISNULL(v.[Type],'') like '%'+@Type+'%' AND
		ISNULL(v.Color,'') like '%'+@Color+'%' AND
		ISNULL(v.[Year],'') like '%'+@Year+'%' AND
		ISNULL(r.ReservedFrom,'') like '%'+@ReservedFrom+'%' AND
		ISNULL(r.ReservedUntil,'') like '%'+@ReservedUntil+'%'
	ORDER BY r.Id ASC
END
GO
/****** Object:  StoredProcedure [dbo].[searchVehicle]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 10.8.2022 17:39
-- Description:	search for vehicles
-- =============================================
CREATE PROCEDURE [dbo].[searchVehicle]
	@Manufacturer nvarchar(100) = '',
	@Model nvarchar(100) = '',
	@Type nvarchar(50) = '',
	@Color nvarchar(50) = '',
	@Year nvarchar(4) = ''
AS
BEGIN
	SET NOCOUNT ON;

	Select * FROM dbo.Vehicle
	WHERE 
		ISNULL(Manufacturer,'') like '%'+@Manufacturer+'%' AND
		ISNULL(Model,'') like '%'+@Model+'%' AND
		ISNULL([Type],'') like '%'+@Type+'%' AND
		ISNULL(Color,'') like '%'+@Color+'%' AND
		ISNULL([Year],'') like '%'+@Year+'%'
	ORDER BY Id
END
GO
/****** Object:  StoredProcedure [dbo].[updateClient]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:29
-- Description:	Update a Client
-- =============================================
CREATE PROCEDURE [dbo].[updateClient]
	@ClientId int,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(100) = NULL,
	@DOB date = NULL,
	@Gender nvarchar(50) = NULL,
	@Country nvarchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	Update dbo.Client
	SET	FirstName = @FirstName,
		LastName = @LastName,
		DOB = @DOB,
		Gender = @Gender,
		Country = @Country
	WHERE Id = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[updateReservation]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 00:00
-- Description:	Update a reservation
-- =============================================
CREATE PROCEDURE [dbo].[updateReservation]
	@ReservationId int,
	@ClientId int,
	@VehicleId int,
	@ReservedFrom datetime,
	@ReservedUntil datetime
AS
BEGIN
	DECLARE @NumberOfCars int = NULL;
	SET NOCOUNT ON;
	
	SELECT @NumberOfCars = COUNT(ClientId) FROM Reservation
	WHERE ClientId = @ClientId;

	IF @NumberOfCars < 3 
		UPDATE Reservation
		SET ClientId = @ClientId,
			VehicleId = @VehicleId,
			ReservedFrom = @ReservedFrom,
			ReservedUntil = @ReservedUntil
		WHERE Id = @ReservationId
	ELSE 
		RAISERROR ('Cannot have more than 3 reservations on 1 client',15,5); 
END
GO
/****** Object:  StoredProcedure [dbo].[updateVehicle]    Script Date: 10.8.2022. 20:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Marin Mikleušević
-- Create date: 9.8.2022 19:32
-- Description:	Update a Vehicle
-- =============================================
CREATE PROCEDURE [dbo].[updateVehicle]
	@VehicleId int,
	@Manufacturer nvarchar(100) = NULL,
	@Model nvarchar(100) = NULL,
	@Type nvarchar(50) = NULL,
	@Color nvarchar(50) = NULL,
	@Year smallint = NULL
AS
BEGIN
	SET NOCOUNT ON;

	Update dbo.Vehicle
	SET	Manufacturer = @Manufacturer,
		Model = @Model,
		[Type] = @Type,
		Color = @Color,
		[Year] = @Year
	WHERE Id = @VehicleId
END
GO
