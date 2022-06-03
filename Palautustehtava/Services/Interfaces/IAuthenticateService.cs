
using Palautustehtava.Models;
namespace Palautustehtava.Services.Interfaces
{
    public interface IAuthenticateService
    {
        LoggedUser Authenticate(string username, string password);
        
    }
}
