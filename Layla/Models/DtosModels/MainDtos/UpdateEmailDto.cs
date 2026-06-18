using System.ComponentModel.DataAnnotations;

namespace Layla.Models.DtosModels.MainDtos
{
    public class UpdateEmailDto
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; } = default!;
    }
}
