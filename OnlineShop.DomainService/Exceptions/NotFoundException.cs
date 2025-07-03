namespace OnlineShop.DomainService.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
