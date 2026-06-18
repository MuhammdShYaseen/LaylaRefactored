using Layla.DataRepository;
using Layla.DomainEvents.Domain.Events;
using Layla.Models.DtosModels.EventDtos;
using Layla.Models.MainModels;
using Layla.Services.EventsDataProviderServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Layla.Services.EventsDataProviderServices.Implementation
{
    public class ReportCreatedEventDataProvider : IEventDataProvider<ReportCreatedEvent, ReportCreatedEventDto>
    {
        private readonly IRepository<Report> _reports;

        public ReportCreatedEventDataProvider(IRepository<Report> reports)
        {
            _reports = reports;
        }
        public async Task<ReportCreatedEventDto> GetDataAsync(ReportCreatedEvent @event, CancellationToken ct)
        {
            return await _reports.Query()
            .AsNoTracking()
            .Where(r => r.Guid == @event.ReportGuid)
            .Select(r => new ReportCreatedEventDto
            {
                ApartmentId = r.ApartmentId,
                ReporterId = r.ReporterId
            })
            .SingleAsync(ct);
        }
    }
}
