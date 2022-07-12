using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
    public class SeatModel
    {
        public uint Id { get; set; }

        public uint Session_Id { get; set; }

        public uint Seat_Row { get; set; }

        public uint Seat_Number { get; set; }

        [Range(0, 1)]
        public uint Status { get; set; }
    }
}