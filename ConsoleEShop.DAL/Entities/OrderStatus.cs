namespace ConsoleEShop.DAL.Entities
{
    /// <summary>
    /// Defines statuses of orders
    /// </summary>
    public enum OrderStatus
    {
        New,
        CanceledByUser,
        PaymentReceived,
        Sent,
        Received,
        Completed,
        CanceledAdministrator
    }
}
