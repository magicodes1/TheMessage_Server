using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Comunications;
using TheMessage.Resources;

namespace TheMessage.Interfaces.Servives
{
    public interface IUserService
    {
        Task<SignUpResponse> signUp(SignUpResource signUpModel);
        Task<LogoutResponse> logout(string userId);
        Task<OnlineUserResponse> getOnlineUsers();
        Task<SignInResponse> signIn(SignInResource signInModel);
    }
}
