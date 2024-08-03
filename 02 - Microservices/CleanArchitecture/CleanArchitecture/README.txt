Run the following to update/install databases: 

In Package Manager with Net.Microservices.CleanArchitecture.Infrastructure selected as the Default Project, and have Net.Microservices.CleanArchitecture.Presentation.Web set as solution startup project

update-database -context ApplicationDbContext
update-database -context EventStoreDbContext
update-database -context IdentityDbContext