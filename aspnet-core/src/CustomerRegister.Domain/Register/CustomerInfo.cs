using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CustomerRegister.Register
{
    public class CustomerInfo : AuditedAggregateRoot<Guid>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CourseName { get; set; }
    }
}