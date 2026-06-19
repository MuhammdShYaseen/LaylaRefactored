using System.ComponentModel.DataAnnotations;

namespace Layla.Models.DtosModels.MainDtos
{
    public class CreateContractDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public string ContractUrl { get; set; } = string.Empty;

        public string? SpecialTerms { get; set; }
    }
}
