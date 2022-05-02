using Apis.Data;
using Apis.Infrastructure.Client;
using Apis.Infrastructure.Ratings;
using DataAcessLayer.Models.Preferences;
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
        
        /// <summary>
        /// Repo inside repo 
        /// </summary>
        private readonly IRatings_Repo _rartingRepo;
        private readonly IClientVehicle_Repo _vehicleRepo;


        public Client_Repo(ApplicationDBContext context, UserManager<ApplicationUser> userManager, IRatings_Repo rartingRepo, IClientVehicle_Repo vehicleRepo)
        {
            _context = context;
            _userManager = userManager;
            _rartingRepo = rartingRepo;
            _vehicleRepo = vehicleRepo;
        }

        public async Task<string> GetProfileImage(int UserId)
        {
            string UserImage = await _context.ClientUsers.Where(x => x.Id == UserId).Select(user => user.ProfileImage).FirstOrDefaultAsync();
            return UserImage;
        }

        //public async Task<ClientUsers> ChangePassword(UserChangePassword user)
        //{
        //    ClientUsers userRecord = await _context.ClientUsers.Where(user => user.Id == user.Id).FirstOrDefaultAsync();
        //    if(userRecord != null)
        //    {
        //        if( userRecord.Password == user.OldPassword )
        //        {

        //        userRecord.Password
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}



        public async Task<object> GetUserPreferences(int TypeId, int UserId)
        {
            //Get Selected Id and AllOther Pref by it's TypeId
            int SelectedPrefId = await _context.TravelPreferences.Where(x => x.TypeId == TypeId).Include(x => x.User_preference)
                .Where(x => x.User_preference.Where(userPref => userPref.Travel_PreferenceId == x.Id && userPref.UserId == UserId).Count() > 0)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            //Other Pref
            List<TravelPreference> preferences = await _context.TravelPreferences.Where(pref => pref.TypeId == TypeId).ToListAsync();
            return new { selectedPreference = SelectedPrefId, preferences = preferences };
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

        public async Task<UserProfileMenu> MenuDetails(int UserId)
        {
            UserProfileMenu userData = await _context.ClientUsers.Where(x => x.Id == UserId).Include(x => x.UserPreference).ThenInclude(preference => preference.Travel_Preference).Select(x => new UserProfileMenu()
            {
                Id = x.Id,
                bio = x.Bio,
                UserName = x.Name,
                UserProfile = x.ProfileImage,
                Preference = x.UserPreference.Select(userPreference => userPreference.Travel_Preference.Title).ToList(),

            }).FirstOrDefaultAsync();
            userData.Vehicles = (await _vehicleRepo.GetUservehicleByUser(UserId)).Select(vehicle => new UserVehicles_ViewModel()
            {
                VehicleId = vehicle.VehicleId   ,
                VehicleColor = vehicle.Color.Color,
                VehicleImage = vehicle.VehicleImage,
                VehicleName = vehicle.Vehicle.Name,
                VeicleBrand = vehicle.Vehicle.VehicleBrand.Name
                
            }).ToList();
            

            return userData;
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
                    Preferences = x.UserPreference.Select(userPreference => userPreference.Travel_Preference.Title).ToList(),
                    Age = DateTime.Now.Subtract(x.BirthDate).Days / 365,
                    PartnerRating = _rartingRepo.GetPatrtnerRatings(UserId),
                    PublisherRating = _rartingRepo.GetPublisherRating(UserId)

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

        public async Task SavePreference(User_TravelPreference userPreferences)
        {
            if (userPreferences != null)
            {
                //check type
                //User_TravelPreference existedPreference = await _context.User_TravelPreferences
                //    .Include(usePref => usePref.Travel_Preference)
                //    .Where(x => x.UserId == userPreferences.UserId && x.Travel_PreferenceId == userPreferences.Travel_PreferenceId)
                //    .FirstOrDefaultAsync();


                //Get Type of Input Data and

                int Input_PrefTypeId = await _context.TravelPreferences.Where(x => x.Id == userPreferences.Travel_PreferenceId)
                    .Select(x => x.TypeId)
                    .FirstOrDefaultAsync();

                User_TravelPreference existedPreference = await _context.User_TravelPreferences
                   .Include(usePref => usePref.Travel_Preference)
                   .Where(x => x.UserId == userPreferences.UserId && x.Travel_Preference.TypeId == Input_PrefTypeId)
                   .FirstOrDefaultAsync();

                if (existedPreference == null)
                {
                    var res = await _context.User_TravelPreferences.AddAsync(userPreferences);
                }
                else
                {
                    existedPreference.Travel_PreferenceId = userPreferences.Travel_PreferenceId;
                    _context.User_TravelPreferences.Update(existedPreference);
                }
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<bool> UpdateImage(int UserId, string ProfileImage)
        {
            var UserData = await _context.ClientUsers.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if(UserData != null)
            {
                UserData.ProfileImage = ProfileImage;
                _context.ClientUsers.Update(UserData);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
