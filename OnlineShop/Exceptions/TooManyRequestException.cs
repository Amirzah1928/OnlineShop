namespace OnlineShop.Exceptions
{
    public class TooManyRequestException(string message) : Exception(message)
    {
    }
}
