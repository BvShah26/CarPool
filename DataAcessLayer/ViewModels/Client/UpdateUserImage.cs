using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UpdateUserImage
    {
        public int Id { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
