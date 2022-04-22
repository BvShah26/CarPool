using Apis.Data;
using Apis.Infrastructure.Client;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Client
{
    public class Client_Repo : IClient_Repo
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public Client_Repo(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<ClientUsers> GetUsers()
        {
            return _context.ClientUsers.ToList();
        }

        public ClientUsers LoginUser(ClientUsers clientUsers)
        {
            ClientUsers record = _context.ClientUsers.FirstOrDefault(item => item.Email == clientUsers.Email && item.Password == clientUsers.Password);
            return record;
        }

        public async Task<ClientPublicProfile> PublicProfile(int UserId)
        {
            ClientPublicProfile record = await _context.ClientUsers.Where(x => x.Id == UserId).Include(user => user.Published_Rides)
                .Include(x => x.UserPreference).ThenInclude(preference => preference.Travel_Preference)
                .Select(x => new ClientPublicProfile()
                {
                    UserName = x.Name,
                    ProfileImage = x.ProfileImage,
                    RegistrationDate = x.RegistrationDate,
                    TotalRides = x.Published_Rides.Count,
                    Preferences = x.UserPreference.Select(userPreference => userPreference.Travel_Preference).ToList()
                    
                })
                .FirstOrDefaultAsync();

            return record;
            //throw new NotImplementedException();
        }

        public ClientUsers RegisterUser(ClientUsers clientUsers)
        {
            clientUsers.RegistrationDate = DateTime.Now;
            var result = _context.ClientUsers.Add(clientUsers);
            _context.SaveChanges();

            ApplicationUser user = new ApplicationUser()
            {
                Email = clientUsers.Email,
                UserName = clientUsers.Email,
                PhoneNumber = clientUsers.MobileNumber.ToString(),
            };

            IdentityResult identityResult = _userManager.CreateAsync(user, clientUsers.Password).Result;
            //if (identityResult.Succeeded)
            //{
            //    _userManager.AddToRoleAsync(user, "MultiplexAdmin").Wait();
            //}
            //return Ok();

            return result.Entity;

        }
    }
}
