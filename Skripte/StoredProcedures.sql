USE [VehicleReservations]
GO
/****** Object:  StoredProcedure [dbo].[spCreateClient]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateClient]
	                                @FirstName nvarchar(50) = NULL,
	                                @LastName nvarchar(100) = NULL,
	                                @DOB date = NULL,
	                                @Gender nvarchar(50) = NULL,
	                                @Country nvarchar(100) = NULL
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                INSERT INTO dbo.Client (FirstName,LastName,DOB,Gender,Country)
	                                VALUES(@FirstName,@LastName,@DOB,@Gender,@Country);
                                END
GO
/****** Object:  StoredProcedure [dbo].[spCreateReservation]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:11
                                -- Description:	Create a reservation
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spCreateReservation]
	                                @ClientId int,
	                                @VehicleId int,
	                                @ReservedFrom datetime = NULL,
	                                @ReservedUntil datetime = NULL
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;
	
			                        INSERT INTO dbo.Reservation(ClientId,VehicleId,ReservedFrom, ReservedUntil)
			                        VALUES(@ClientId, @VehicleId, @ReservedFrom, @ReservedUntil)
                                END
GO
/****** Object:  StoredProcedure [dbo].[spCreateVehicle]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:14
                                -- Description:	Create a Vehicle
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spCreateVehicle]
	                                @Manufacturer nvarchar(100) = NULL,
	                                @Model nvarchar(100) = NULL,
	                                @Type nvarchar(50) = NULL,
	                                @Color nvarchar(50) = NULL,
	                                @Year smallint = NULL
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                INSERT INTO dbo.Vehicle(Manufacturer,Model,[Type],Color,[Year])
	                                VALUES(@Manufacturer,@Model,@Type,@Color,@Year);
                                END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteClient]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:21
                                -- Description:	Delete a Client
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spDeleteClient]
	                                @ClientId int
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                DELETE FROM Client WHERE Id = @ClientId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteReservation]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:22
                                -- Description:	Delete a reservation
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spDeleteReservation]
	                                @ReservationId int
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                DELETE FROM Reservation WHERE Id = @ReservationId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteVehicle]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:24
                                -- Description:	Delete a Vehicle
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spDeleteVehicle]
	                                @VehicleId int
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                DELETE FROM Vehicle WHERE Id = @VehicleId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllAvailableVehicles]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                 -- Author:		Marin Mikleušević
                                 -- Create date: 13.8.2022 09:41
                                 -- Description:	Select all available vehicles
                                 -- =============================================
                                 CREATE PROCEDURE [dbo].[spGetAllAvailableVehicles]
                                 AS
                                 BEGIN
	                                SET NOCOUNT ON;

	                                SELECT * FROM Vehicle v 
										  WHERE v.Id NOT IN( 
											    SELECT v.Id
												      FROM Reservation r
											    LEFT JOIN Vehicle v
												      ON r.VehicleId = v.Id
											    WHERE r.ReservedFrom <= GETDATE() AND r.ReservedUntil >= GETDATE()
									) 
                                  END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllClients]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:25
                                -- Description:	Basic select all
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetAllClients]
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT * FROM dbo.Client
	                                ORDER BY Id ASC;
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllCurrentReservations]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 13.8.2022 09:41
                                -- Description:	Select all current reservations
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetAllCurrentReservations]
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT r.Id, r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		                                c.LastName, c.DOB, c.Gender, c.Country, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		                                FROM Reservation r
	                                LEFT JOIN Client c
		                                ON r.ClientId = c.Id
	                                LEFT JOIN Vehicle v
		                                ON r.VehicleId = v.Id
                                    WHERE r.ReservedUntil >= GETDATE()
	                                ORDER BY r.Id ASC
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllReservations]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:27
                                -- Description:	Select all reservations
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetAllReservations]
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT r.Id, r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		                                c.LastName, c.DOB, c.Gender, c.Country, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		                                FROM Reservation r
	                                LEFT JOIN Client c
		                                ON r.ClientId = c.Id
	                                LEFT JOIN Vehicle v
		                                ON r.VehicleId = v.Id
	                                ORDER BY r.Id ASC
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllVehicles]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:28
                                -- Description:	Basic select all
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetAllVehicles]
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT * FROM dbo.Vehicle
	                                ORDER BY Id ASC
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetClient]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:30
                                -- Description:	Select a client
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetClient]
	                                @ClientId int
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT * FROM dbo.Client
	                                WHERE Id = @ClientId;
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetReservation]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:31
                                -- Description:	Select a reservation
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetReservation]
	                                @ReservationId int
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT r.Id, r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		                                c.LastName, c.DOB, c.Gender, c.Country, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		                                FROM Reservation r
	                                LEFT JOIN Client c
		                                ON r.ClientId = c.Id
	                                LEFT JOIN Vehicle v
		                                ON r.VehicleId = v.Id
	                                WHERE r.Id = @ReservationId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetVehicle]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:33
                                -- Description:	Select a vehicle
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spGetVehicle]
	                                @VehicleId int
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT * FROM dbo.Vehicle
	                                WHERE Id = @VehicleId;
                                END
GO
/****** Object:  StoredProcedure [dbo].[spSearchClient]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:34
                                -- Description:	search for clients
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spSearchClient]
	                                @FirstName nvarchar(20) = '',
	                                @LastName nvarchar(20) = '',
	                                @DOB nvarchar(10) = '',
	                                @Gender nvarchar(20) = '',
	                                @Country nvarchar(20) = ''
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
/****** Object:  StoredProcedure [dbo].[spSearchReservation]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:35
                                -- Description:	search for reservations
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spSearchReservation]
	                                @ReservedFrom nvarchar(10) = '',
	                                @ReservedUntil nvarchar(10) = '',
	                                @FirstName nvarchar(20) = '',
	                                @LastName nvarchar(20) = '',
	                                @DOB nvarchar(10) = '',
	                                @Gender nvarchar(20) = '',
	                                @Country nvarchar(20) = '',
	                                @Manufacturer nvarchar(20) = '',
	                                @Model nvarchar(20) = '',
	                                @Type nvarchar(20) = '',
	                                @Color nvarchar(20) = '',
	                                @Year nvarchar(4) = ''
                                AS
                                BEGIN
	                                SET NOCOUNT ON;

	                                SELECT r.Id, r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		                                c.LastName, c.DOB, c.Gender, c.Country, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
									FROM dbo.Reservation r
		                                LEFT JOIN dbo.Client c
	                                ON c.Id = r.ClientId
		                                LEFT JOIN dbo.Vehicle v
	                                ON v.Id = r.VehicleId
	                                WHERE 
		                                ISNULL(r.ReservedFrom,'') like '%'+@ReservedFrom+'%' AND
		                                ISNULL(r.ReservedUntil,'') like '%'+@ReservedUntil+'%' AND
		                                ISNULL(c.FirstName,'') like '%'+@FirstName+'%' AND
		                                ISNULL(c.LastName,'') like '%'+@LastName+'%' AND
		                                ISNULL(c.DOB,'') like '%'+@DOB+'%' AND
		                                ISNULL(c.Gender,'') like '%'+@Gender+'%' AND
		                                ISNULL(c.Country,'') like '%'+@Country+'%' AND
		                                ISNULL(v.Manufacturer,'') like '%'+@Manufacturer+'%' AND
		                                ISNULL(v.Model,'') like '%'+@Model+'%' AND
		                                ISNULL(v.[Type],'') like '%'+@Type+'%' AND
		                                ISNULL(v.Color,'') like '%'+@Color+'%' AND
		                                ISNULL(v.[Year],'') like '%'+@Year+'%'
	                                ORDER BY r.Id ASC
                                END
GO
/****** Object:  StoredProcedure [dbo].[spSearchVehicle]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:43
                                -- Description:	search for vehicles
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spSearchVehicle]
	                                @Manufacturer nvarchar(20) = '',
	                                @Model nvarchar(20) = '',
	                                @Type nvarchar(20) = '',
	                                @Color nvarchar(20) = '',
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
/****** Object:  StoredProcedure [dbo].[spUpdateClient]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:42
                                -- Description:	Update a Client
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spUpdateClient]
	                                @ClientId int,
	                                @FirstName nvarchar(50) = NULL,
	                                @LastName nvarchar(100) = NULL,
	                                @DOB date = NULL,
	                                @Gender nvarchar(50) = NULL,
	                                @Country nvarchar(100) = NULL
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                Update dbo.Client
	                                SET	FirstName = @FirstName,
		                                LastName = @LastName,
		                                DOB = @DOB,
		                                Gender = @Gender,
		                                Country = @Country
	                                WHERE Id = @ClientId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateReservation]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 20:19
                                -- Description:	Update a reservation
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spUpdateReservation]
	                                @ReservationId int,
	                                @ReservedFrom datetime,
	                                @ReservedUntil datetime
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

		                             UPDATE Reservation
		                             SET 
			                            ReservedFrom = @ReservedFrom,
			                            ReservedUntil = @ReservedUntil
		                             WHERE Id = @ReservationId
                                END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateVehicle]    Script Date: 14.8.2022. 17:27:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
                                -- Author:		Marin Mikleušević
                                -- Create date: 11.8.2022 08:40
                                -- Description:	Update a Vehicle
                                -- =============================================
                                CREATE PROCEDURE [dbo].[spUpdateVehicle]
	                                @VehicleId int,
	                                @Manufacturer nvarchar(100) = NULL,
	                                @Model nvarchar(100) = NULL,
	                                @Type nvarchar(50) = NULL,
	                                @Color nvarchar(50) = NULL,
	                                @Year smallint = NULL
                                AS
                                BEGIN
	                                SET NOCOUNT OFF;

	                                Update dbo.Vehicle
	                                SET	Manufacturer = @Manufacturer,
		                                Model = @Model,
		                                [Type] = @Type,
		                                Color = @Color,
		                                [Year] = @Year
	                                WHERE Id = @VehicleId
                                END
GO
