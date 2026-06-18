using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Layla.DomainEvents.Domain.Common;
using Layla.DomainEvents.Domain.Events;

namespace Layla.Models.MainModels
{
    public class Report : Entity
    {
        public enum ReportStatus
        {
            Pending,
            Reviewed,
            Resolved,
            Rejected
        }

        [Required]
        public int ReporterId { get; private set; } // المستخدم الذي قام بالتبليغ

        [ForeignKey("ReporterId")]
        public User? Reporter { get; set; }

        [Required]
        public int ApartmentId { get; private set; } // الشقة المبلغ عنها

        [ForeignKey("ApartmentId")]
        public Apartment? Apartment { get; set; }

        [Required, MaxLength(1000)]
        public string Reason { get; private set; } = string.Empty; // سبب التبليغ

        [MaxLength(50)]
        public ReportStatus Status { get; private set; } = ReportStatus.Pending; // Pending, Reviewed, Rejected, Resolved

        public static Report Create(int apartmentId, int reporterId ,string reason)
        {
            var report = new Report
            {
                Reason = reason,
                ApartmentId = apartmentId,
                ReporterId = reporterId,
                Status = ReportStatus.Pending
            };

            report.AddDomainEvent(new ReportCreatedEvent(report.Guid));
            return report;
        }

        public void ChangeStatus(ReportStatus newStatus)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            Touch();

            // مستقبلاً:
            // AddDomainEvent(new ReportStatusChangedEvent(...));
        }
    }
}
