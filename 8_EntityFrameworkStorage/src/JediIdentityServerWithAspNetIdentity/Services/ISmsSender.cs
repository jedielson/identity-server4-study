using System.Threading.Tasks;

namespace JediIdentityServerWithAspNetIdentity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
