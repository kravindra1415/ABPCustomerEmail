using CustomerRegister.CustomerRegister;
using CustomerRegister.Dtos;
using CustomerRegister.Register;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace CustomerRegister
{
    //public class CustomerInfoService : CrudAppService<CustomerInfo, CustomerInfoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCustomerDto>
    //{
    //    public CustomerInfoService(IRepository<CustomerInfo, Guid> repository) : base(repository) { }
    //}

    public class CustomerInfoService : CustomerRegisterAppService, ICustomerInfoServices
    {
        private readonly IRepository<CustomerInfo, Guid> _repository;
        private readonly EmailSettings _emailSettings;

        //public CustomerInfoService(IRepository<CustomerInfo, Guid> repository, IOptions<EmailSettings> options)
        //{
        //    _repository = repository;
        //    _emailSettings = options.Value;
        //}

        public CustomerInfoService(IRepository<CustomerInfo, Guid> repository)
        {
            _repository = repository;

        }

        public async Task<PagedResultDto<CustomerInfoDto>> GetCustomers(GetCustomerDto input)
        {
            var customer = await _repository.GetListAsync();

            var totalCount = input.Filter == null
            ? await _repository.CountAsync()
            : await _repository.CountAsync(a => a.CustomerName.Contains(input.Filter));

            //var customerDto = ObjectMapper.Map<CustomerInfoDto, CustomerInfo>();

            return new PagedResultDto<CustomerInfoDto>(
                totalCount,
         ObjectMapper.Map<List<CustomerInfo>, List<CustomerInfoDto>>(customer));
        }

        public async Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            var customer = new CustomerInfo
            {
                CustomerName = input.CustomerName,
                CustomerEmail = input.CustomerEmail,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CourseName = input.CourseName
            };

            await _repository.InsertAsync(customer);

            EmailData emailData = new()
            {
                EmailToName = input.CustomerName,
                EmailBody = "HI " + input.CustomerName + ", This is the reminder for you that your course " + input.CourseName + ", is on " + input.StartDate,
                EmailToId = input.CustomerEmail,
                EmailSubject = "Subject"
            };

            SendEmail(emailData);

            return ObjectMapper.Map<CustomerInfo, CustomerInfoDto>(customer);
        }

        public bool SendEmail(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new();

                CustomerInfo info = new();

                MailboxAddress emailFrom = new(CustomerRegisterConsts.Name, CustomerRegisterConsts.EmailId);
                emailMessage.From.Add(emailFrom);

                //MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
                MailboxAddress emailTo = new(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = emailData.EmailSubject;

                BodyBuilder emailBodyBuilder = new()
                {
                    TextBody = emailData.EmailBody
                };
                emailMessage.Body = emailBodyBuilder.ToMessageBody();


                SmtpClient emailClient = new();

                emailClient.Connect(CustomerRegisterConsts.Host, CustomerRegisterConsts.Port, CustomerRegisterConsts.UseSSL);
                emailClient.Authenticate(CustomerRegisterConsts.EmailId, CustomerRegisterConsts.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}

//"EmailSettings": {
//    "EmailId": "testtechnology1020@gmail.com",
//    "Name": "Support - Pro Code Guide",
//    "Password": "nnyxycfwbpdmtnte",
//    "Host": "smtp.gmail.com",
//    "Port": 465,
//    "UseSSL": true
//  },