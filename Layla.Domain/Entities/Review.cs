using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Layla.Domain.Common;
using Layla.Domain.Events;
namespace Layla.Domain.Entities
{
    public class Review : Entity
    {

        [Required]
        public int ApartmentId { get; private set; }

        [ForeignKey("ApartmentId")]
        public Apartment? Apartment { get; set; }

        [Required]
        public int UserId { get; private set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Range(1, 5)]
        public int Rating { get; private set; }

        [MaxLength(1000)]
        public string? Comment { get; private set; }

        public static Review Create(int userId, int apartmentId,  int rating, string comment)
        {
            var review = new Review
            {
                UserId = userId,
                Comment = comment,
                Rating = rating,
            };

            review.AddDomainEvent(new ReviewCreatedEvent(review.Guid));
            
            return review;
        }

        public void Update(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
            Touch();
        }
    }
}