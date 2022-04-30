# Unicorn-Insurance

In order to get this application to fully function on your machine you will have to set up 2 SQL Server connection strings in the API project 
for the Data DB and the Identity DB, and then run a Entity Framework Core migration for both using:

update-database --context UnicornDataDBContext

update-database --context UnicornIdentityDBContext

Example LocalDB ConnectionString: Server=(localdb)\\mssqllocaldb;Database=UnicornDataDB;Trusted_Connection=True;MultipleActiveResultSets=true

There are also a couple other AppSettings that are required for the site to be fully functional, such as:

-A SendGrid key in the API project AppSettings. SendGrid is used to send a user a verification email when they register an account.

-A Stripe Publishable key and Secret key in the MVC project AppSettings. Stripe is used in order to checkout.

-An Azure Blob Storage connection string in the MVC project AppSettings. Azure Blob Storage is used to store images for products that are added to the site.

And finally, there are two URLs required in the MVC project AppSettings in order for the client and server to communicate

-There is an appsetting called "HttpClientUrl." This is the URL to where the API project is running, such as "https://localhost:5001"

-There is an appsetting called "ClientVerifyEmail:URL." This is the URL to the VerifyEmail page route in the MVC project, such as "https://localhost:44373/Users/verifyemail"

