namespace WebApplication1.Services.ServiceInterfaces
{
    public interface INotificationService
    {
        Task SendWelcomeEmailAsync(string? email, string? supplierName);
    }
}
