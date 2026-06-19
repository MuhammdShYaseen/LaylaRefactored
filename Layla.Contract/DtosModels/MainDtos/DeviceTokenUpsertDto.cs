using System.ComponentModel.DataAnnotations;

namespace Layla.Models.DtosModels.MainDtos
{
    public class DeviceTokenUpsertDto
    {
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public string Platform { get; set; } = null!;
        [Required]
        public string DeviceId { get; private set; } = null!;
    }
}
