namespace CatalogService.Infrastructure.Interfaces
{
    public interface IRabbitMqProducer : IDisposable
    {
        Task PublishAsync(string message, CancellationToken cancellationToken);
    }
}
