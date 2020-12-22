using Infrastructure.Interfaces;

namespace WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email => "test@test.test";
    }
}
