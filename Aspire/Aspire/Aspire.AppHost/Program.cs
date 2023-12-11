using System.Collections.Immutable;

using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr();

var daprComponentsFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "dapr_components"));
_ = builder.AddProject<Projects.MyApp>("myapp").WithDaprSidecar(new DaprSidecarOptions
{
    AppId = "myapp",
    AppPort = 5000,
    AppProtocol = "http",
    LogLevel = "debug",
    ResourcesPaths = ImmutableHashSet.Create<string>(daprComponentsFolder, $"{daprComponentsFolder}/local", $"{daprComponentsFolder}/aspire"),
    DaprHttpPort = 3500,
    DaprGrpcPort = 50001,
    MetricsPort = 9090,
    Config = $"{daprComponentsFolder}/config.yaml"
});

builder.Build().Run();
