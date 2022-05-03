using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers.ClientController
{
    public class UserController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment WebHostEnviroment;


        public UserController(IConfiguration config, IWebHostEnvironment webHostEnviroment)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
            WebHostEnviroment = webHostEnviroment;
        }

        public string UploadFile(IFormFile Image)
        {
            string fileName = null;

            if (Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnviroment.WebRootPath, "Img");
                fileName = Guid.NewGuid().ToString() + "-" + Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }


        [HttpGet]
        public IActionResult Profile(int id)
        {
            HttpResponseMessage responseProfile = httpClient.GetAsync($"ClientUser/PublicProfile/{id}").Result;
            if (responseProfile.IsSuccessStatusCode)
            {
                string res = responseProfile.Content.ReadAsStringAsync().Result;
                ClientPublicProfile userProfile = JsonConvert.DeserializeObject<ClientPublicProfile>(res);
                return View(userProfile);
            }
            return View();
        }



        [HttpGet]
        public IActionResult Menu()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {


                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"Clientuser/GetMenuDetails/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    UserProfileMenu userProfile = JsonConvert.DeserializeObject<UserProfileMenu>(res);
                    return View(userProfile);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpGet]
        public IActionResult ProfileImage()
        {

            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                //Get User Old Profile Picture
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"ClientUser/GetProfileImage/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    ViewBag.UserProfileImage = res;
                    
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        [HttpPost]
        public IActionResult ProfileImage(UpdateUserImage userProfile, string? OldImage)
        {
            
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {
                if (!string.IsNullOrEmpty(OldImage))
                {
                    string uploadDir = Path.Combine(WebHostEnviroment.WebRootPath, "Images");
                    FileInfo file = new FileInfo(Path.Combine(uploadDir, OldImage));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                string ImageName =  UploadFile(userProfile.ProfileImage);
                
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"Clientuser/UpdatePicture/{UserId}", ImageName).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                }
                return RedirectToAction("Menu","User");
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }


        public Boolean IsLogin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return false;
            }
            return true;
        }
    }
}
