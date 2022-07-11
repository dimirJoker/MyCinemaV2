using System;
using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public class MovieModel
    {
        public uint Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Thumbnail { get; set; }

        [Range(0, float.MaxValue)]
        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Range(0, 1)]
        public uint Status { get; set; }
    }
}