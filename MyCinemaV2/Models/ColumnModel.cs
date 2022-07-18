using System;
using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public abstract class ColumnModel
    {
        public uint? Id { get; set; }
        public uint? Movie_Id { get; set; }
        public uint? Session_Id { get; set; }

        public uint? Seat_Row { get; set; }
        public uint? Seat_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan? Duration { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Thumbnail { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Range(0, 1)]
        public uint? Status { get; set; }

        [Required]
        public DateTime? Session { get; set; }
    }
}