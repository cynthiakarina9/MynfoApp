namespace Mynfo.Models
{
    using System;
    public class Box
    {
        public int BoxId { get; set; }
        public string Name { get; set; }
        public bool BoxDefault { get; set; }
        public int UserId { get; set; } 
        public DateTime Date { get; set; }
    }
}
