using Apis.Data;
using Apis.Infrastructure.Client;
using Apis.Infrastructure.Ratings;
using DataAcessLayer.Models.Preferences;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Client;
using EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using bc = BCrypt.Net.BCrypt;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Apis.Repos.Client
{
    public class Client_Repo : IClient_Repo
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Repo inside repo 
        /// </summary>
        private readonly IRatings_Repo _rartingRepo;
        private readonly IClientVehicle_Repo _vehicleRepo;


        public Client_Repo(ApplicationDBContext context, UserManager<ApplicationUser> userManager, IRatings_Repo rartingRepo, IClientVehicle_Repo vehicleRepo
            , IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _rartingRepo = rartingRepo;
            _vehicleRepo = vehicleRepo;
            _emailSender = emailSender;
        }

        public async Task<string> GetProfileImage(int UserId)
        {
            string UserImage = await _context.ClientUsers.Where(x => x.Id == UserId).Select(user => user.ProfileImage).FirstOrDefaultAsync();
            return UserImage;
        }

        public async Task<int> ChangePassword(UserChangePassword updatedPassword)
        {
            ClientUsers userRecord = await _context.ClientUsers.FindAsync(updatedPassword.UserId);
            if (userRecord != null)
            {
                if (userRecord.Password == updatedPassword.OldPassword)
                {
                    userRecord.Password = updatedPassword.NewPassword;
                    _context.ClientUsers.Update(userRecord);
                    await _context.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        //public async Task<bool> (UserChangePassword user)
        //{
        //    ClientUsers userRecord = await _context.ClientUsers.Where(user => user.Id == user.Id).FirstOrDefaultAsync();
        //    if (userRecord != null)
        //    {
        //        if (userRecord.Password == user.OldPassword)
        //        {

        //            userRecord.Password
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //} //boolllllllllllllllllllllllllllllll



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
            ClientUsers record = _context.ClientUsers.FirstOrDefault(item => item.Email == clientUsers.Email && bc.Verify(clientUsers.Password,item.Password)  );
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
                VehicleId = vehicle.VehicleId,
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
            clientUsers.Password = bc.HashPassword(clientUsers.Password);
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

        public bool ResetPassword(int UserId, string Email)
        {
            string randomPassword = Guid.NewGuid().ToString("d").Substring(1, 9);
            ClientUsers user = _context.ClientUsers.Find(UserId);
            if (user != null)
            {
                user.Password = randomPassword;
                _context.ClientUsers.Update(user);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }



            var MsgBody = $"<html lang='en-US'>  <head> <meta content='text/html; charset=utf-8' http-equiv='Content-Type' /> <title>Reset Password Email Template</title> <meta name='description' content='Reset Password Email Template.'></head>  <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'> <!--100% body table--> <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8' style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'> <tr> <td> <table style='background-color: #f2f3f8; max-width:670px;  margin:0 auto;' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'> <tr> <td style='height:80px;'>&nbsp;</td> </tr> <tr> <td style='text-align:center;'>  </td> </tr> <tr> <td style='height:20px;'>&nbsp;</td> </tr> <tr> <td> <table width='95%' border='0' align='center' cellpadding='0' cellspacing='0' style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'> <tr> <td style='height:40px;'>&nbsp;</td> </tr> <tr> <td style='padding:0 35px;'> <h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>You have requested to reset your password</h1> <span style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span> <p style='color:#455056; font-size:15px;line-height:24px; margin:0;'> Here is your new password. Kidnly Login to your account via same and for security kindly change password immediately  </p> <a href='javascript:void(0);' style='background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>{randomPassword}</a> </td> </tr> <tr> <td style='height:40px;'>&nbsp;</td> </tr> </table> </td> <tr> <td style='height:20px;'>&nbsp;</td> </tr> <tr> <td style='text-align:center;'> <p style='font-size:14px; color:rgba(69, 80, 86, 0.7411764705882353); line-height:18px; margin:0 0 0;'>&copy; <strong>bvshah.com</strong></p> </td> </tr> <tr> <td style='height:80px;'>&nbsp;</td> </tr> </table> </td> </tr> </table> <!--/100% body table--> </body>  </html>";
            var message = new Message(Email, "Reset Password", MsgBody);
            _emailSender.SendEmail(message);
            return true;
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
            if (UserData != null)
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
