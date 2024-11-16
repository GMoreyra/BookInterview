var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgresdb");

builder.AddProject<Projects.Api>("api")
       .WithReference(postgresdb)
       .WaitFor(postgresdb);

await builder.Build().RunAsync();
