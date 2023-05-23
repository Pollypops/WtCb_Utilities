namespace Wetcardboard_Utilities_General.Http
{
    public interface IHttpFunctions
    {
        HttpClient GetClientWithBearerToken(string token);
    }
}
