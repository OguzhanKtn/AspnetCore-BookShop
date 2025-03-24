namespace Infrastructure.Services;

public sealed class AzureStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
}
