using Wetcardboard_Authentication.Model;

namespace Wetcardboard_Authentication.Authenticator
{
    public interface IAuthenticator
    {
        void Authenticate();
        void SignOut();
    }
}
