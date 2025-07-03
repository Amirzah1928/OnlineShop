namespace OnlineShop.APIs.Exceptions
{
    public class TooManyRequestException(string message) : Exception(message)
    {
    }
}
