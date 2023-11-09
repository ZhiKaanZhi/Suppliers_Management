namespace WebApplication1.Services.ServiceInterfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Sends a welcome email asynchronously to a new supplier.
        /// </summary>
        /// <param name="email">The email address where the welcome email will be sent.</param>
        /// <param name="supplierName">The name of the supplier to whom the welcome email is addressed.</param>
        /// <returns>A task that represents the asynchronous send email operation.</returns>
        Task SendWelcomeEmailAsync(string? email, string? supplierName);
    }
}
