dotnet ef database update => create and update the database if necessary which will match the db context in our project.

dotnet ef migrations add initialDb => looks at the databses that we have and adds to an empty databse stuff we need

dotnet ef database update => will now have the tables populated  