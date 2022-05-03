using DataAcessLayer.Models.Preferences;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Client
{
    public interface IClient_Repo
    {
        List<ClientUsers> GetUsers();

        ClientUsers RegisterUser(ClientUsers clientUsers);

        ClientUsers LoginUser(ClientUsers clientUsers);

        Task<ClientPublicProfile> PublicProfile(int UserId);



        Task<string> GetProfileImage(int UserId);
        Task<bool> UpdateImage(int UserId, string ProfileImage);



        Task<UserProfileMenu> MenuDetails(int UserId);


        Task<object> GetUserPreferences(int TypeId,int UserId);
        Task SavePreference(User_TravelPreference userPreferences);


        bool ResetPassword(int UserId, string Email);
        Task<int> ChangePassword(UserChangePassword user);
    }
}
