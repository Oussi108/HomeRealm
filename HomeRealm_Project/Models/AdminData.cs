﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    public class AdminData
    {
        [JsonProperty("adminUsers")]
        public List<AdminUser> adminUsers { get; set; }
    }
}