using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment WebHostEnviroment;

        public AccountController(IConfiguration config, IWebHostEnvironment webHostEnviroment)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
            WebHostEnviroment = webHostEnviroment;
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
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
        [HttpPost]
        public IActionResult Register(UserRegistration_ViewModel userRegister)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (userRegister != null)
                    {
                        ClientUsers clientUsers = new ClientUsers()
                        {
                            MobileNumber = userRegister.MobileNumber,
                            Password = userRegister.Password,
                            Name = userRegister.Name,
                            Gender = userRegister.Gender,
                            BirthDate = userRegister.BirthDate,
                            
                            Address = userRegister.Address,
                            Email = userRegister.Email,
                        };
                        if (userRegister.ProfileImage != null)
                        {
                            clientUsers.ProfileImage = UploadFile(userRegister.ProfileImage);
                        }

                        HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser", clientUsers).Result;
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            string res = responseMessage.Content.ReadAsStringAsync().Result;
                            ClientUsers client = JsonConvert.DeserializeObject<ClientUsers>(res);
                            HttpContext.Session.SetString("UserName", client.Name);
                            HttpContext.Session.SetInt32("UserId", client.Id);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Registration Failed");
                }
            }
            return View(userRegister);

        }

        [HttpGet]
        public IActionResult Login(string? url)
        {
            ViewBag.ReturnURL = url;
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClientLoginView clientLogin)
        {
            if (clientLogin != null)
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser/Login", clientLogin).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    ClientUsers client = JsonConvert.DeserializeObject<ClientUsers>(res);

                    HttpContext.Session.SetString("UserName", client.Name);
                    HttpContext.Session.SetInt32("UserId", client.Id);
                    if (String.IsNullOrEmpty(clientLogin.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return LocalRedirect(clientLogin.ReturnUrl);
                }
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewBag.InvalidCredentials = true;
                }
                return View(clientLogin);

            }
            return View();
        }

        //Login Identity

    }
}
