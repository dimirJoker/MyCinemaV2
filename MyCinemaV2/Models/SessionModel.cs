using System;
using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public class SessionModel
    {
        public uint? Id { get; set; }
        public uint? Movie_Id { get; set; }

        [Required]
        public DateTime? Session { get; set; }
    }
}