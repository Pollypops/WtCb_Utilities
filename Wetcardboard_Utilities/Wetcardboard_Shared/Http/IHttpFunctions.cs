namespace Wetcardboard_Shared.Http
{
    public interface IHttpFunctions
    {
        HttpClient GetClientWithBearerToken(string token);
    }
}
