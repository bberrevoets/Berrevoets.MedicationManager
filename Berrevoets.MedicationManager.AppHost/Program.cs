var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MedicationService>("medicationservice");

builder.AddProject<Projects.NotificationService>("notificationservice");

builder.AddProject<Projects.UserService>("userservice");

builder.Build().Run();
