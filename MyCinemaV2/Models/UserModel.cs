using System.ComponentModel.DataAnnotations;

namespace MyCinemaV2.Models
{
	public class UserModel
	{
		public uint Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Username { get; set; }

		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
