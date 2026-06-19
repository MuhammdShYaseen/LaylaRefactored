using Layla.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Layla.Models.DtosModels.MessageDtos
{
    public sealed class SendVoiceDto
    {
        [Required]
        public int ApartmentId { get; init; }

        [Required]
        [AllowedContentTypes("audio/mpeg", "audio/wav", "audio/ogg")]
        [MaxFileSize(2 * 1024 * 1024)]
        public IFormFile AudioFile { get; init; } = default!;

        [Range(1, 600)]
        public int DurationSeconds { get; init; }
    }
}
