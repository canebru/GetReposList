
# GetReposList
First pass at assignment
Problems getting RabbitMQ to start.  Could not figure out connection refused error.  Tried disabling firewall, virus protection, etc.
Current version has a hack to pass the LoadData message to the MessageHandler (bypassing RabbitMQ) so that it starts the pull of the data from the repos and populates the database.
Because of this problem, it was left as one application.  
A GET message to "api/loaddata" will still load the data and "api/repositories" will display the list from the database.

This version requires a table created under SQL Server where it is storing just 3 values/columns from the repos list:

CREATE TABLE [dbo].[TestTable] (
    [Id]       INT         NOT NULL,
    [name]     NCHAR (100) NULL,
    [fullname] NCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

where 
Data Source = (localdb)\MSSQLLocalDB
Database = master
