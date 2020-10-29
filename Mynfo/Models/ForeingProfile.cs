using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mynfo.Models
{
    public class ForeingProfile
    {
        [PrimaryKey]
        public int IdForeingProfile { get; set; }

        public int IdBox { get; set; }

        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public ValueType value { get; set; }
    }
}
