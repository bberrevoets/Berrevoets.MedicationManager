using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlUserService = builder.AddSqlServer("sqlUserService")
    .WithDataVolume("sqluserservice")
    .WithLifetime(ContainerLifetime.Persistent);

var userDb = sqlUserService.AddDatabase("userdb");

builder.AddProject<UserService>("userservice")
    .WithReference(userDb)
    .WaitFor(userDb);

builder.AddProject<MedicationService>("medicationservice");

builder.AddProject<NotificationService>("notificationservice");

builder.Build().Run();