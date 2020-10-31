using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Mynfo.Models
{
    public class Get_nfc
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido_paterno")]
        public string Apellido_paterno { get; set; }

        [JsonProperty("apellido_materno")]
        public string Apellido_materno { get; set; }

        [JsonProperty("id_usuario")]
        public string Id_usuario { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }                 
}