namespace PostMessengerService.Application.Middlewares;

public interface IUserProviderMiddleware
{
    string GetUsername();
}