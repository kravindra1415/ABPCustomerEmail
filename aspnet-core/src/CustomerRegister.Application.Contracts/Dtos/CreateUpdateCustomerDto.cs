using CustomerRegister.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegister.Dtos
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string CourseName { get; set; }
    }
}
