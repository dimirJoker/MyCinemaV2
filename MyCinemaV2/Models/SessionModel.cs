using System;
using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public class SessionModel
    {
        public uint Id { get; set; }

        [MaxLength(10)]
        [Range(1, uint.MaxValue)]
        public uint Movie_Id { get; set; }

        public DateTime Session { get; set; }
    }
}