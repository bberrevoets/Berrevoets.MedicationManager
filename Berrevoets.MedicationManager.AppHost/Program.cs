var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MedicationService>("medicationservice");

builder.AddProject<Projects.UserService>("userservice");

builder.AddProject<Projects.NotificationService>("notificationservice");

builder.Build().Run();
