namespace Mynfo.Models
{
    using System;
    public class BoxLocal
    {
        public int BoxId { get; set; }
        public string Name { get; set; }
        public bool BoxDefault { get; set; }
        public int UserId { get; set; } 
        public DateTime Time { get; set; }
    }
}
