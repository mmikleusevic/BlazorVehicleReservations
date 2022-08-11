using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVehicleReservations.API.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region spCreateClient
            string procedure = @"USE [VehicleReservations]
            GO
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO


            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 08:06
            -- Description:	Create a client
            -- =============================================
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spCreateReservation
            procedure = @"SET ANSI_NULLS ON
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
	            DECLARE @NumberOfCars int = NULL
	            SET NOCOUNT OFF;

	            SELECT @NumberOfCars = COUNT(ClientId) FROM Reservation
	            WHERE ClientId = @ClientId;
	
			    INSERT INTO dbo.Reservation(ClientId,VehicleId,ReservedFrom, ReservedUntil)
			    VALUES(@ClientId, @VehicleId, @ReservedFrom, @ReservedUntil)
            END
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spCreateVehicle
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spDeleteClient
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spDeleteReservation
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spDeleteVehicle
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetAllClients
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetAllReservations
            procedure = @"SET ANSI_NULLS ON
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

	            SELECT r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		            c.LastName, c.DOB, c.Gender, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		            FROM Reservation r
	            LEFT JOIN Client c
		            ON r.ClientId = c.Id
	            LEFT JOIN Vehicle v
		            ON r.VehicleId = v.Id
	            ORDER BY r.Id ASC
            END
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetAllVehicles
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetClient
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetReservation
            procedure = @"SET ANSI_NULLS ON
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

	            SELECT r.ClientId, r.VehicleId, r.ReservedFrom, r.ReservedUntil, c.FirstName,
		            c.LastName, c.DOB, c.Gender, v.Manufacturer, v.Model, v.[Type], v.Color, v.[Year]  
		            FROM Reservation r
	            LEFT JOIN Client c
		            ON r.ClientId = c.Id
	            LEFT JOIN Vehicle v
		            ON r.VehicleId = v.Id
	            WHERE r.Id = @ReservationId
            END
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetVehicle
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spSearchClient
            procedure = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO

            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 08:34
            -- Description:	search for clients
            -- =============================================
            CREATE PROCEDURE [dbo].[spSearchClient]
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spSearchReservation
            procedure = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO


            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 08:35
            -- Description:	search for reservations
            -- =============================================
            CREATE PROCEDURE [dbo].[spSearchReservation]
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spSearchVehicle
            procedure = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO

            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 08:43
            -- Description:	search for vehicles
            -- =============================================
            CREATE PROCEDURE [dbo].[spSearchVehicle]
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spUpdateClient
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spUpdateReservation
            procedure = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO

            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 08:39
            -- Description:	Update a reservation
            -- =============================================
            CREATE PROCEDURE [dbo].[spUpdateReservation]
	            @ReservationId int,
	            @ClientId int,
	            @VehicleId int,
	            @ReservedFrom datetime,
	            @ReservedUntil datetime
            AS
            BEGIN
	            DECLARE @NumberOfCars int = NULL;
	            SET NOCOUNT OFF;
	
	            SELECT @NumberOfCars = COUNT(ClientId) FROM Reservation
	            WHERE ClientId = @ClientId;

		         UPDATE Reservation
		         SET ClientId = @ClientId,
			        VehicleId = @VehicleId,
			        ReservedFrom = @ReservedFrom,
			        ReservedUntil = @ReservedUntil
		         WHERE Id = @ReservationId
            END
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spUpdateVehicle
            procedure = @"SET ANSI_NULLS ON
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
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
            #region spGetAllClientReservations
            procedure = @"SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO

            -- =============================================
            -- Author:		Marin Mikleušević
            -- Create date: 11.8.2022 09:49
            -- Description:	Select all client reservations
            -- =============================================
            CREATE PROCEDURE [dbo].[spGetAllClientReservations]
	            @ClientId int
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
	            WHERE r.ClientId = @ClientId
            END
            GO";
            migrationBuilder.Sql(procedure);
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            #region dropMigration
            string procedure = @"Drop procedure spCreateClient";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spCreateReservation";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spCreateVehicle";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spDeleteClient";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spDeleteReservation";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spDeleteVehicle";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetAllClients";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetAllReservations";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetAllVehicles";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetClient";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetReservation";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetVehicle";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spSearchClient";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spSearchReservation";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spSearchVehicle";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spUpdateClient";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spUpdateReservation";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spUpdateVehicle";
            migrationBuilder.Sql(procedure);
            procedure = @"Drop procedure spGetAllClientReservations";
            migrationBuilder.Sql(procedure);
            #endregion
        }
    }
}
