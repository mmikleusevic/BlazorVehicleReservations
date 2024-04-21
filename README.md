Author: TeamDB

Description:

It is necessary to create a web application for vehicle rental.

The application should include the following functionalities:
   - Client overview
   - Vehicle overview
   - Reservation entry
   - Overview of entered reservations
   - Reservation deletion (optional)
   - Data specification and rules:

Entities for system operation:

    - The Client entity should contain at least the following data: first name, last name, year of birth, gender, country
    - The Vehicle entity should contain at least the following data: manufacturer (e.g., Kia, Porsche...), type (e.g., SUV, station wagon, sedan...), color, year
    - The Reservation entity must contain information about the rental time (from, to), the client, and the vehicle.
    
Rules:

    - It is not possible to create a reservation for the same vehicle at the same rental time
    - A client cannot rent more than three vehicles simultaneously
    - A client cannot rent more than one vehicle per type simultaneously
    - The client overview should have the ability to search by at least two parameters (e.g., first name, last name)
    - The vehicle overview should have the ability to search by at least two parameters (e.g., type, model)
    - The reservation overview should have the ability to search by at least two parameters (e.g., date, client)
        
Task 1

Create a database in MSSQL with the necessary tables (entities) that support the defined dataset.

Task 2

Create a web application according to the specification. Use C# for the backend and choose any technology for the frontend. Use SQL stored procedures to communicate with the database.
