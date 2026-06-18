using Layla.Models.MainModels;

namespace Layla.Services.DataCRUD.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment?> GetByBookingIdAsync(int bookingId);
        Task<Payment> AddAsync(Payment payment);
        Task<Payment?> UpdateAsync(int id, Payment payment);
        Task<bool> DeleteAsync(int id);
    }
}
