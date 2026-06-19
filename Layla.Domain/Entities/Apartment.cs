using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Layla.Domain.Common;
using Layla.Domain.ValueObjects.ApartmentValueObject;
using Layla.Domain.Events;

namespace Layla.Domain.Entities
{
    public class Apartment : Entity
    {
        #region Enums
        public enum Amenities
        {
            ShellAndCore,
            Basic,
            Standard,
            Good,
            Luxury,
            SuperLuxury,
            FullyFitted,
        }
        public enum ApartmentView
        {
            SeaView,
            MountainView,
            CityView,
            GardenView,
            StreetView,
            PanoramicView,
            DualAspect,
            CornerView,
            None
        }
        public enum BuildingType
        {
            Apartment,
            Villa,                  // فيلا
            Townhouse,              // تاون هاوس
            TwinHouse,              // منزل ثنائي
            DetachedHouse,          // منزل منفصل
            SemiDetachedHouse,      // منزل شبه منفصل
            Duplex,                 // دوبلكس
            Triplex,                // تريبلوكس
            Penthouse,              // بنتهاوس
            Palace,                 // قصر
            Farmhouse,              // مزرعة
            Chalet,                 // شاليه
        }
        #endregion

        [Required, MaxLength(200)]
        public string Title { get; private set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; private set; }

        [Required]
        public GeoLocation? Location { get; private set; }
        public int CityId { get; private set; }
        public City City { get; private set; } = null!;

        #region Detiles
        public int NumberOfBedRooms { get; private set; } = 1;
        public int NumberOfLivingRooms { get; private set; } = 1;
        public int NumberOfReceptionRooms { get; private set; } = 1;
        public int NumberOfBathrooms { get; private set; } = 1;
        public int NumberOfBalconies { get; private set; } = 0;
        public int FloorNumber { get; private set; } = 0;
        public double Area { get; private set; }
        public string Orientation { get; private set; } = string.Empty;
        public BuildingType Type { get; private set; } = BuildingType.Apartment;
        public ApartmentView View {  get; private set; } = ApartmentView.None;
        public Amenities Finishing { get; private set; } = Amenities.Standard;
        public bool HasElevator { get; private set; } = false;
        public bool HasParking { get; private set; } = false;
        public bool HasPool { get; private set; } = false;

        #endregion

        [Required]
        public Money PricePerHour { get; private set; } =  Money.Create(1);

        [Required]
        public Money PricePerDay { get; private set; } = Money.Create(1);

        public bool IsAvailable { get; private set; } = true;
        

        [Required]
        public int OwnerId { get; private set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; } = null!;
        public bool IsChatEnabled { get; private set; } = true;
        public ICollection<Booking> Bookings { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<MediaFile> MediaFiles { get; set; } = null!;
        public ICollection<Conversation> Conversations { get; set; } = null!;

        public static Apartment Create(string street, string buildingNumber, string apartmentNumber, string city, string district,
                                       double latitude, double longitude, string country, int ownerId, double area,
                                       Amenities finishing, int floorNumber, bool hasElevator, bool hasParking, bool hasPool,
                                       int numberOfBalconies, int numberOfBathrooms, int numberOfBedRooms, int numberOfLivingRooms,
                                       int numberOfReceptionRooms, string orientation, ApartmentView view, BuildingType type,
                                       decimal pricePerHour, decimal pricePerDay, string description, bool isAvailable, string title)
        {
            var location = GeoLocation.Create(street, buildingNumber,
                                              apartmentNumber, city,
                                              district, country, Coordinates.Create(latitude, longitude)
                                              );
            var apartment = new Apartment
            {
                Location = location,
                Area = area,
                Finishing = finishing,
                FloorNumber = floorNumber,
                HasElevator = hasElevator,
                HasParking = hasParking,
                HasPool = hasPool,
                NumberOfBalconies = numberOfBalconies,
                NumberOfBathrooms = numberOfBathrooms,
                NumberOfBedRooms = numberOfBedRooms,
                NumberOfLivingRooms = numberOfLivingRooms,
                NumberOfReceptionRooms = numberOfReceptionRooms,
                Orientation = orientation,
                View = view,
                Type = type,
                PricePerHour = Money.Create(pricePerHour),
                PricePerDay = Money.Create(pricePerDay),
                Description = description,

                Title = string.IsNullOrWhiteSpace(title) ? throw new AggregateException("Title must not be empty") : title,
                IsAvailable = isAvailable,
                OwnerId = ownerId,
                IsChatEnabled = true
            };

            apartment.AddDomainEvent(new ApartmentCreatedEvent(apartment.Guid));

            return apartment;
        }
        public void Update(string street, string buildingNumber, string apartmentNumber, string city, string district,
                                       double latitude, double longitude, string country, int ownerId, double area,
                                       Amenities finishing, int floorNumber, bool hasElevator, bool hasParking, bool hasPool,
                                       int numberOfBalconies, int numberOfBathrooms, int numberOfBedRooms, int numberOfLivingRooms,
                                       int numberOfReceptionRooms, string orientation, ApartmentView view, BuildingType type,
                                       decimal pricePerHour, decimal pricePerDay, string description, bool isAvailable, string title)
        {

            var location = GeoLocation.Create(street, buildingNumber,
                                             apartmentNumber, city,
                                             country,
                                             district, Coordinates.Create(latitude, longitude)
                                             );

            Location = location;
            Area = area;
            Finishing = finishing;
            FloorNumber = floorNumber;
            HasElevator = hasElevator;
            HasParking = hasParking;
            HasPool = hasPool;
            NumberOfBalconies = numberOfBalconies;
            NumberOfBathrooms = numberOfBathrooms;
            NumberOfBedRooms = numberOfBedRooms;
            NumberOfLivingRooms = numberOfLivingRooms;
            NumberOfReceptionRooms = numberOfReceptionRooms;
            Orientation = orientation;
            View = view;
            Type = type;
            PricePerHour = Money.Create(pricePerHour);
            PricePerDay = Money.Create(pricePerDay);
            Description = description;

            Title = string.IsNullOrWhiteSpace(title) ? throw new AggregateException("Title must not be empty") : title;
            IsAvailable = isAvailable;
            OwnerId = ownerId;
            IsChatEnabled = true;
            Touch();

        }

        public void EnableDisableChat(bool isChatEnabled)
        {
            IsChatEnabled = isChatEnabled;
        }
        public void Availability (bool isAvailable)
        {
            IsAvailable = isAvailable;
        }

       
    }
}