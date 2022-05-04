﻿using Apis.Infrastructure.Client;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        private readonly IClient_Repo _Repo;

        public ClientUserController(IClient_Repo repo)
        {
            _Repo = repo;
        }
        // GET: api/<ClientUserController>
        [HttpGet]
        public IEnumerable<ClientUsers> Get()
        {
            return _Repo.GetUsers();
        }

        // GET api/<ClientUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientUserController>
        [HttpPost]
        public ClientUsers Post(ClientUsers clientUsers)
        {
            return _Repo.RegisterUser(clientUsers);
        }

        [HttpPost("Login")]
        public IActionResult Login(ClientUsers clientUsers)
        {
            if (clientUsers == null)
            {
                return BadRequest();
            }
            ClientUsers res = _Repo.LoginUser(clientUsers);
            if (res == null)
            {
                return Unauthorized();
            }
            return Ok(res);

        }

        //Public Profile // Doing
        [HttpGet("PublicProfile/{UserId}")]
        public async Task<IActionResult> PublicProfile(int UserId)
        {
            ClientPublicProfile userProfile = await _Repo.PublicProfile(UserId);
            return Ok(userProfile);
        }


        [HttpGet("GetMenuDetails/{UserId}")]
        public async Task<IActionResult> GetMenuDetails(int UserId)
        {

            try
            {
                UserProfileMenu profileMenu = await _Repo.MenuDetails(UserId);


                return Ok(profileMenu);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet("GetProfileImage/{UserId}")]
        public async Task<IActionResult> GetProfileImage(int UserId)
        {
            string UserProfileImage = await _Repo.GetProfileImage(UserId);
            return Ok(UserProfileImage);

        }

        [HttpPut("UpdatePicture/{UserId}")]
        public async Task<IActionResult> UpdatePicture(int UserId, [FromBody] string ProfileImage)
        {
            bool IsUpdated = await _Repo.UpdateImage(UserId, ProfileImage);
            return Ok(IsUpdated);
        }

        [HttpPut("ResetPassword/{UserId}")]
        public async Task<IActionResult> ResetPassword(int UserId, [FromBody] string Email)
        {
            try
            {
                bool isUpdated = _Repo.ResetPassword(UserId, Email);
                if (isUpdated == false)
                {
                    return NotFound();
                }
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("ChangePassword/{UserId}")]
        public async Task<IActionResult> ChangePassword(int UserId, [FromBody] UserChangePassword changePassword)
        {
            if (UserId != changePassword.UserId)
            {
                return BadRequest();
            }
            try
            {
                int isUpdated = await _Repo.ChangePassword(changePassword);
                if (isUpdated == -1)
                {
                    return NotFound();
                }
                if (isUpdated == 0)
                {
                    return Unauthorized();
                }
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet("GetUserEdit/{UserId}")]
        public async Task<IActionResult> GetUserEdit(int UserId)
        {
            try
            {
                var userRecord = await _Repo.GetUserEdit(UserId);
                if (userRecord == null)
                {
                    return NotFound();
                }
                return Ok(userRecord);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        [HttpPut("EditProfile/{UserId}")]

        public async Task<IActionResult> EditProfile(int UserId, [FromBody] UserEditProfile editProfile)
        {
            if (editProfile.UserId != UserId)
            {
                return BadRequest();
            }


            try
            {
                var rec = await _Repo.EditProfile(editProfile);
                if (rec == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }

        }
    }
}
