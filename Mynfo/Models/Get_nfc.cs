﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Mynfo.Models
{
    public class Get_nfc
    {
        [JsonProperty("BoxId")]
        public string boxId { get; set; }

        [JsonProperty("Name")]
        public string name { get; set; }

        [JsonProperty("BoxDefault")]
        public string boxDefault { get; set; }

        [JsonProperty("UserId")]
        public string userId { get; set; }

        [JsonProperty("Time")]
        public string time { get; set; }

        [JsonProperty("ImagePath")]
        public string imagePath { get; set; }

        [JsonProperty("UserTypeId")]
        public string userTypeId { get; set; }

        [JsonProperty("FirstName")]
        public string firstName { get; set; }

        [JsonProperty("LastName")]
        public string lastName { get; set; }

        [JsonProperty("ImageFullPath")]
        public string imageFullPath { get; set; }

        [JsonProperty("FullName")]
        public string fullName { get; set; }
    }
}                   




