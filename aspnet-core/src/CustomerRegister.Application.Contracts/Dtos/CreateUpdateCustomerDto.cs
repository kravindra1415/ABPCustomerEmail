using CustomerRegister.EmailTemplates;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegister.Dtos
{
    public class CreateUpdateCustomerDto
    {
        [Required]

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public EmailTemplateTypes? EmailTemplateType { get; set; }
        [Required]
        public string CourseName { get; set; }
    }
}
