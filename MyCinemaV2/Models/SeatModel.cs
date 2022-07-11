using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public class SeatModel
    {
        public uint Id { get; set; }

        [MaxLength(10)]
        [Range(1, uint.MaxValue)]
        public uint Session_Id { get; set; }

        [Range(1, 10)]
        public uint Seat_Row { get; set; }

        [Range(1, 50)]
        public uint Seat_Number { get; set; }

        [Range(0, 1)]
        public uint Status { get; set; }
    }
}